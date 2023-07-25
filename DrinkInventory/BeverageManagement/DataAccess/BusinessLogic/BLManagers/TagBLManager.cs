using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using SubSonic.Query;

namespace Jaxis.Inventory.Data
{
    public class TagBLManager : BLManager<ITag, IBLTag>, ITagBLManager
    {
        private readonly Dictionary<Guid, string> m_TagNumbers = new Dictionary<Guid, string>();

        #region ITagBLManager Members

        public override IBLTag Get(Guid _id)
        {
            var item = base.Get(_id);
            if (!m_TagNumbers.ContainsKey(_id))
            {
                m_TagNumbers[_id] = item.TagNumber;
            }
            return item;
        }

        public string GetTagNumber( Guid _id )
        {
            return Log.Wrap("TagBLManager::GetTagNumber", LogType.Debug, false, () =>
            {
                if (!m_TagNumbers.ContainsKey(_id))
                {
                    var item = base.Get(_id);
                    m_TagNumbers[_id] = item.TagNumber;
                }
                return m_TagNumbers[_id];
            });
        }

        public void UnBrand(IBLTag _tag, ExitReasons _reason)
        {
            Log.Wrap("TagBLManager::UnBrand", LogType.Debug, false, () =>
            {
                var invMan = BLManagerFactory.Get().ManageInventory();
                if (null != _tag.CurrentInventory)
                {
                    var inventory = _tag.CurrentInventory;
                    inventory.Memo = _tag.Memo;
                    inventory.ExitReason = (int)_reason;
                    invMan.Save(inventory);
                }
                invMan.RemoveTagFromInventory(_tag.TagID, _reason);
                _tag.LocationID = Guid.Empty;
                return Save(_tag);
            });
        }

        bool BrandBottles( IEnumerable<BLBrandedBottle> _bottles )
        {
            //return Log.Wrap("TagBLManager::BrandBottles", LogType.Debug, false, () =>
            {
                var man = BLManagerFactory.Get().ManageBrandedBottles();
                return man.SaveBrandedBottles(_bottles);
            }//);
        }

        public IBLTag GetTagByTagNumber(string _tagId)
        {
            //return Log.Wrap("TagBLManager::GetTagByTagNumber", LogType.Debug, false, () =>
            {
                IBLTag rc = null;
                rc = DataManagerFactory.Get().Manage<ITag>().GetAll().Where(
                    t => t.TagNumber == _tagId).ToList().Cast<IBLTag>().FirstOrDefault();
                return rc;
            }//);
        }

        public void UpdateTagLocations( Guid _location, Guid _tagId )
        {
            //Log.Wrap("TagBLManager::UpdateTagLocations", LogType.Debug, false, () =>
            {
                var db = new CodingHorror(string.Format("UPDATE Tags SET LocationID = '{0}' WHERE TagID = '{1}' ", _location, _tagId));
                db.Execute();
                //return true;
            }//);
        }

        public IEnumerable<IBLTag> GetTagWithInventory(string _tagId)
        {
            //return Log.Wrap("TagBLManager::GetTagWithInventory", LogType.Debug, false, () =>
            {
                var rc = new List<IBLTag>();

                var db = new CodingHorror(string.Format("SELECT T.TagID FROM Inventories I JOIN Tags T ON I.TagID = T.TagID WHERE T.TagNumber = '{0}' AND I.ExitDate IS NULL", _tagId));
                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rc.Add( BLManagerFactory.Get().ManageTags().Get( reader.GetGuid(0 )));
                    }
                }

                return rc;
            }//);
        }
        #endregion

    }

    public class DeviceBLManager : BLManager<IDevice, IBLDevice>, IDeviceBLManager
    {
        
    }
}
