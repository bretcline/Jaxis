using System.Collections.Generic;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IDataItems;
using Jaxis.Inventory.Data.IUIDataItems;

// ReSharper disable CheckNamespace
namespace Jaxis.Inventory.Data
// ReSharper restore CheckNamespace
{
// ReSharper disable InconsistentNaming
    public partial class vwPosPour : IBLPosPour, IUIPosPour
// ReSharper restore InconsistentNaming
    {
        public IEnumerable<IPosPour> GetAll()
        {
            return All();
        }

        public PosStatus Status
        {
            get
            {
                PosStatus status;
                PosStatus.TryParse(StatusText, false, out status);
                return status;
            }
        }
    }
}
