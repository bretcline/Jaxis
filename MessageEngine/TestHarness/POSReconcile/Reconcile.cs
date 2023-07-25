using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BeverageMonitor.Entities;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IDataItems;
using Jaxis.Util.Log4Net;
using IPour = Jaxis.Inventory.Data.IPour;

namespace POSReconcile
{
    //public enum PosStatus
    //{
    //    Alert = 1,
    //    Pending = 2,
    //    Complete = 4,
    //    Void = 8,
    //    UnknownAlias = 16,
    //    UnknownRecipe = 32,
    //    UnderPour = 64,
    //    OverPour = 128,
    //    Substitution = 256,
    //    TopOff = 512
    //}



    public class Reconcile
    {
        protected class ItemPour
        {
            public Pour pour { get; set; }
            public Ingredient ingredient { get; set; }
            public POSTicketItem ticketItem { get; set; }

            public Pour pourAddition { get; set; }
        }


        private const double MaxSplash = 5; // MLF this needs to be a Admin setting
        private const int MaxSplashTime = 15; // MLF this needs to be a Admin setting

        public void ConsolidatedReconcile( int _interval )
        {
            using (var data = new BeverageMonitorEntities())
            {
                int interval = _interval;

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

        protected  void ReconcileByWhatever( BeverageMonitorEntities _data, IQueryable<ItemPour> _pourtickets, int _status )
        {

            var matchedPours = new Dictionary<Guid, List<Guid>>();

            var items = _pourtickets.ToList().OrderBy( i => i.ticketItem.POSTicketItemID ).ToList();
            Console.WriteLine( String.Format( "{0}, {1}", items.Count, _status ));

            if (0 < items.Count)
            {
                foreach (var item in items)
                {
                    //if (item.ticketItem.Reconciled < item.ticketItem.Quantity &&
                    if (item.pour.POSTicketItemID == null)
                    {
                        if( !matchedPours.ContainsKey( item.ticketItem.POSTicketItemID ))
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
                            Console.WriteLine(string.Format("Ingredient for Ticket Item {0} already matched", item.ticketItem.Description ) );
                          
                        }
                    }
                    else
                    {
                        Console.WriteLine( "Ticket Item Already closed");
                    }
                    Console.WriteLine(String.Format("{0} {1}", item.ticketItem.POSTicketItemID, item.pour.PourID));
                }



                _data.SaveChanges();
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
                    select new { ticketItem = TI, pour = P };

                foreach (var pourticket in pourtickets)
                {
                    pourticket.ticketItem.Status ^= 2;
                    
                    pourticket.ticketItem.Status |= pourticket.pour.Status;
                }

                var ticket =
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
                    into missingPours
                    from P in missingPours.DefaultIfEmpty()
                    where TI.Reconciled < TI.Quantity
                    select new { ticketItem = TI, Ingredient = I, pour = P };

                var uniqueTI = ( from t in ticket
                               select new {t.ticketItem.POSTicketItemID} ).Distinct();


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
                          (_underVolume || P.Volume < (SP.PourStandard + (SP.PourStandard * SP.StandardVariance)))
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
                    select new ItemPour {pour = P, pourAddition = S, ticketItem = T };

                var tickets = pourtickets.ToList();

                foreach (var pourticket in tickets)
                {
                    if( pourticket.pour.POSTicketItemID == null)
                    {
                        pourticket.pour.POSTicketItemID = pourticket.ticketItem.POSTicketItemID;
                        pourticket.pour.Status |= 512;
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
                          (_underVolume || P.Volume < (SP.PourStandard + (SP.PourStandard * SP.StandardVariance)))
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
                          (_overVolume || P.Volume > (SP.PourStandard - (SP.PourStandard * SP.StandardVariance)) ) &&
                          (_underVolume || P.Volume < (SP.PourStandard + (SP.PourStandard * SP.StandardVariance)) )
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
                            IList<string> result;
                            if (0 == alias.Count())
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
                                    if (0 == recipes.Count())
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
                                            foreach (Pour P in _pours)
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
                            var tickets = data.POSTickets.Where(T => T.POSTicketID == ticketItem.POSTicketID).FirstOrDefault();
                            string establishment = null;
                            if (null != tickets)
                            {
                                establishment = tickets.Establishment;
                            }

                            var alias = data.TicketItemAliases.Where(A => A.Description == ticketItem.Description).ToList();
                            {
                                IList<string> result;
                                if (0 == alias.Count())
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
                                        if (0 == recipes.Count())
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
            var pours = new List<Pour>();
            bool found = false;

            try
            {
                Log.Time("POS - Process Recipe", LogType.Debug, true, () =>
                {
                    using (var data = new BeverageMonitorEntities())
                    {
                        // Process Required ingredients
                        IList<Ingredient> Ingredients = _recipe.Ingredients.Where(i => i.Type == (int) IngredientRequirementType.Required).ToList();
                            // GetIngredients(_recipe, IngredientRequirementType.Required, _ingredientCat);
                        if (0 < Ingredients.Count())
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
                                _recipe.Ingredients.Where(i => i.Type == (int) IngredientRequirementType.OneOf).ToList();
                                //GetIngredients(_recipe, IngredientRequirementType.OneOf, _ingredientCat);
                            if (0 < Ingredients.Count())
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
                                    _recipe.Ingredients.Where(i => i.Type == (int) IngredientRequirementType.Optional).
                                        ToList();
                                    //GetIngredients(_recipe, IngredientRequirementType.Optional, _ingredientCat);
                                if (0 < Ingredients.Count())
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
                                    if (0 != (_ticketItem.Status & (int) PosStatus.Pending))
                                    {
                                        _ticketItem.Status ^= (int) PosStatus.Pending;
                                    }
                                    _ticketItem.Status |= (int) PosStatus.Complete;
                                }
                                else
                                {
                                    _ticketItem.Status |= (int) PosStatus.Pending;
                                }
                                IList<string> result;
                                //DataManagerFactory.Get().Manage<IPOSTicketItem>().Save(_ticketItem, out result);
                                foreach (Pour pour in pours)
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
            List<Pour> _pours, bool _opional, bool _ignorePourSize, bool _substitutions)
        {
            var foundPours = new List<Pour>();
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
                        IList<Location> locations = data.Locations.Where( L => L.POSAlias == _establishment).ToList();
                        if (0 < locations.Count())
                        {
                            foreach (var location in locations)
                            {
                                foreach (var ingredient in _ingredients)
                                {
                                    if (false == ingredientNumbers.Contains(ingredient.Number))
                                    {
                                        ingredientNumbers.Add(ingredient.Number);
                                    }

                                    var pours = GetMatchingPours( _ticketItem, location, ingredient, _ignorePourSize,
                                                                 _substitutions);

                                    if (null != pours && 0 < pours.Count() &&
                                        false == foundNumbers.Contains(ingredient.Number))
                                    {
                                        foundNumbers.Add(ingredient.Number);
                                            // We only need to find one Ingredient of each number/type
                                        foreach (Pour P in pours)
                                        {
                                            foundPours.Add(P);
                                        }
                                    }
                                }
                            }
                        }

                        if (ingredientNumbers.Count == foundNumbers.Count || true == _opional)
                            // We found one of each or just looking for any optional ingredients
                        {
                            foreach (var pour in foundPours)
                            {
                                _pours.Add(pour);
                            }
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



        private List<Pour> GetMatchingPours(POSTicketItem _ticketItem, Location _location, Ingredient _ingredient, bool _ignorePourSize, bool _substitutions)
        {
            return Log.Time<List<Pour>>("POS Get Matching Pours", LogType.Debug, true, () =>
            {
                var matchPours = new List<Pour>();
                using (var data = new BeverageMonitorEntities())
                {
                    var pours = new List<Pour>();
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

        private List<Pour> MatchPours(BeverageMonitorEntities _data, Ingredient _ingredient, Location _location, List<Pour> _pours, bool _substitutions, POSTicketItem _ticketItem)
        {
            return Log.Time<List<Pour>>("POS Match Pours", LogType.Debug, true, () =>
            {
                var upcItems = new List<UPC>();
                if (null == _pours || 0 == _pours.Count() || true == _substitutions)
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
                                   0 == _pours.Count())
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

        private void CombineTopOffWithPour(List<Pour> _pours, POSTicketItem _ticketItem, StandardPour _sPour, List<Pour> _matchPours, bool _ignorePourSize)
        {
            Log.Time("POS Combine TopOff with Pour", LogType.Debug, true, () =>
            {
                if (null != _sPour &&
                     null != _pours && 0 != _pours.Count())
                {
                    _pours = _pours.OrderByDescending(P => P.Volume).ToList();
                    int i = 0;
                    while (0 == _matchPours.Count() &&
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

        private void LookupIngredientsByUPC( BeverageMonitorEntities _data, Ingredient _ingredient, Location _location, List<UPC> _upcs, List<Pour> _pours)
        {
            Log.Time("POS - Process Ticket Item", LogType.Debug, true, () =>
            {
                if (_ingredient.UPCID.HasValue)
                {
                    _pours.AddRange( _data.Pours.Where(P => P.POSTicketItemID == null &&
                                                    P.Alerted == false &&
                                                    P.LocationID == _location.LocationID &&
                                                    P.UPCID == _ingredient.UPCID.Value).ToList() );
                }
                else
                {
                    // Look for Ingredients that match the manufacturer and quality
                    if (_ingredient.ManufacturerID.HasValue || _ingredient.CategoryID.HasValue)
                    {
                        _upcs.AddRange(_data.UPCs.Where(U => U.ManufacturerID == _ingredient.ManufacturerID.Value &&
                                                          //U.CategoryID == _ingredient.CategoryID.Value &&
                                                          U.Quality == _ingredient.Quality).ToList() );
                    }
                    // Look for Ingredients that match the Sub cat and quality
                    else if (_ingredient.CategoryID.HasValue)
                    {
                        _upcs.AddRange(_data.UPCs.Where(U => U.CategoryID == _ingredient.CategoryID.Value &&
                                                          U.Quality == _ingredient.Quality).ToList() );
                    }
                    if (null != _upcs)
                    {
                        int i = 0;
                        while (i < _upcs.Count() &&
                               0 == _pours.Count())
                        {
                            var upcid = _upcs[i].UPCID;
                            _pours.AddRange( _data.Pours.Where(P => P.POSTicketItemID == null &&
                                                            P.Alerted == false &&
                                                            P.LocationID == _location.LocationID &&
                                                            P.UPCID == upcid).ToList() );
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
}
