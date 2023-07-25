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


        public bool CheckNozzle(IStandardNozzle _nozzle)
        {
            bool rc = false;
            if (null != _nozzle &&
                _nozzle.Shape == Shape &&
                _nozzle.Length == Length &&
                _nozzle.Width == Width)
            {
                rc = true;
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
            Length = _nozzle.Length;
            Width = _nozzle.Width;
            Shape = _nozzle.Shape;
        }
        
        public override string ToString( )
        {
            return CalculateNozzleArea().ToString( "0.00" );
        }

    }
}