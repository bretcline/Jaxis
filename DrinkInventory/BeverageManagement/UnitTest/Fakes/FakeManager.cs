using System;
using System.Collections.Generic;
using System.Linq;
using Jaxis.Inventory.Data;
using Jaxis.Util.Log4Net;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest.Fakes
{
    class FakeManager<TBlInterface,TDataInterface,TConcrete> 
        where TConcrete : TBlInterface, new() where TBlInterface : IDataObject<TDataInterface>
    {
        protected readonly Dictionary<Guid, TBlInterface> Items;

        public FakeManager()
        {
            Items = new Dictionary<Guid, TBlInterface>();
        }

        public TBlInterface Create()
        {
            return new TConcrete();
        }

        public bool Save(TBlInterface _item)
        {
            Log.Info(string.Format("FakeManager saving {0}", _item.ObjectID));
            Items[_item.ObjectID] = _item;
            return true;
        }

        public bool Delete(TBlInterface _item)
        {
            Items.Remove(_item.ObjectID);
            return true;
        }

        public IEnumerable<TBlInterface> GetAll()
        {
            return Items.Values.ToList();
        }

        public TBlInterface Get(Guid _id)
        {
            TBlInterface item;
            var found = Items.TryGetValue(_id, out item);
            if (!found)
            {
                Log.Info(string.Format("FakeManager did not find an item with ID = {0}", _id));
            }

            return item;
        }
    }
}
