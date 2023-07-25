using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Inventory.Data
{

    public class LocalDataAttribute : Attribute
    {
    }

    public interface IDataObject<T>
    {
        //void Save( );
        //void Delete( );
        IEnumerable<T> GetAll();
        
        Guid ObjectID { get; set; }

        bool IsDirty();
    }
}
