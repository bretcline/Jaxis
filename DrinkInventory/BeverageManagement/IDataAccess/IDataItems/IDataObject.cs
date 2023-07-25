using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jaxis.Inventory.Data
{
    public interface IDataObject<T>
    {
        //void Save( );
        //void Delete( );
        IEnumerable<T> GetAll( );

        [Obfuscation(Exclude = true)]
        Guid ObjectID { get; set; }
    }
}
