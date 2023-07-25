using System;

namespace Jaxis.Inventory.Data
{

    public enum NozzleShapes
    {
        Round = 1,
        Oval = 2,
        Rectangle = 3,
        RollerBall = 4,
    }

    public interface IStandardNozzle : IDataObject<IStandardNozzle>
    {
        Guid StandardNozzleID { get; set; }
        double Length { get; set; }
        double Width { get; set; }
        int Shape { get; set; }

        double CalculateNozzleArea();
        double CalculateNozzleArea(double _length, double _width, NozzleShapes _shape);
        void SetValues(IStandardNozzle _nozzle);
        bool CheckNozzle(IStandardNozzle _obj);
    }
}