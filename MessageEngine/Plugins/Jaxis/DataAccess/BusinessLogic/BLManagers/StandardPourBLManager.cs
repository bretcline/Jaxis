using System;
using System.Linq;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public class StandardPourBLManager : BLManager<IStandardPour, IBLStandardPour>, IStandardPourBLManager
    {
        readonly Func<double, double > m_Converter = (double _value) => BLManagerFactory.Get().ConvertPourToUnits(_value);

        public double Get( string _name )
        {
            double rc = 0.0;
            var standard =
                DataManagerFactory.Get().Manage<IStandardPour>().GetAll().Where(
                    p => p.Name == _name).FirstOrDefault();
            
            rc = m_Converter(standard.PourStandard);
            return rc;
        }
    }
}