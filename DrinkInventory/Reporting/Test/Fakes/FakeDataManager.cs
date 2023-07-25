using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;
using Jaxis.Util.Log4Net;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    public class FakeDataManager<TConcrete, TInterface> : IDataManager<TInterface> where TConcrete : FakeDomainObject, TInterface
                                                                                   where TInterface : IDomainObject
    {
        protected static IDictionary<Guid, TInterface> ObjectDictionary;

        static FakeDataManager()
        {
            ObjectDictionary = new Dictionary<Guid, TInterface>();
        }

        public static IQueryable<TInterface> Objects
        {
            get { return ObjectDictionary.Values.AsQueryable(); }
        }

        public TInterface Create()
        {
            return Activator.CreateInstance(typeof (TConcrete)) as TConcrete;
        }

        public bool Save(TInterface _item)
        {
            ObjectDictionary[((TConcrete)_item).Id] = _item;
            return true;
        }

        public bool Delete(TInterface _item)
        {
            Delete(((TConcrete)_item).Id);
            return true;
        }

        public bool Delete(Guid _id)
        {
            ObjectDictionary.Remove(_id);
            return true;
        }

        public int Delete(Expression<Func<TInterface, bool>> _predicate)
        {
            var toDelete = ObjectDictionary.Values.AsQueryable().Where(_predicate).ToList();
            foreach (var obj in toDelete)
            {
                ObjectDictionary.Remove(((TConcrete) obj).Id);
            }

            return 0; //?
        }

        public IQueryable<TInterface> GetAll()
        {
            return ObjectDictionary.Values.AsQueryable();
        }

        public TInterface Get(Guid _id)
        {
            if (!ObjectDictionary.ContainsKey(_id))
                throw new Exception("Object id not found.");

            return ObjectDictionary[_id];
        }

        public static void DumpObjectsToLog()
        {
            foreach (var obj in Objects)
            {
                Log.Info(string.Format("{0} ObjectId = {1}", typeof(TInterface).Name, obj ));
            }
        }

        #region IDataManager<TInterface> Members


        public void Hydrate( TInterface _item, bool _includeRelatedObjects = false )
        {
            throw new NotImplementedException( );
        }

        #endregion

        #region IDataManager<TInterface> Members

        void IDataManager<TInterface>.Delete( TInterface _item )
        {
            throw new NotImplementedException( );
        }

        void IDataManager<TInterface>.Delete( Guid _id )
        {
            throw new NotImplementedException( );
        }

        #endregion

        #region IDataManager<TInterface> Members


        TInterface IDataManager<TInterface>.Save( TInterface _item )
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
