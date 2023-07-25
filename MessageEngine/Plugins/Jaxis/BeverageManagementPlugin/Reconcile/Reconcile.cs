using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BeverageMonitor.Entities;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
//using Jaxis.Inventory.Data;
using Jaxis.Util.Log4Net;
using Ingredient = BeverageMonitor.Entities.Ingredient;
using IPour = Jaxis.Inventory.Data.IPour;
using Location = BeverageMonitor.Entities.Location;
using POSTicketItem = BeverageMonitor.Entities.POSTicketItem;
using Recipe = BeverageMonitor.Entities.Recipe;
using StandardPour = BeverageMonitor.Entities.StandardPour;
using TicketItemAlias = BeverageMonitor.Entities.TicketItemAlias;
using UPC = BeverageMonitor.Entities.UPC;

namespace Jaxis.BeverageManagement
{
    public class Reconcile
    {
        protected class ItemPour
        {
            public IPour pour { get; set; }
            public Ingredient ingredient { get; set; }
            public POSTicketItem ticketItem { get; set; }

            public IPour pourAddition { get; set; }
        }

        private const double MaxSplash = 5; // MLF this needs to be a Admin setting
        private const int MaxSplashTime = 15; // MLF this needs to be a Admin setting
        private const string ReconcileLogger = "POSReconcile";

        public void ConsolidatedReconcile(int _interval) 
        {
            try
            {
                using (var data = new BeverageMonitorEntities())
                {
                    data.CommandTimeout = 600;
                    int interval = _interval;

                    ReconcileByNone(data);

                    ReconcileByManufacturer(data, interval, 4);
                    ReconcileByManufacturer(data, interval, 64, true);
                    ReconcileByManufacturer(data, interval, 128, false, true);
                    ReconcileByManufacturerNoQuality(data, interval, 256, true, true);

                    ReconcileByCategory(data, interval, 4);
                    ReconcileByCategory(data, interval, 64, true);
                    ReconcileByCategory(data, interval, 128, false, true);
                    ReconcileByCategoryNoQuality(data, interval, 256, true, true);

                    ReconcileByUPC(data, interval, 4);
                    ReconcileByUPC(data, interval, 64, true);
                    ReconcileByUPC(data, interval, 128, false, true);
                    ReconcileByUPCByCategory(data, interval, 256, true, true);

                    interval = _interval * 2;

                    ReconcileByManufacturer(data, interval, 4);
                    ReconcileByManufacturer(data, interval, 64, true);
                    ReconcileByManufacturer(data, interval, 128, false, true);
                    ReconcileByManufacturerNoQuality(data, interval, 256, true, true);

                    ReconcileByCategory(data, interval, 4);
                    ReconcileByCategory(data, interval, 64, true);
                    ReconcileByCategory(data, interval, 128, false, true);
                    ReconcileByCategoryNoQuality(data, interval, 256, true, true);

                    ReconcileByUPC(data, interval, 4);
                    ReconcileByUPC(data, interval, 64, true);
                    ReconcileByUPC(data, interval, 128, false, true);
                    ReconcileByUPCByCategory(data, interval, 256, true, true);

                    PourCombiner(data, interval, 4);

                    UpdateTicketItemStatus(data);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("Reconcile::ConsolidatedReconcile()", exp);
            }
        }

        protected void ReconcileByWhatever(BeverageMonitorEntities _data, IQueryable<ItemPour> _pourtickets, int _status)
        {
            try
            {
                var matchedPours = new Dictionary<Guid, List<Guid>>();

                var results = _pourtickets.ToList();

                var items = results.OrderBy(i => i.ticketItem.POSTicketItemID).ToList();
                Log.Debug( ReconcileLogger, String.Format("ReconcileByWhatever {0}, {1}", items.Count, _status));

                if (0 < items.Count)
                {
                    foreach (var item in items)
                    {
                        //if (item.ticketItem.Reconciled < item.ticketItem.Quantity &&
                        if (item.pour.POSTicketItemID == null)
                        {
                            if (!matchedPours.ContainsKey(item.ticketItem.POSTicketItemID))
                            {
                                matchedPours[item.ticketItem.POSTicketItemID] = new List<Guid>();
                            }
                            if (!matchedPours[item.ticketItem.POSTicketItemID].Contains(item.ingredient.IngredientID))
                            {
                                matchedPours[item.ticketItem.POSTicketItemID].Add(item.ingredient.IngredientID);
                                item.pour.POSTicketItemID = item.ticketItem.POSTicketItemID;
                                item.pour.IngredientID = item.ingredient.IngredientID;
                                item.pour.Status = _status;
                                if (item.pourAddition != null && item.pourAddition.POSTicketItemID == null)
                                {
                                    item.pourAddition.POSTicketItemID = item.ticketItem.POSTicketItemID;
                                    item.pourAddition.IngredientID = item.ingredient.IngredientID;
                                    item.pour.Status = 512;
                                }
                            }
                            else
                            {
                                Log.Debug( ReconcileLogger, string.Format("Ingredient for Ticket Item {0} already matched",
                                                                item.ticketItem.Description));
                            }
                        }
                        else
                        {
                            Log.Debug( ReconcileLogger, "Ticket Item Already closed");
                        }
                    }
                    _data.SaveChanges();
                }
            }
            catch (Exception err)
            {
                Log.Exception( err );
            }
        }

        protected void UpdateTicketItemStatus(BeverageMonitorEntities data)
        {
            try
            {
                var pourtickets =
                    from TI in data.POSTicketItems
                    join T in data.POSTickets on TI.POSTicketID equals T.POSTicketID
                    join A in data.TicketItemAliases on TI.Description equals A.Description
                    join R in data.Recipes on A.RecipeID equals R.RecipeID
                    join I in data.Ingredients on A.RecipeID equals I.RecipeID
                    join L in data.Locations on T.Establishment equals L.POSAlias
                    join P in data.Pours on
                        new
                        {
                            PourTicketItemID = (Guid?)TI.POSTicketItemID,
                            PourIngredientID = (Guid?)I.IngredientID
                        }
                        equals new { PourTicketItemID = P.POSTicketItemID, PourIngredientID = P.IngredientID }
                    select new { ticketItem = TI, pour = P, alias = A };

                decimal price = 10.00m;
                var adminValue = data.AdministrativeValues.FirstOrDefault(v => v.PropertyName == "Default Drink Price");
                if (null != adminValue)
                {
                    price = decimal.Parse(adminValue.PropertyValue);
                } 
                foreach (var pourticket in pourtickets)
                {
                    pourticket.ticketItem.Status ^= 2;
                    pourticket.ticketItem.Status |= pourticket.pour.Status ?? 0;
                    if (null == pourticket.ticketItem.Price || 0.01m > pourticket.ticketItem.Price.Value || pourticket.ticketItem.Price.Value == price)
                    {
                        pourticket.ticketItem.Price = ( 0 != pourticket.alias.Price ) ? pourticket.alias.Price : price;
                    }
                }

                //var ticket =
                //    from TI in data.POSTicketItems
                //    join T in data.POSTickets on TI.POSTicketID equals T.POSTicketID
                //    join A in data.TicketItemAliases on TI.Description equals A.Description
                //    join R in data.Recipes on A.RecipeID equals R.RecipeID
                //    join I in data.Ingredients on A.RecipeID equals I.RecipeID
                //    join L in data.Locations on T.Establishment equals L.POSAlias
                //    join P in data.Pours on
                //        new
                //        {
                //            PourTicketItemID = (Guid?)TI.POSTicketItemID,
                //            PourIngredientID = (Guid?)I.IngredientID
                //        }
                //        equals new { PourTicketItemID = P.POSTicketItemID, PourIngredientID = P.IngredientID }
                //    into missingPours
                //    from P in missingPours.DefaultIfEmpty()
                //    where TI.Reconciled < TI.Quantity
                //    select new { ticketItem = TI, Ingredient = I, pour = P };

                //var uniqueTI = (from t in ticket
                //                select new { t.ticketItem.POSTicketItemID }).Distinct();
                //foreach (var id in uniqueTI)
                //{
                //    var nullItems = ticket.Where(t => t.pour == null).Count();
                //    if( nullItems == 0 )
                //    {
                //    }
                //    var items = ticket.Where(t => t.ticketItem.POSTicketItemID.Equals( id ) );
                //    int count = items.Count();
                //    int pours = 0;
                //    bool containsNull = false;
                //    foreach (var item in items)
                //    {
                //        if( null == item.pou)

                //        if (null != item.pour)
                //        {
                //            pours++;
                //        }
                //    }
                //    if( count > pours )
                //}

                data.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }


        protected void ReconcileByNone(BeverageMonitorEntities data)
        {
            try
            {
                var pourtickets =
                    from TI in data.POSTicketItems
                    join T in data.POSTickets on TI.POSTicketID equals T.POSTicketID
                    join A in data.TicketItemAliases on TI.Description equals A.Description
                    where A.RecipeID == null && TI.Status == 2
                    select TI;

                Log.Debug( ReconcileLogger, string.Format( "Clearing out {0} tickets marked as 'Ignore'", pourtickets.Count()));

                foreach (var pourticket in pourtickets)
                {
                    pourticket.Status = 4;
                }
                data.SaveChanges();
            }
            catch (Exception err)
            {
                Log.Exception(err);
            }
        }


        protected void ReconcileByManufacturer(BeverageMonitorEntities data, int _interval, int _status, bool _overVolume = false, bool _underVolume = false)
        {
            try
            {
                var pourtickets =
                    from TI in data.POSTicketItems
                    join T in data.POSTickets on TI.POSTicketID equals T.POSTicketID
                    join A in data.TicketItemAliases on TI.Description equals A.Description
                    join R in data.Recipes on A.RecipeID equals R.RecipeID
                    join I in data.Ingredients on A.RecipeID equals I.RecipeID
                    join SP in data.StandardPours on I.StandardPourID equals SP.StandardPourID
                    join L in data.Locations on T.Establishment equals L.POSAlias
                    join U in data.UPCs on
                        new
                        {
                            ManufacturerID = (I.ManufacturerID.HasValue) ? I.ManufacturerID.Value : Guid.Empty,
                            Quality = (I.Quality.HasValue) ? I.Quality.Value : -1
                        }
                        equals new { ManufacturerID = U.ManufacturerID, Quality = U.Quality }
                    join P in data.Pours on U.UPCID equals P.UPCID
                    where TI.Status == 2 &&
                          P.POSTicketItemID == null &&
                          P.PourTime < System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval) &&
                          P.PourTime > System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval * -1) &&
                          (_overVolume || P.Volume > (SP.PourStandard - (SP.PourStandard * SP.StandardVariance))) &&
                          (_underVolume || P.Volume < (SP.PourStandard + (SP.PourStandard * SP.StandardVariance))) &&
                          P.LocationID == L.LocationID
                    select new ItemPour { ticketItem = TI, pour = P, ingredient = I };

                ReconcileByWhatever(data, pourtickets, _status);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void PourCombiner(BeverageMonitorEntities data, int _interval, int _status, bool _overVolume = false, bool _underVolume = false)
        {
            try
            {
                var pourtickets =
                    from P in data.Pours
                    join S in data.Pours on P.TagID equals S.TagID
                    join T in data.POSTicketItems on S.POSTicketItemID equals T.POSTicketItemID
                    where P.PourID != S.PourID &&
                          P.PourTime > S.PourTime &&
                          P.PourTime < System.Data.Objects.EntityFunctions.AddMilliseconds(S.PourTime, 2500) &&
                          P.DeviceID == S.DeviceID &&
                          P.LocationID == S.LocationID
                    select new ItemPour { pour = P, pourAddition = S, ticketItem = T };

                var tickets = pourtickets.ToList();

                foreach (var pourticket in tickets)
                {
                    if (pourticket.pour.POSTicketItemID == null)
                    {
                        pourticket.pour.POSTicketItemID = pourticket.ticketItem.POSTicketItemID;
                        pourticket.pour.Status |= (int)PosStatus.Combined;
                        if (0 != (pourticket.pour.Status & (int)PosStatus.Pending))
                        {
                            pourticket.pour.Status ^= (int)PosStatus.Pending;
                        }
                    }
                }

                data.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //JOIN Pours P2 ON P.TagID = P2.TagID AND P.PourID <> P2.PourID AND U.UPCID = P2.UPCID
        //	AND DATEADD( SECOND, 6, P2.PourTime ) > P.PourTime
        //	AND P2.PourTime < P.PourTime
        //	AND P.LocationID = P2.LocationID


        protected void ReconcileByManufacturerNoQuality(BeverageMonitorEntities data, int _interval, int _status, bool _overVolume = false, bool _underVolume = false)
        {
            try
            {
                var pourtickets =
                    from TI in data.POSTicketItems
                    join T in data.POSTickets on TI.POSTicketID equals T.POSTicketID
                    join A in data.TicketItemAliases on TI.Description equals A.Description
                    join R in data.Recipes on A.RecipeID equals R.RecipeID
                    join I in data.Ingredients on A.RecipeID equals I.RecipeID
                    join SP in data.StandardPours on I.StandardPourID equals SP.StandardPourID
                    join L in data.Locations on T.Establishment equals L.POSAlias
                    join U in data.UPCs on
                        new
                        {
                            ManufacturerID = (I.ManufacturerID.HasValue) ? I.ManufacturerID.Value : Guid.Empty
                        }
                        equals new { ManufacturerID = U.ManufacturerID }
                    join P in data.Pours on U.UPCID equals P.UPCID
                    where TI.Status == 2 &&
                          P.POSTicketItemID == null &&
                          P.PourTime < System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval) &&
                          P.PourTime > System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval * -1) &&
                          (_overVolume || P.Volume > (SP.PourStandard - (SP.PourStandard * SP.StandardVariance))) &&
                          (_underVolume || P.Volume < (SP.PourStandard + (SP.PourStandard * SP.StandardVariance))) &&
                          P.LocationID == L.LocationID
                    select new ItemPour { ticketItem = TI, pour = P, ingredient = I };

                ReconcileByWhatever(data, pourtickets, _status);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ReconcileByCategory(BeverageMonitorEntities data, int _interval, int _status, bool _overVolume = false, bool _underVolume = false)
        {
            try
            {
                var pourtickets =
                    from TI in data.POSTicketItems
                    join T in data.POSTickets on TI.POSTicketID equals T.POSTicketID
                    join A in data.TicketItemAliases on TI.Description equals A.Description
                    join R in data.Recipes on A.RecipeID equals R.RecipeID
                    join I in data.Ingredients on A.RecipeID equals I.RecipeID
                    join SP in data.StandardPours on I.StandardPourID equals SP.StandardPourID
                    join L in data.Locations on T.Establishment equals L.POSAlias
                    join U in data.UPCs on
                        new
                        {
                            CategoryID = (I.CategoryID.HasValue) ? I.CategoryID.Value : Guid.Empty,
                            Quality = (I.Quality.HasValue) ? I.Quality.Value : -1
                        }
                        equals new { CategoryID = U.CategoryID, Quality = U.Quality }
                    join P in data.Pours on U.UPCID equals P.UPCID
                    where TI.Status == 2 &&
                          P.POSTicketItemID == null &&
                          TI.Quantity == 1 &&
                          P.PourTime < System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval) &&
                          P.PourTime > System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval * -1) &&
                          (_overVolume || P.Volume > (SP.PourStandard - (SP.PourStandard * SP.StandardVariance))) &&
                          (_underVolume || P.Volume < (SP.PourStandard + (SP.PourStandard * SP.StandardVariance)))
                    select new ItemPour { ticketItem = TI, pour = P, ingredient = I };

                ReconcileByWhatever(data, pourtickets, _status);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ReconcileByCategoryNoQuality(BeverageMonitorEntities data, int _interval, int _status, bool _overVolume = false, bool _underVolume = false)
        {
            try
            {
                var pourtickets =
                    from TI in data.POSTicketItems
                    join T in data.POSTickets on TI.POSTicketID equals T.POSTicketID
                    join A in data.TicketItemAliases on TI.Description equals A.Description
                    join R in data.Recipes on A.RecipeID equals R.RecipeID
                    join I in data.Ingredients on A.RecipeID equals I.RecipeID
                    join SP in data.StandardPours on I.StandardPourID equals SP.StandardPourID
                    join L in data.Locations on T.Establishment equals L.POSAlias
                    join U in data.UPCs on
                        new
                        {
                            CategoryID = (I.CategoryID.HasValue) ? I.CategoryID.Value : Guid.Empty
                        }
                        equals new { CategoryID = U.CategoryID }
                    join P in data.Pours on U.UPCID equals P.UPCID
                    where TI.Status == 2 &&
                          P.POSTicketItemID == null &&
                          TI.Quantity == 1 &&
                          P.PourTime < System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval) &&
                          P.PourTime > System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval * -1) &&
                          (_overVolume || P.Volume > (SP.PourStandard - (SP.PourStandard * SP.StandardVariance))) &&
                          (_underVolume || P.Volume < (SP.PourStandard + (SP.PourStandard * SP.StandardVariance)))
                    select new ItemPour { ticketItem = TI, pour = P, ingredient = I };

                ReconcileByWhatever(data, pourtickets, _status);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ReconcileByUPC(BeverageMonitorEntities data, int _interval, int _status, bool _overVolume = false, bool _underVolume = false)
        {
            try
            {
                var pourtickets =
                    from TI in data.POSTicketItems
                    join T in data.POSTickets on TI.POSTicketID equals T.POSTicketID
                    join A in data.TicketItemAliases on TI.Description equals A.Description
                    join R in data.Recipes on A.RecipeID equals R.RecipeID
                    join I in data.Ingredients on A.RecipeID equals I.RecipeID
                    join SP in data.StandardPours on I.StandardPourID equals SP.StandardPourID
                    join L in data.Locations on T.Establishment equals L.POSAlias
                    join P in data.Pours on I.UPCID equals P.UPCID
                    where TI.Status == 2 &&
                          P.POSTicketItemID == null &&
                          TI.Quantity == 1 &&
                          P.PourTime < System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval) &&
                          P.PourTime > System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval * -1) &&
                          (_overVolume || P.Volume > (SP.PourStandard - (SP.PourStandard * SP.StandardVariance))) &&
                          (_underVolume || P.Volume < (SP.PourStandard + (SP.PourStandard * SP.StandardVariance)))
                    select new ItemPour { ticketItem = TI, pour = P, ingredient = I };

                ReconcileByWhatever(data, pourtickets, _status);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ReconcileByUPCByCategory(BeverageMonitorEntities data, int _interval, int _status, bool _overVolume = false, bool _underVolume = false)
        {
            try
            {
                var pourtickets =
                    from TI in data.POSTicketItems
                    join T in data.POSTickets on TI.POSTicketID equals T.POSTicketID
                    join A in data.TicketItemAliases on TI.Description equals A.Description
                    join R in data.Recipes on A.RecipeID equals R.RecipeID
                    join I in data.Ingredients on A.RecipeID equals I.RecipeID
                    join SP in data.StandardPours on I.StandardPourID equals SP.StandardPourID
                    join L in data.Locations on T.Establishment equals L.POSAlias
                    join U in data.UPCs on I.UPCID equals U.UPCID
                    join U2 in data.UPCs on U.CategoryID equals U2.CategoryID
                    join P in data.Pours on U2.UPCID equals P.UPCID
                    where TI.Status == 2 &&
                          P.POSTicketItemID == null &&
                          TI.Quantity == 1 &&
                          P.PourTime < System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval) &&
                          P.PourTime > System.Data.Objects.EntityFunctions.AddMinutes(T.TicketDate, _interval * -1) &&
                          (_overVolume || P.Volume > (SP.PourStandard - (SP.PourStandard * SP.StandardVariance))) &&
                          (_underVolume || P.Volume < (SP.PourStandard + (SP.PourStandard * SP.StandardVariance)))
                    select new ItemPour { ticketItem = TI, pour = P, ingredient = I };

                ReconcileByWhatever(data, pourtickets, _status);
            }
            catch (Exception)
            {

                throw;
            }
        }





        public void ReconcileNow(List<IPour> _pours, IPOSTicketItem _ticketItem)
        {
            try
            {
                using (var data = new BeverageMonitorEntities())
                {
                    // Only process TicketItems that are passed in
                    Log.Time("POS - Reconcile Now (First)", LogType.Debug, true, () =>
                    {
                        var Tickets = data.POSTicketItems.Where(T => T.POSTicketID == _ticketItem.POSTicketID).ToList();
                        //string Establishment = null;
                        //if (null != Tickets && 0 != Tickets.Count())
                        //{
                        //    Establishment = Tickets.First().Establishment;
                        //}

                        var alias = data.TicketItemAliases.Where(A => A.Description == _ticketItem.Description).ToList();
                        {
                            if (!alias.Any())
                            {
                                _ticketItem.ItemStatus |= PosStatus.UnknownAlias;
                                _ticketItem.ItemStatus |= PosStatus.Alert;
                                //tiManager.Save(_ticketItem, out result);
                            }
                            else
                            {
                                foreach (TicketItemAlias itemAlias in alias)
                                {
                                    var recipes = data.Recipes.Where(R => R.RecipeID == itemAlias.RecipeID);
                                    if (!recipes.Any())
                                    {
                                        _ticketItem.ItemStatus |= PosStatus.UnknownRecipe;
                                        _ticketItem.ItemStatus |= PosStatus.Alert;
                                        //tiManager.Save(_ticketItem, out result);
                                    }
                                    else
                                    {
                                        bool match = false;
                                        foreach (var R in recipes)
                                        {
                                            // MLF do we need to verify that _Pours match the recipe?
                                            match = true;
                                        }
                                        if (true == match)
                                        {
                                            _ticketItem.Reconciled += 1;
                                            if (_ticketItem.Reconciled == _ticketItem.Quantity)
                                            {
                                                if (0 != (_ticketItem.ItemStatus & PosStatus.Pending))
                                                {
                                                    _ticketItem.ItemStatus ^= PosStatus.Pending;
                                                }
                                                _ticketItem.ItemStatus |= PosStatus.Complete;
                                            }
                                            else
                                            {
                                                _ticketItem.ItemStatus |= PosStatus.Pending;
                                            }
                                            //tiManager.Save(_ticketItem, out result);
                                            foreach (IPour P in _pours)
                                            {
                                                P.POSTicketItemID = _ticketItem.ObjectID;
                                                //DataManagerFactory.Get().Manage<IPour>().Save(P, out result);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        data.SaveChanges();
                    });
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("Reconcile::ReconcileNow(Pours,TickItem", exp);
            }
        }

        public void ReconcileNow(IngredientContainerTypes _ingredientCat, bool _ignorePourSize, bool _substitutions)
        {
            try
            {
                using (var data = new BeverageMonitorEntities())
                {
                    //var allRecipes = data.Recipes.ToList();

                    // Look for TicketItems that have not been reconciled
                    var ticketItems = data.POSTicketItems.Where(I => I.Reconciled != I.Quantity && 0 == (I.Status & (int)PosStatus.Alert)).ToList();
                    //var allTickets = data.POSTickets.ToList();

                    //                    foreach (var ticketItem in ticketItems)
                    for (int i = 0; i < ticketItems.Count; ++i)
                    {
                        Log.Time("POS - Reconcile Now (second)", LogType.Debug, true, () =>
                        {
                            var ticketItem = ticketItems[i];
                            var tickets = data.POSTickets.FirstOrDefault(T => T.POSTicketID == ticketItem.POSTicketID);
                            string establishment = null;
                            if (null != tickets)
                            {
                                establishment = tickets.Establishment;
                            }

                            var alias = data.TicketItemAliases.Where(A => A.Description == ticketItem.Description).ToList();
                            {
                                if (!alias.Any())
                                {
                                    ticketItem.Status |= (int)PosStatus.UnknownAlias;
                                    ticketItem.Status |= (int)PosStatus.Alert;
                                    //tiManager.Save(ticketItem, out result);
                                }
                                else
                                {
                                    foreach (var itemAlias in alias)
                                    {
                                        var recipes = data.Recipes.Where(R => R.RecipeID == itemAlias.RecipeID).ToList();
                                        if (!recipes.Any())
                                        {
                                            ticketItem.Status |= (int)PosStatus.UnknownAlias;
                                            ticketItem.Status |= (int)PosStatus.Alert;
                                            //tiManager.Save(ticketItem, out result);
                                        }
                                        else
                                        {
                                            foreach (var recipe in recipes)
                                            {
                                                ProcessRecipe(recipe, ticketItem, establishment, _ingredientCat, _ignorePourSize, _substitutions);
                                            }
                                        }
                                    }
                                }
                            }
                        });
                    }
                    data.SaveChanges();
                    ticketItems.Clear();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("", "Reconcile::ReconcileNow()", exp);
            }
        }

        private void ProcessRecipe(Recipe _recipe, POSTicketItem _ticketItem, string _establishment, IngredientContainerTypes _ingredientCat, bool _ignorePourSize, bool _substitutions)
        {
            var pours = new List<IPour>();
            bool found = false;

            try
            {
                Log.Time("POS - Process Recipe", LogType.Debug, true, () =>
                {
                    using (var data = new BeverageMonitorEntities())
                    {
                        // Process Required ingredients
                        IList<Ingredient> Ingredients = _recipe.Ingredients.Where(i => i.Type == (int)IngredientRequirementType.Required).ToList();
                        // GetIngredients(_recipe, IngredientRequirementType.Required, _ingredientCat);
                        if (Ingredients.Any())
                        {
                            found = ProcessIngredients(_ticketItem, _establishment, Ingredients, pours, false,
                                                       _ignorePourSize, _substitutions);
                        }
                        else
                        {
                            found = true; // no ingredients of this type to find
                        }
                        if (true == found)
                        {
                            // Process one-of ingredients
                            Ingredients =
                                _recipe.Ingredients.Where(i => i.Type == (int)IngredientRequirementType.OneOf).ToList();
                            //GetIngredients(_recipe, IngredientRequirementType.OneOf, _ingredientCat);
                            if (Ingredients.Any())
                            {
                                found = ProcessIngredients(_ticketItem, _establishment, Ingredients, pours, false,
                                                           _ignorePourSize, _substitutions);
                            }
                            else
                            {
                                found = true; // no ingredients of this type to find
                            }
                            if (true == found)
                            {
#warning MLF may want to move this out of the main loop... process all Required and OneOf stuff first the look for any optionals
                                // Process opional ingredients
                                Ingredients =
                                    _recipe.Ingredients.Where(i => i.Type == (int)IngredientRequirementType.Optional).
                                        ToList();
                                //GetIngredients(_recipe, IngredientRequirementType.Optional, _ingredientCat);
                                if (Ingredients.Any())
                                {
                                    found = ProcessIngredients(_ticketItem, _establishment, Ingredients, pours, true,
                                                               _ignorePourSize, _substitutions);
                                }
                            }
                        }
                        if (true == found)
                        {
                            if (0 < pours.Count)
                            {
                                // Found all pours needed to complete this TicketItem
                                _ticketItem.Reconciled += 1;
                                if (_ticketItem.Reconciled == _ticketItem.Quantity)
                                {
                                    if (0 != (_ticketItem.Status & (int)PosStatus.Pending))
                                    {
                                        _ticketItem.Status ^= (int)PosStatus.Pending;
                                    }
                                    _ticketItem.Status |= (int)PosStatus.Complete;
                                }
                                else
                                {
                                    _ticketItem.Status |= (int)PosStatus.Pending;
                                }
                                //DataManagerFactory.Get().Manage<IPOSTicketItem>().Save(_ticketItem, out result);
                                foreach (IPour pour in pours)
                                {
                                    data.Attach(pour);
                                    pour.POSTicketItemID = _ticketItem.POSTicketItemID;

                                    //DataManagerFactory.Get().Manage<IPour>().Save(pour, out result);
                                }
                                pours.Clear();
                            }
                            found = false;
                        }
                        data.SaveChanges();
                    }
                });
            }
            catch (Exception exp)
            {
                Log.WriteException("Reconcile::ProcessRecipe", exp);
            }
        }


        private bool ProcessIngredients(POSTicketItem _ticketItem, string _establishment, IList<Ingredient> _ingredients,
            List<IPour> _pours, bool _opional, bool _ignorePourSize, bool _substitutions)
        {
            var foundPours = new List<IPour>();
            var ingredientNumbers = new List<int>();
            var foundNumbers = new List<int>();
            bool rc = false;

            try
            {
                Log.Time("POS - Process Ingredients", LogType.Debug, true, () =>
                {
                    using (var data = new BeverageMonitorEntities())
                    {
                        // We will only look at pours from the matching POS location
                        IList<Location> locations = data.Locations.Where(L => L.POSAlias == _establishment).ToList();
                        if (locations.Any())
                        {
                            foreach (var location in locations)
                            {
                                foreach (var ingredient in _ingredients)
                                {
                                    if (false == ingredientNumbers.Contains(ingredient.Number))
                                    {
                                        ingredientNumbers.Add(ingredient.Number);
                                    }

                                    var pours = GetMatchingPours(_ticketItem, location, ingredient, _ignorePourSize,
                                                                 _substitutions);

                                    if (null != pours && pours.Any() &&
                                        false == foundNumbers.Contains(ingredient.Number))
                                    {
                                        foundNumbers.Add(ingredient.Number);
                                        // We only need to find one Ingredient of each number/type
                                        foundPours.AddRange(pours);
                                    }
                                }
                            }
                        }

                        if (ingredientNumbers.Count == foundNumbers.Count || true == _opional)
                        // We found one of each or just looking for any optional ingredients
                        {
                            _pours.AddRange(foundPours);
                            rc = true;
                        }
                        data.SaveChanges();
                    }
                });
            }
            catch (Exception exp)
            {
                Log.WriteException("Reconcile::ProcessIngredients", exp);
            }

            return rc;
        }



        private List<IPour> GetMatchingPours(POSTicketItem _ticketItem, Location _location, Ingredient _ingredient, bool _ignorePourSize, bool _substitutions)
        {
            return Log.Time<List<IPour>>("POS Get Matching Pours", LogType.Debug, true, () =>
            {
                var matchPours = new List<IPour>();
                using (var data = new BeverageMonitorEntities())
                {
                    var pours = new List<IPour>();
                    var UPCs = new List<UPC>();

                    var sPour = data.StandardPours.FirstOrDefault(p => p.StandardPourID == _ingredient.StandardPourID);

                    // Look for Ingredients that m atch the UPC
                    LookupIngredientsByUPC(data, _ingredient, _location, UPCs, pours);

                    pours = this.MatchPours(data, _ingredient, _location, pours, _substitutions, _ticketItem);

                    // Match up splash/top off pours with main pour
                    CombineTopOffWithPour(pours, _ticketItem, sPour, matchPours, _ignorePourSize);
                }
                return matchPours;
            });
        }

        private List<IPour> MatchPours(BeverageMonitorEntities _data, Ingredient _ingredient, Location _location, List<IPour> _pours, bool _substitutions, POSTicketItem _ticketItem)
        {
            return Log.Time<List<IPour>>("POS Match Pours", LogType.Debug, true, () =>
            {
                var upcItems = new List<UPC>();
                if (null == _pours || !_pours.Any() || true == _substitutions)
                {
                    var categoryId = Guid.Empty;
                    if (_ingredient.UPCID.HasValue)
                    {
                        upcItems.Add(_data.UPCs.FirstOrDefault(u => u.UPCID == _ingredient.UPCID.Value));
                        //var upc = _data.UPCs.FirstOrDefault( u => u.UPCID == _ingredient.UPCID.Value );
                        //categoryId = upc.CategoryID;
                    }
                    else if (_ingredient.CategoryID.HasValue)
                    {
                        upcItems = _data.UPCs.Where(U => U.CategoryID == categoryId).ToList();
                        //                        categoryId = _ingredient.CategoryID.Value;
                    }
                    else if (_ingredient.ManufacturerID.HasValue)
                    {
                        upcItems = _data.UPCs.Where(U => U.ManufacturerID == _ingredient.ManufacturerID).ToList();
                    }
                    //else if (Convert.ToInt32(IngredientContainerTypes.Manufacturer) == _Ingredient.FIDType)
                    //{
                    //    UPCs = UPCman.GetAll().Where(U => U.ManufacturerID == _Ingredient.FID &&
                    //                                        U.Quality == _Ingredient.Quality).ToList();
                    //}
                    //upcItems = _data.UPCs.Where(U => U.CategoryID == categoryId).ToList();
                    //if (Guid.Empty != categoryId)
                    {
                        var i = 0;
                        if (_pours != null)
                        {
                            var allAvailablePours = _data.Pours.Where(P => P.POSTicketItemID == null &&
                                                                             P.Alerted == false &&
                                                                             P.LocationID == _location.LocationID).ToList();

                            while (i < upcItems.Count() &&
                                   !_pours.Any())
                            {
                                _pours = allAvailablePours.Where(p => p.UPCID == upcItems[i].UPCID).ToList();
                                i++;
                            }
                            if (0 != _pours.Count())
                            {
                                _ticketItem.Status |= (int)PosStatus.Substitution;
                            }
                        }
                    }
                }
                return _pours;

            });
        }

        private void CombineTopOffWithPour(List<IPour> _pours, POSTicketItem _ticketItem, StandardPour _sPour, List<IPour> _matchPours, bool _ignorePourSize)
        {
            Log.Time("POS Combine TopOff with Pour", LogType.Debug, true, () =>
            {
                if (null != _sPour &&
                     null != _pours && 0 != _pours.Count())
                {
                    _pours = _pours.OrderByDescending(P => P.Volume).ToList();
                    int i = 0;
                    while (!_matchPours.Any() &&
                            i < _pours.Count())
                    {
                        var splash = _pours.Where(P => P.PourID != _pours[i].PourID &&
                                                  P.PourTime > _pours[i].PourTime &&
                                                  P.PourTime < _pours[i].PourTime.AddSeconds(MaxSplashTime) &&
                                                  P.Volume < MaxSplash).ToList();
                        if (0 != splash.Count())
                        {
                            if ((_pours[i].Volume + splash.First().Volume) > (_sPour.PourStandard - (_sPour.PourStandard * _sPour.StandardVariance)) &&
                                (_pours[i].Volume + splash.First().Volume) < (_sPour.PourStandard + (_sPour.PourStandard * _sPour.StandardVariance)))
                            {
                                _matchPours.Add(_pours[i]);
                                _matchPours.Add(splash.First());
                            }
                            else if (true == _ignorePourSize)
                            {
                                if ((_pours[i].Volume + splash.First().Volume) > (_sPour.PourStandard - (_sPour.PourStandard * _sPour.StandardVariance)))
                                {
                                    _ticketItem.Status |= (int)PosStatus.OverPour;
                                }
                                else
                                {
                                    _ticketItem.Status |= (int)PosStatus.UnderPour;
                                }
                                _matchPours.Add(_pours[i]);
                                _matchPours.Add(splash.First());
                            }
                        }
                        else // No splash pours for this pour
                        {
                            if (_pours[i].Volume > _sPour.PourStandard - (_sPour.PourStandard * _sPour.StandardVariance) &&
                                _pours[i].Volume < _sPour.PourStandard + (_sPour.PourStandard * _sPour.StandardVariance))
                            {
                                _matchPours.Add(_pours[i]);
                            }
                            else if (true == _ignorePourSize)
                            {
                                if (_pours[i].Volume > _sPour.PourStandard - (_sPour.PourStandard * _sPour.StandardVariance))
                                {
                                    _ticketItem.Status |= (int)PosStatus.OverPour;
                                }
                                else
                                {
                                    _ticketItem.Status |= (int)PosStatus.UnderPour;
                                }
                                _matchPours.Add(_pours[i]);
                            }
                        }
                        i++;
                    }
                }
            });
        }

        private void LookupIngredientsByUPC(BeverageMonitorEntities _data, Ingredient _ingredient, Location _location, List<UPC> _upcs, List<IPour> _pours)
        {
            Log.Time("POS - Process Ticket Item", LogType.Debug, true, () =>
            {
                if (_ingredient.UPCID.HasValue)
                {
                    _pours.AddRange(_data.Pours.Where(P => P.POSTicketItemID == null &&
                                                    P.Alerted == false &&
                                                    P.LocationID == _location.LocationID &&
                                                    P.UPCID == _ingredient.UPCID.Value).ToList());
                }
                else
                {
                    // Look for Ingredients that match the manufacturer and quality
                    if (_ingredient.ManufacturerID.HasValue || _ingredient.CategoryID.HasValue)
                    {
                        _upcs.AddRange(_data.UPCs.Where(U => U.ManufacturerID == _ingredient.ManufacturerID.Value &&
                            //U.CategoryID == _ingredient.CategoryID.Value &&
                                                          U.Quality == _ingredient.Quality).ToList());
                    }
                    // Look for Ingredients that match the Sub cat and quality
                    else if (_ingredient.CategoryID.HasValue)
                    {
                        _upcs.AddRange(_data.UPCs.Where(U => U.CategoryID == _ingredient.CategoryID.Value &&
                                                          U.Quality == _ingredient.Quality).ToList());
                    }
                    if (null != _upcs)
                    {
                        int i = 0;
                        while (i < _upcs.Count() &&
                               !_pours.Any())
                        {
                            var upcid = _upcs[i].UPCID;
                            _pours.AddRange(_data.Pours.Where(P => P.POSTicketItemID == null &&
                                                            P.Alerted == false &&
                                                            P.LocationID == _location.LocationID &&
                                                            P.UPCID == upcid).ToList());
                            i++;
                        }
                    }
                }
            });
        }

        //private IList<IIngredient> GetIngredients(IRecipe _recipe, IngredientRequirementType _type, IngredientContainerTypes _ingredientCat)
        //{
        //    IQueryable<IIngredient> ingredients = null;
        //    if (IngredientContainerTypes.UPC == _ingredientCat)
        //    {
        //        ingredients = DataManagerFactory.Get().Manage<IIngredient>().GetAll().Where(I => I.UPCID != null &&
        //                                                                                         I.RecipeID == _recipe.ObjectID &&
        //                                                                                         I.Type == Convert.ToInt32(_type));
        //    }
        //    else if (IngredientContainerTypes.Category == _ingredientCat)
        //    {
        //        ingredients = DataManagerFactory.Get().Manage<IIngredient>().GetAll().Where(I => I.CategoryID != null &&
        //                                                                                         I.RecipeID == _recipe.ObjectID &&
        //                                                                                         I.Type == Convert.ToInt32(_type));
        //    }
        //    else // (IngredientContainerTypes.Manufacturer == _IngredientCat)
        //    {
        //        ingredients = DataManagerFactory.Get().Manage<IIngredient>().GetAll().Where(I => I.ManufacturerID != null &&
        //                                                                                         I.RecipeID == _recipe.ObjectID &&
        //                                                                                         I.Type == Convert.ToInt32(_type));
        //    }

        //    return ingredients.ToList();
        //}

    }




//    public class Reconcile
//    {
//        private const double MaxSplash = 5; // MLF this needs to be a Admin setting
//        private const int MaxSplashTime = 15; // MLF this needs to be a Admin setting

//        private List<IPour> GetMatchingPours(IPOSTicketItem _ticketItem, ILocation _location, IIngredient _ingredient, bool _ignorePourSize, bool _substitutions )
//        {
//            return Log.Time<List<IPour>>("POS Get Matching Pours", LogType.Debug, true, () =>
//            {
//                var pours = new List<IPour>();
//                var matchPours = new List<IPour>();
//                List<IUPCItem> UPCs = null;
//                var man = DataManagerFactory.Get().Manage<IPour>();
//                var upcMan = DataManagerFactory.Get().Manage<IUPCItem>();

//                var sPour = DataManagerFactory.Get().Manage<IStandardPour>().Get(_ingredient.StandardPourID);

//                // Look for Ingredients that match the UPC
//                upcMan = LookupIngredientsByUPC(_ingredient, man, _location, upcMan, UPCs, ref pours);

//                pours = this.MatchPours(_ingredient, man, _location, upcMan, pours, _substitutions, _ticketItem);

//                // Match up splash/top off pours with main pour
//                CombineTopOffWithPour(pours, _ticketItem, sPour, matchPours, _ignorePourSize);

//                return matchPours;
//            });
//        }

//        private List<IPour> MatchPours(IIngredient _ingredient, IDataManager<IPour> _man, ILocation _location, IDataManager<IUPCItem> _upcMan, List<IPour> _pours, bool _substitutions, IPOSTicketItem _ticketItem)
//        {
//            return Log.Time<List<IPour>>("POS Match Pours", LogType.Debug, true, () =>
//            {
//                List<IUPCItem> upcItems;
//                if (null == _pours || 0 == _pours.Count() || true == _substitutions)
//                {
//                    var categoryId = Guid.Empty;
//                    if (_ingredient.UPCID.HasValue)
//                    {
//                        var upc = _upcMan.Get(_ingredient.UPCID.Value);
//                        categoryId = upc.CategoryID;
//                    }
//                    else if (_ingredient.CategoryID.HasValue)
//                    {
//                        categoryId = _ingredient.CategoryID.Value;
//                    }
//                    //else if (Convert.ToInt32(IngredientContainerTypes.Manufacturer) == _Ingredient.FIDType)
//                    //{
//                    //    UPCs = UPCman.GetAll().Where(U => U.ManufacturerID == _Ingredient.FID &&
//                    //                                        U.Quality == _Ingredient.Quality).ToList();
//                    //}
//                    upcItems = _upcMan.GetAll().Where(U => U.CategoryID == categoryId).ToList();
//                    if (Guid.Empty != categoryId)
//                    {
//                        var i = 0;
//                        if (_pours != null)
//                        {
//                            var allAvailablePours = _man.GetAll().Where(P => P.POSTicketItemID == null &&
//                                                                             P.Alerted == false &&
//                                                                             P.LocationID == _location.LocationID).ToList();

//                            while (i < upcItems.Count() &&
//                                   0 == _pours.Count())
//                            {
//                                _pours = allAvailablePours.Where( p => p.UPCID == upcItems[i].UPCID).ToList();
//                                i++;
//                            }
//                            if (0 != _pours.Count())
//                            {
//                                _ticketItem.ItemStatus |= PosStatus.Substitution;
//                            }
//                        }
//                    }
//                }
//                return _pours;
                                                                                                   
//            });
//        }

//        private void CombineTopOffWithPour(List<IPour> _pours, IPOSTicketItem _ticketItem, IStandardPour _sPour, List<IPour> _matchPours, bool _ignorePourSize)
//        {
//            Log.Time("POS Combine TopOff with Pour", LogType.Debug, true, () =>
//            {
//                if (null != _sPour &&
//                     null != _pours && 0 != _pours.Count())
//                {
//                    _pours = _pours.OrderByDescending(P => P.Volume).ToList();
//                    int i = 0;
//                    while (0 == _matchPours.Count() &&
//                            i < _pours.Count())
//                    {
//                        var splash = _pours.Where(P => P.PourID != _pours[i].PourID &&
//                                                  P.PourTime > _pours[i].PourTime &&
//                                                  P.PourTime < _pours[i].PourTime.AddSeconds(MaxSplashTime) &&
//                                                  P.Volume < MaxSplash).ToList();
//                        if (0 != splash.Count())
//                        {
//                            if ((_pours[i].Volume + splash.First().Volume) > (_sPour.PourStandard - (_sPour.PourStandard * _sPour.StandardVariance)) &&
//                                (_pours[i].Volume + splash.First().Volume) < (_sPour.PourStandard + (_sPour.PourStandard * _sPour.StandardVariance)))
//                            {
//                                _matchPours.Add(_pours[i]);
//                                _matchPours.Add(splash.First());
//                            }
//                            else if (true == _ignorePourSize)
//                            {
//                                if ((_pours[i].Volume + splash.First().Volume) > (_sPour.PourStandard - (_sPour.PourStandard * _sPour.StandardVariance)))
//                                {
//                                    _ticketItem.ItemStatus |= PosStatus.OverPour;
//                                }
//                                else
//                                {
//                                    _ticketItem.ItemStatus |= PosStatus.UnderPour;
//                                }
//                                _matchPours.Add(_pours[i]);
//                                _matchPours.Add(splash.First());
//                            }
//                        }
//                        else // No splash pours for this pour
//                        {
//                            if (_pours[i].Volume > _sPour.PourStandard - (_sPour.PourStandard * _sPour.StandardVariance) &&
//                                _pours[i].Volume < _sPour.PourStandard + (_sPour.PourStandard * _sPour.StandardVariance))
//                            {
//                                _matchPours.Add(_pours[i]);
//                            }
//                            else if (true == _ignorePourSize)
//                            {
//                                if (_pours[i].Volume > _sPour.PourStandard - (_sPour.PourStandard * _sPour.StandardVariance))
//                                {
//                                    _ticketItem.ItemStatus |= PosStatus.OverPour;
//                                }
//                                else
//                                {
//                                    _ticketItem.ItemStatus |= PosStatus.UnderPour;
//                                }
//                                _matchPours.Add(_pours[i]);
//                            }
//                        }
//                        i++;
//                    }
//                }
//            });
//        }

//        private IDataManager<IUPCItem> LookupIngredientsByUPC(IIngredient _ingredient, IDataManager<IPour> _man, ILocation _location, IDataManager<IUPCItem> _upcMan, List<IUPCItem> _upcs, ref List<IPour> _pours)
//        {
//            var pours = _pours;
//            Log.Time("POS - Process Ticket Item", LogType.Debug, true, () =>
//            {
//                if (_ingredient.UPCID.HasValue)
//                {
//                    pours = _man.GetAll().Where(P => P.POSTicketItemID == null &&
//                                                    P.Alerted == false &&
//                                                    P.LocationID == _location.LocationID &&
//                                                    P.UPCID == _ingredient.UPCID.Value).ToList();
//                }
//                else
//                {
//                    _upcMan = DataManagerFactory.Get().Manage<IUPCItem>();
//                    // Look for Ingredients that match the manufacturer and quality
//                    if (_ingredient.ManufacturerID.HasValue || _ingredient.CategoryID.HasValue)
//                    {
//                        _upcs = _upcMan.GetAll().Where(U => U.ManufacturerID == _ingredient.ManufacturerID.Value &&
//                                                          U.CategoryID == _ingredient.CategoryID.Value &&
//                                                          U.Quality == _ingredient.Quality).ToList();
//                    }
//                    // Look for Ingredients that match the Sub cat and quality
//                    else if (_ingredient.CategoryID.HasValue)
//                    {
//                        _upcs = _upcMan.GetAll().Where(U => U.CategoryID == _ingredient.CategoryID.Value &&
//                                                          U.Quality == _ingredient.Quality).ToList();
//                    }
//                    if (null != _upcs)
//                    {
//                        int i = 0;
//                        while (i < _upcs.Count() &&
//                               0 == pours.Count())
//                        {
//                            pours = _man.GetAll().Where(P => P.POSTicketItemID == null &&
//                                                            P.Alerted == false &&
//                                                            P.LocationID == _location.LocationID &&
//                                                            P.UPCID == _upcs[i].UPCID).ToList();
//                            i++;
//                        }
//                    }
//                }
//            });
//            _pours = pours;
//            return _upcMan;
//        }

//        private bool ProcessIngredients(IPOSTicketItem _ticketItem, string _establishment, IList<IIngredient> _ingredients,
//            List<IPour> _pours, bool _opional, bool _ignorePourSize, bool _substitutions)
//        {
//            var foundPours = new List<IPour>();
//            var ingredientNumbers = new List<int>();
//            var foundNumbers = new List<int>();
//            bool rc = false;

//            try
//            {
//                Log.Time("POS - Process Ingredients", LogType.Debug, true, () =>
//                {
//                    // We will only look at pours from the matching POS location
//                    IList<ILocation> locations = DataManagerFactory.Get().Manage<ILocation>().GetAll().Where(L => L.POSAlias == _establishment).ToList();
//                    if (0 < locations.Count())
//                    {
//                        foreach (var location in locations)
//                        {
//                            foreach (var ingredient in _ingredients)
//                            {
//                                if (false == ingredientNumbers.Contains(ingredient.Number))
//                                {
//                                    ingredientNumbers.Add(ingredient.Number);
//                                }

//                                var pours = GetMatchingPours(_ticketItem, location, ingredient, _ignorePourSize, _substitutions);

//                                if (null != pours && 0 < pours.Count() && false == foundNumbers.Contains(ingredient.Number))
//                                {
//                                    foundNumbers.Add(ingredient.Number); // We only need to find one Ingredient of each number/type
//                                    foreach (IPour P in pours)
//                                    {
//                                        foundPours.Add(P);
//                                    }
//                                }
//                            }
//                        }
//                    }

//                    if (ingredientNumbers.Count == foundNumbers.Count || true == _opional) // We found one of each or just looking for any optional ingredients
//                    {
//                        foreach (var pour in foundPours)
//                        {
//                            _pours.Add(pour);
//                        }
//                        rc = true;
//                    }
//                });
//            }
//            catch (Exception exp)
//            {
//                Log.WriteException("Reconcile::ProcessIngredients", exp);
//            }

//            return rc;
//        }

//        private IList<IIngredient> GetIngredients(IRecipe _recipe, IngredientRequirementType _type, IngredientContainerTypes _ingredientCat)
//        {
//            IQueryable<IIngredient> ingredients = null;
//            if (IngredientContainerTypes.UPC == _ingredientCat)
//            {
//                ingredients = DataManagerFactory.Get().Manage<IIngredient>().GetAll().Where(I => I.UPCID != null &&
//                                                                                                 I.RecipeID == _recipe.ObjectID &&
//                                                                                                 I.Type == Convert.ToInt32(_type));
//            }
//            else if (IngredientContainerTypes.Category == _ingredientCat)
//            {
//                ingredients = DataManagerFactory.Get().Manage<IIngredient>().GetAll().Where(I => I.CategoryID != null &&
//                                                                                                 I.RecipeID == _recipe.ObjectID &&
//                                                                                                 I.Type == Convert.ToInt32(_type));
//            }
//            else // (IngredientContainerTypes.Manufacturer == _IngredientCat)
//            {
//                ingredients = DataManagerFactory.Get().Manage<IIngredient>().GetAll().Where(I => I.ManufacturerID != null &&
//                                                                                                 I.RecipeID == _recipe.ObjectID &&
//                                                                                                 I.Type == Convert.ToInt32(_type));
//            }

//            return ingredients.ToList();
//        }

//        private void ProcessRecipe(IRecipe _recipe, IPOSTicketItem _ticketItem, string _establishment, IngredientContainerTypes _ingredientCat, bool _ignorePourSize, bool _substitutions)
//        {
//            var pours = new List<IPour>();
//            bool found = false;

//            try
//            {
//                Log.Time("POS - Process Recipe", LogType.Debug, true, () =>
//                {
//                    // Process Required ingredients
//                    IList<IIngredient> Ingredients = _recipe.Ingredients.Where(i => i.Type == (int)IngredientRequirementType.Required).ToList();// GetIngredients(_recipe, IngredientRequirementType.Required, _ingredientCat);
//                    if (0 < Ingredients.Count())
//                    {
//                        found = ProcessIngredients(_ticketItem, _establishment, Ingredients, pours, false, _ignorePourSize, _substitutions);
//                    }
//                    else
//                    {
//                        found = true; // no ingredients of this type to find
//                    }
//                    if (true == found)
//                    {
//                        // Process one-of ingredients
//                        Ingredients = _recipe.Ingredients.Where(i => i.Type == (int)IngredientRequirementType.OneOf).ToList();//GetIngredients(_recipe, IngredientRequirementType.OneOf, _ingredientCat);
//                        if (0 < Ingredients.Count())
//                        {
//                            found = ProcessIngredients(_ticketItem, _establishment, Ingredients, pours, false, _ignorePourSize, _substitutions);
//                        }
//                        else
//                        {
//                            found = true; // no ingredients of this type to find
//                        }
//                        if (true == found)
//                        {
//#warning MLF may want to move this out of the main loop... process all Required and OneOf stuff first the look for any optionals
//                            // Process opional ingredients
//                            Ingredients = _recipe.Ingredients.Where(i => i.Type == (int)IngredientRequirementType.Optional).ToList();//GetIngredients(_recipe, IngredientRequirementType.Optional, _ingredientCat);
//                            if (0 < Ingredients.Count())
//                            {
//                                found = ProcessIngredients(_ticketItem, _establishment, Ingredients, pours, true, _ignorePourSize, _substitutions);
//                            }
//                        }
//                    }
//                    if (true == found)
//                    {
//                        if (0 < pours.Count)
//                        {
//                            // Found all pours needed to complete this TicketItem
//                            _ticketItem.Reconciled += 1;
//                            if (_ticketItem.Reconciled == _ticketItem.Quantity)
//                            {
//                                if (0 != (_ticketItem.ItemStatus & PosStatus.Pending))
//                                {
//                                    _ticketItem.ItemStatus ^= PosStatus.Pending;
//                                }
//                                _ticketItem.ItemStatus |= PosStatus.Complete;
//                            }
//                            else
//                            {
//                                _ticketItem.ItemStatus |= PosStatus.Pending;
//                            }
//                            IList<string> result;
//                            DataManagerFactory.Get().Manage<IPOSTicketItem>().Save(_ticketItem, out result);
//                            foreach (IPour pour in pours)
//                            {
//                                pour.POSTicketItemID = _ticketItem.ObjectID;
//                                DataManagerFactory.Get().Manage<IPour>().Save(pour, out result);
//                            }
//                            pours.Clear();
//                        }
//                        found = false;
//                    }
//                });
//            }
//            catch (Exception exp)
//            {
//                Log.WriteException("Reconcile::ProcessRecipe", exp);
//            }
//        }

//        public void ReconcileNow(List<IPour> _pours, IPOSTicketItem _ticketItem)
//        {
//            try
//            {
//                // Only process TicketItems that are passed in
//                Log.Time("POS - Reconcile Now (First)", LogType.Debug, true, () =>
//                {
//                    var tiManager = DataManagerFactory.Get().Manage<IPOSTicketItem>();
//                    var Tickets = DataManagerFactory.Get().Manage<IPOSTicket>().GetAll().Where(T => T.POSTicketID == _ticketItem.POSTicketID);
//                    //string Establishment = null;
//                    //if (null != Tickets && 0 != Tickets.Count())
//                    //{
//                    //    Establishment = Tickets.First().Establishment;
//                    //}

//                    var alias = DataManagerFactory.Get().Manage<ITicketItemAlias>().GetAll().Where(A => A.Description == _ticketItem.Description);
//                    {
//                        IList<string> result;
//                        if (0 == alias.Count())
//                        {
//                            _ticketItem.ItemStatus |= PosStatus.UnknownAlias;
//                            _ticketItem.ItemStatus |= PosStatus.Alert;
//                            tiManager.Save(_ticketItem, out result);
//                        }
//                        else
//                        {
//                            foreach (ITicketItemAlias itemAlias in alias)
//                            {
//                                var recipes = DataManagerFactory.Get().Manage<IRecipe>().GetAll().Where(R => R.ObjectID == itemAlias.RecipeID);
//                                if (0 == recipes.Count())
//                                {
//                                    _ticketItem.ItemStatus |= PosStatus.UnknownRecipe;
//                                    _ticketItem.ItemStatus |= PosStatus.Alert;
//                                    tiManager.Save(_ticketItem, out result);
//                                }
//                                else
//                                {
//                                    bool match = false;
//                                    foreach (var R in recipes)
//                                    {
//                                        // MLF do we need to verify that _Pours match the recipe?
//                                        match = true;
//                                    }
//                                    if (true == match)
//                                    {
//                                        _ticketItem.Reconciled += 1;
//                                        if (_ticketItem.Reconciled == _ticketItem.Quantity)
//                                        {
//                                            if (0 != (_ticketItem.ItemStatus & PosStatus.Pending))
//                                            {
//                                                _ticketItem.ItemStatus ^= PosStatus.Pending;
//                                            }
//                                            _ticketItem.ItemStatus |= PosStatus.Complete;
//                                        }
//                                        else
//                                        {
//                                            _ticketItem.ItemStatus |= PosStatus.Pending;
//                                        }
//                                        tiManager.Save(_ticketItem, out result);
//                                        foreach (IPour P in _pours)
//                                        {
//                                            P.POSTicketItemID = _ticketItem.ObjectID;
//                                            DataManagerFactory.Get().Manage<IPour>().Save(P, out result);
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                });
//            }
//            catch (Exception exp)
//            {
//                Log.WriteException("Reconcile::ReconcileNow(Pours,TickItem", exp);
//            }
//        }

//        public void ReconcileNow(IngredientContainerTypes _ingredientCat, bool _ignorePourSize, bool _substitutions)
//        {
//            try
//            {
//                IList<ITicketItemAlias> allAlias = DataManagerFactory.Get().Manage<ITicketItemAlias>().GetAll().ToList();
//                var allRecipes = DataManagerFactory.Get().Manage<IRecipe>().GetAll().ToList();

//                var tiManager = DataManagerFactory.Get().Manage<IPOSTicketItem>();

//                // Look for TicketItems that have not been reconciled
//                var ticketItems = tiManager.GetAll().Where(I => I.Reconciled != I.Quantity && 0 == (I.Status&(int)PosStatus.Alert)).ToList();
//                var allTickets = DataManagerFactory.Get().Manage<IPOSTicket>().GetAll().ToList();

//                //                    foreach (var ticketItem in ticketItems)
//                for (int i = 0; i < ticketItems.Count; ++i)
//                {
//                    Log.Time("POS - Reconcile Now (second)", LogType.Debug, true, () =>
//                    {
//                        var ticketItem = ticketItems[i];
//                        var tickets = allTickets.Where(T => T.POSTicketID == ticketItem.POSTicketID).FirstOrDefault();
//                        string establishment = null;
//                        if (null != tickets)
//                        {
//                            establishment = tickets.Establishment;
//                        }

//                        IList<ITicketItemAlias> alias = allAlias.Where(A => A.Description == ticketItem.Description).ToList();
//                        {
//                            IList<string> result;
//                            if (0 == alias.Count())
//                            {
//                                ticketItem.ItemStatus |= PosStatus.UnknownAlias;
//                                ticketItem.ItemStatus |= PosStatus.Alert;
//                                tiManager.Save(ticketItem, out result);
//                            }
//                            else
//                            {
//                                foreach (ITicketItemAlias itemAlias in alias)
//                                {
//                                    var recipes = allRecipes.Where(R => R.RecipeID == itemAlias.RecipeID).ToList();
//                                    if (0 == recipes.Count())
//                                    {
//                                        ticketItem.ItemStatus |= PosStatus.UnknownRecipe;
//                                        ticketItem.ItemStatus |= PosStatus.Alert;
//                                        tiManager.Save(ticketItem, out result);
//                                    }
//                                    else
//                                    {
//                                        foreach (var recipe in recipes)
//                                        {
//                                            ProcessRecipe(recipe, ticketItem, establishment, _ingredientCat, _ignorePourSize, _substitutions);
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    });
//                }
//              ticketItems.Clear();
//            }
//            catch (Exception exp)
//            {
//                Log.WriteException("", "Reconcile::ReconcileNow()", exp);
//            }
//        }
//    }
}




// unbranded inventory (new engine plugin... consumer tickets) add flag to ticketitemalais table... then use recipe and indg to determine what to remove from inventory

// notepad ++

// wcfprocessor.cs widgets fror tikets and pours on POS reconcile tab

// MLF send tickets
// AddActivityItem

// create new Recipe in POSReconcile TicketItems 



// look at deviceid in locations vs pour first -- move posalias to new table, more than one alias per location

// BrokenDate in Inventory... != null.. Item is not in inventory... or ExitDate == BrokenDate.. need Broke method in the manager - tagged and untagged
// spill pour type.. new button/frm on GUI

// Add par level To inventor (and grid on Inventory tab)... update par table on save.. only allow edit on par column

// new Method for unbranded bottle at as Location... use on branding screen.. radio button


// look for a the min at the current location _Bottle.Location -- done -- need error dlg when none found
// move manuf form upc to new table -- done -- need to remove manuf string from UPC
// do not update cost -- done
// Recipe creator now need To support Sub cat & quality && main & quality -- done -- needs test
// add f key to standard pour table (of pour) to ingredent table -- not size -- need to remove all double stuff in standard pour table
// par level table -- Location, UPC, and level (float) -- done -- need to remove par level from other tables
// use standardpours table changes and categies to determine allowable pour range --- done
// match up top off pours with main pour
// Sub -- anything of the same cat.. don't care about quality -- done
// Over/under pour flags... match here and flag -- bit field.. can be sub and over pour -- done

