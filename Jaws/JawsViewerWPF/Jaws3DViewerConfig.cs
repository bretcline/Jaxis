using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace JawsViewerWPF
{
    public class Jaws3DViewerConfig
    {
        public bool MoveUpper { get; set; }
        public bool MoveLower { get; set; }
        public bool MoveX { get; set; }
        public bool MoveY { get; set; }
        public bool MoveZ { get; set; }

        public Point3D CameraPosition { get; set; }

        public double UpperOffset { get; set; }
        public double LowerOffset { get; set; }

        public Jaws3DViewerConfig()
        {
            
        }
    }
}
