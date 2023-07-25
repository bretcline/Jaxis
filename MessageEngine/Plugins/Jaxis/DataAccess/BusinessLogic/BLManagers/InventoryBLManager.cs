using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using SubSonic.Query;

namespace Jaxis.Inventory.Data
{
    public class InventoryBLManager : BLManager<IInventory, IBLInventory>, IInventoryBLManager
    {
        //Edit box for Memo field
        private InventoryHelper m_Helper = new InventoryHelper();

        public IBLInventory Create(IBLUPCItem _upcItem, IBLLocation _location, decimal _cost)
        {
            var item = new Inventory
            {
                UPC = _upcItem,
                UPCID = _upcItem.UPCID,
                Location = _location,
                LocationID = _location.LocationID,
                Cost = _cost,
                EnterDate = DateTime.Now,
                Amount =
                    BLManagerFactory.ConvertToMLFromSizeType(_upcItem.Size, _upcItem.SizeType),
                UserSessionID = BLManagerFactory.Get().UserSession.SessionID
            };

            return item;
        }

        public IBLInventory GetInventoryByTag( Guid _tagId )
        {
            IBLInventory rc = null;
            try
            {
                var item = DataManagerFactory.Get().Manage<IInventory>().GetAll().FirstOrDefault(I => I.TagID == _tagId && I.ExitDate == null);
                rc = item as IBLInventory;
            }
            catch (Exception err)
            {
                Log.WriteException( "InventoryBLManager::GetInventory", err );
            }
            return rc;
        }

        public IBLInventory Create(IBLBrandedBottle _Bottle)
        {
            var item = new Inventory
            {
                Amount =
                    BLManagerFactory.ConvertToMLFromSizeType(_Bottle.UPC.Size,
                                                            _Bottle.UPC.SizeType),
                Cost = _Bottle.Cost,
                LocationID = _Bottle.Tag.LocationID,
                UPCID = _Bottle.UPC.UPCID,
                TagID = _Bottle.Tag.TagID,
                EnterDate = DateTime.Now,
                UserSessionID = BLManagerFactory.Get().UserSession.SessionID
            };

            return item;
        }

        public bool AddToInventory(IBLInventory _inv, int _quantity)
        {
            bool rc = false;
            try
            {
                var upcMan = BLManagerFactory.Get().ManageUPCs();
                var upc = upcMan.Get(_inv.UPCID);
                if (null != upc)
                {
                    _inv.EnterDate = DateTime.Now;

                    //need to check if this is a case 
                    if (upc.ChildUPCID.HasValue)
                    {
                        var bottleUPC = upcMan.Get(upc.ChildUPCID.Value);
                        if (null != bottleUPC)
                        {
                            _inv.Amount = bottleUPC.Size;
                            if (upc.BottleCount.HasValue)
                            {
                                _inv.Cost /= upc.BottleCount.Value;
                                for (int i = 0; i < _quantity * upc.BottleCount.Value; i++)
                                {
                                    var newInv = BLManagerFactory.Get().ManageInventory().Create(bottleUPC, _inv.Location, _inv.Cost);
                                    Save(newInv);
                                }
                            }
                        }
                    }
                    else
                    {
                        _inv.Amount = upc.Size;
                        for (int i = 0; i < _quantity; i++)
                        {
                            Save(_inv);
                        }
                    }

                    if (!_inv.UPC.UnitCost.HasValue)
                    {
                        // MLF but set if no value
                        _inv.UPC.UnitCost = Convert.ToDecimal(_inv.Cost);
                        upcMan.Save(_inv.UPC);
                    }
                }
            }
            catch (Exception err)
            {
                Log.WriteException( "InventoryBLManager::AddToInventory", err);
            }
            return rc;
        }

        private bool CleanTag(Guid _tagID, ExitReasons _reason )
        {
            bool rc = false;
            try
            {
                var items = DataManagerFactory.Get().Manage<IInventory>().GetAll().Where(I => I.TagID == _tagID &&
                                                I.TagDate != null &&
                                                I.ExitDate == null).ToList();

                foreach (var blInventory in items)
                {
                    blInventory.ExitDate = DateTime.Now;
                    blInventory.ExitReason = (int)_reason;
                    rc = Save(blInventory as IBLInventory);
                }
            }
            catch (Exception err)
            {
                Log.WriteException("InventoryBLManager::CleanTag", err);
            }
            return rc;
        }

        public bool TagInventory<T>(T _bottle) where T : IBLBottle
        {
            bool rc = false;

            try
            {
                CleanTag(_bottle.Tag.TagID, ExitReasons.Cleaned);

                IBLInventory item;
                if (_bottle is IBLBrandedBottle && (_bottle as IBLBrandedBottle).NewInventory)
                {
                    item = Create(_bottle as IBLBrandedBottle);
                }
                else
                {
                    item = GetOldestUntaggedItem(_bottle.UPC.UPCID, _bottle.FromLocation.LocationID);
                    if (_bottle is IBLBrandedBottle && null == item)
                    {
                        item = Create(_bottle as IBLBrandedBottle);
                    }
                }
                if (null != item)
                {
                    if (_bottle.Quantity.HasValue)
                    {
                        item.Amount = _bottle.Quantity.Value;
                    }
                    item.TagDate = DateTime.Now;
                    item.TagID = _bottle.Tag.TagID;
                    item.Location = _bottle.ToLocation;
                    rc = Save(item);
                    MoveItem(false, item.InventoryID, _bottle.FromLocation.LocationID, _bottle.ToLocation.LocationID);
                }
            }
            catch (Exception err)
            {
                Log.WriteException("InventoryBLManager::TagInventory<>", err);
            }
            return rc;
        }

        public bool TagInventory(IBLUPCItem _UPC, IBLTag _tag, IBLLocation _fromLocation, IBLLocation _toLocation)
        {
            bool rc = false;
            try
            {
                CleanTag(_tag.TagID, ExitReasons.Cleaned);
                IBLInventory item = GetOldestUntaggedItem(_UPC.UPCID, _fromLocation.LocationID);
                if (null != item)
                {
                    item.Amount = _UPC.Size;
                    item.TagDate = DateTime.Now;
                    item.TagID = _tag.TagID;
                    item.Location = _toLocation;
                    rc = Save(item);
                    MoveItem(false, item.InventoryID, _fromLocation.LocationID, _toLocation.LocationID);
                }
            }
            catch (Exception err)
            {
                Log.WriteException("InventoryBLManager::TagInventory", err);
            }
            return rc;
        }

        private IBLInventory GetOldestUntaggedItem(Guid _UPCID, Guid _from)
        {
            var rc = GetOldestUntaggedItems(_UPCID, _from, 1);
            return (rc != null) ? rc.FirstOrDefault() : null;
        }


        private IEnumerable<IBLInventory> GetOldestUntaggedItems(Guid _UPCID, Guid _from, int _count)
        {
            List<IBLInventory> rc = null;
            try
            {
                var items = (from inv in DataManagerFactory.Get().Manage<IInventory>().GetAll()
                            orderby inv.EnterDate ascending
                            where inv.ExitDate == null
                                && inv.LocationID == _from
                                && inv.TagDate == null
                                && inv.UPCID == _UPCID
                            select inv).Take( _count );
                rc = items.ToList().Cast<IBLInventory>().ToList();
            }
            catch (Exception err)
            {
                Log.WriteException("InventoryBLManager::GetOldestUntaggedItem", err);
            }
            return rc;
        }

        public bool MoveUnTaggedInventory(Guid _UPCID, Guid _from, Guid _to, decimal _cost = 0.0m)
        {
            bool rc = false;
            try
            {
                var upc = BLManagerFactory.Get().ManageUPCs().Get(_UPCID);

                if (_from == BLManagerFactory.Get().ManageLocations().NewInventoryLocation.LocationID)
                {
                    var location = BLManagerFactory.Get().ManageLocations().Get(_to);

                    rc = AddToInventory(Create(upc, location, _cost), 1);
                }
                else
                {
                    int count = 1;
                    var upcID = _UPCID;
                    if (null != upc && upc.BottleCount.HasValue && 0 < upc.BottleCount)
                    {
                        count = upc.BottleCount.Value;
                    }

                    var items = GetOldestUntaggedItems(upcID, _from, count);
                    foreach (var item in items)
                    {
                        item.LocationID = _to;
                        Save(item);
                        MoveItem(false, item.InventoryID, _from, _to);
                    }
                    rc = true;
                }
            }
            catch (Exception err)
            {
                Log.WriteException( "MoveUnTaggedInventory", err);
            }
            return rc;
        }

        private void MoveItem(bool _isTag, Guid _itemID, Guid _from, Guid _to )
        {
            try
            {
                var move = new Move();

                if (_isTag)
                {
                    move.TagID = _itemID;
                }
                else
                {
                    move.InventoryID = _itemID;
                }
                move.OldLocation = _from;
                move.NewLocation = _to;
                move.Quantity = 1;
                move.MoveTime = DateTime.Now;

                move.Save();
            }
            catch (Exception err)
            {
                Log.WriteException( "InventoryBLManager::MoveItem", err);
            }
        }


        public bool RemoveUnTaggedItemsFromInventory(Guid _UPCID, Guid _LocationID, int _count, ExitReasons _reason, string _memo = null)
        {
            bool rc = false;
            try
            {
                var upc = BLManagerFactory.Get().ManageUPCs().Get(_UPCID);

                int count = 1;
                var upcID = _UPCID;
                if (null != upc && upc.BottleCount.HasValue && 0 < upc.BottleCount)
                {
                    count = upc.BottleCount.Value;
                }

                count = _count*count;

                var items = GetOldestUntaggedItems(_UPCID, _LocationID, count);
                foreach (var item in items)
                {
                    item.ExitDate = DateTime.Now;
                    item.ExitReason = (int)_reason;
                    item.Memo = _memo;
                    Save(item);
                    rc = true;
                }
            }
            catch (Exception err)
            {
                Log.WriteException("InventoryBLManager::RemoveUnTaggedItemsFromInventory", err);
            }
            return rc;
        }


        public bool RemoveUnTaggedFromInventory(Guid _UPCID, Guid _LocationID, ExitReasons _reason, string _memo = null)
        {
            bool rc = false;
            try
            {
                RemoveUnTaggedItemsFromInventory(_UPCID, _LocationID, 1, _reason, _memo);
            }
            catch (Exception err)
            {
                Log.WriteException("InventoryBLManager::RemoveUnTaggedFromInventory", err);
            }
            return rc;
        }

        public bool RemoveTagFromInventory(Guid _tagId, ExitReasons _reason)
        {
            return CleanTag( _tagId, _reason );
        }

        public bool MarkBrokenTaggedBottle(Guid _tagId)
        {
            bool rc = false;

            // Mark the bottle as broken and removed from Inventory
            var item = GetInventoryByTag(_tagId);
            if (null != item)
            {
                item.ExitDate = DateTime.Now;
                item.ExitReason = (int)ExitReasons.Broken;
                Save(item);
                rc = true;
            } 

            return rc;
        }

        public bool MarkBrokenUnTaggedBottle(Guid _UPCID, Guid _LocationID)
        {
            bool rc = false;

            var item = GetOldestUntaggedItem(_UPCID, _LocationID);
            if (null != item)
            {
                item.ExitReason = (int)ExitReasons.Broken;
                item.ExitDate = DateTime.Now;
                Save(item);
                rc = true;
            }
            return rc;
        }


        public int CheckAvailableInventory(Guid _upcID, Guid _locationID)
        {
            int rc = 0;
            try
            {
                rc = DataManagerFactory.Get().Manage<IInventory>().GetAll().Count(I => I.UPCID == _upcID && 
                    I.LocationID == _locationID && 
                    I.TagDate == null &&
                    I.ExitDate == null);
            }
            catch (Exception err)
            {
                Log.WriteException("CheckAvailabileInventory", err);
            }
            return rc;
        }


        public int GetInventoryCount(Guid _upcid, Guid _locationId)
        {
            var rc = 0;
            Log.Time("InventoryBLManager::GetInventoryCount", LogType.Debug, false, () =>
            {
                try
                {
                    var db =
                        new CodingHorror(
                            string.Format("select COUNT(1) from Inventories WHERE UPCID = '{0}' AND LocationID = '{1}' AND ExitDate IS NULL AND TagDate IS NULL",
                                          _upcid, _locationId));
                    rc = db.ExecuteScalar<int>();
                }
                catch (Exception err)
                {
                    Log.WriteException("CheckAvailabileInventory", err);
                }
            });
            return rc;
        }

        public void UpdateInventoryCost(Guid _upcid, double _cost)
        {
            m_Helper.UpdateInventoryCost(_upcid, _cost);
        }
    }

    internal class InventoryHelper
    {
        private BeverageMonitorDB m_Database = null;

        private BeverageMonitorDB GetDB()
        {
            if (null == m_Database)
                m_Database = new BeverageMonitorDB();
            return m_Database;
        }

        public void UpdateInventoryCost(Guid _upcid, double _cost)
        {
            var db = GetDB();
            var proc = db.procUpdateInventoryCost(_upcid, _cost);
            proc.Execute();
        }
    }
}
