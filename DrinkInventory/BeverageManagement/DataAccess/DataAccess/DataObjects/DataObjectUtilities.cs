using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Inventory.Data.DataAccess.DataObjects
{
    //public static class DataObjectUtilities
    //{


    //    public static double CalculateNozzleDiameter( double? _length, double? _width, int? _shape )
    //    {
    //        double rc = 0.0;
    //        if (_length != null && _width != null && _shape != null)
    //        {
    //            rc = CalculateNozzleDiameter( _length.Value, _width.Value, (NozzleShape) _shape.Value);
    //        }
    //        return rc;
    //    }

    //    public static double CalculateNozzleDiameter( double _length, double _width, NozzleShape _shape )
    //    {
    //        double area = 0.0;
    //        switch (_shape)
    //        {
    //            case NozzleShape.Round:
    //            {
    //                double average = ( _length + _width ) / 2;
    //                area = Math.PI * Math.Pow((average/2), 2);
    //                break;
    //            }
    //            case NozzleShape.Oval:
    //            {
    //                area = Math.PI * ( _length / 2 ) * (_width / 2);
    //                break;
    //            }
    //            case NozzleShape.Rectangle:
    //            {
    //                area = _length * _width;
    //                break;
    //            }
    //        }
    //        return area;
    //    }
    //}
}
