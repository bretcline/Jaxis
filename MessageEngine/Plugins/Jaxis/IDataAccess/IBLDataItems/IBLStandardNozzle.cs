using System;

namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface IBLStandardNozzle : IStandardNozzle
    {
        Guid StandardNozzleID { get; set; }
        double Length { get; set; }
        double Width { get; set; }
        int Shape { get; set; }

        double CalculateNozzleArea( );
        double CalculateNozzleArea( double _length, double _width, NozzleShapes _shape );
        void SetValues( IStandardNozzle _nozzle );
        bool CheckNozzle(IStandardNozzle _nozzle);
    }
}