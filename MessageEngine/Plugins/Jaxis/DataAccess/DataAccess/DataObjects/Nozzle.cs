using System;
using System.Collections.Generic;
using Jaxis.Inventory.Data.IBLDataItems;

namespace Jaxis.Inventory.Data
{
    public partial class StandardNozzle : IStandardNozzle, IBLStandardNozzle
    {

        public IEnumerable<IStandardNozzle> GetAll( )
        {
            return All( );
        }

        public double CalculateNozzleArea( )
        {
            return CalculateNozzleArea( Length, Width, (NozzleShapes)Shape );
        }

        /// <summary>
        /// Returns true if the parameter nozzle is equal to one of the default nozzles. 
        /// </summary>
        public bool CheckNozzle(IStandardNozzle _nozzle)
        {
            bool rc = false;
            // NOTE: This is a bad, temporal fix, we should clean it eventually
            if (Shape == 0 && Length == 0 && Width == 0)
            {
                rc = true;
            }
            else if (null != _nozzle &&
               _nozzle.Shape == Shape &&
               _nozzle.Length == Length &&
               _nozzle.Width == Width)
            {
                rc = true;
            }
            else if (null == _nozzle)
            { // check it against the standard nozzle.
                var liquor = BLManagerFactory.Get().GetDefaultNozzle(DefaultNozzleType.Liquor);
                var liquorCheck = CheckNozzle(liquor);

                var wine = BLManagerFactory.Get().GetDefaultNozzle(DefaultNozzleType.Wine);
                var wineCheck = CheckNozzle(wine);

                rc = !(liquorCheck == wineCheck);
                if (liquorCheck == true && wineCheck == true)
                {
                    rc = true;
                }
            }
            return rc;
        }

        public double CalculateNozzleArea( double _length, double _width, NozzleShapes _shape )
        {
            double area = 0.0;
            switch (_shape)
            {
                case NozzleShapes.Round:
                    {
                        double average = ( _length + _width ) / 2;
                        area = Math.PI * Math.Pow((average/2), 2);
                        break;
                    }
                case NozzleShapes.Oval:
                    {
                        area = Math.PI * ( _length / 2 ) * (_width / 2);
                        break;
                    }
                case NozzleShapes.Rectangle:
                    {
                        area = _length * _width;
                        break;
                    }
            }
            return area;
        }
        
        public void SetValues( IStandardNozzle _nozzle )
        {
            if (_nozzle != null)
            {
                Length = _nozzle.Length;
                Width = _nozzle.Width;
                Shape = _nozzle.Shape;
            }
        }
        
        public override string ToString( )
        {
            return CalculateNozzleArea().ToString( "0.00" );
        }

    }
}