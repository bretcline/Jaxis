using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dToothLoad
{
    public class DataRead
    {
        protected double m_AngleX = 0.0;
        protected double m_AngleY = 0.0;
        protected double m_AngleZ = 0.0;
        public double AngleX { get { return m_AngleX; } set { m_AngleX = 90.0 - value; } }
        public double AngleY { get { return m_AngleY; } set { m_AngleY = 90.0 - value; } }
        public double AngleZ { get { return m_AngleZ; } set { m_AngleZ = value; } }
        public double Time { get; set; }
        public int Sensor { get; set; }

        public DataRead(double _AngleX, double _AngleY, double _AngleZ, double _Time, int _Sensor)
        {
            AngleX = _AngleX;
            AngleY = _AngleY;
            AngleZ = _AngleZ;
            Time = _Time;
            Sensor = _Sensor;
        }

        public DataRead()
        {
        }
    }

}
