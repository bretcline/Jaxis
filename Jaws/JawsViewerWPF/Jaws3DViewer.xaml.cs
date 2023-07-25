using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Serialization;
using HelixToolkit.Wpf;
//using _3dToothLoad;

namespace JawsViewerWPF
{
    /// <summary>
    /// Interaction logic for Jaws3DViewer.xaml
    /// </summary>
    public partial class Jaws3DViewer : UserControl
    {
        protected MouseHelper<PerspectiveCamera> mouseHelper = null;

        GeometryModel3D lowerModel = new GeometryModel3D();
        GeometryModel3D upperModel = new GeometryModel3D();
        ModelVisual3D upperVisual3D = new ModelVisual3D();
        ModelVisual3D lowerVisual3D = new ModelVisual3D();
        private Model3DGroup lowerJaw = null;
        private Model3DGroup upperJaw = null;

        private Transform3DGroup lowerTransform;
        private Transform3DGroup upperTransform;

        private ZStarDevice.DataRead upperData ;
        public ZStarDevice.DataRead UpperData
        {
            get
            {
                return upperData;
            }
            set
            {
                upperData = value;
                MoveUpperJaw( upperData );
            }
        }

        public ZStarDevice.DataRead lowerData;
        public ZStarDevice.DataRead LowerData
        {
            get
            {
                return lowerData;
            }
            set
            {
                lowerData = value;
                MoveLowerJaw( lowerData );
            }
        }


        private int upperIndex = 0;
        private int lowerIndex = 0;

        private double multiplyer = 1;

        public Jaws3DViewer()
        {
            InitializeComponent();
        }

        private Jaws3DViewerConfig m_Config;
        public Jaws3DViewerConfig Config
        {
            get { return m_Config; }
            set 
            {
                m_Config = value;
                Configure();
            }
        }

        public void Configure()
        {
            
            mouseHelper = new MouseHelper<PerspectiveCamera>(viewPortTeeth);
            mouseHelper.EventSource = viewPortTeeth;
            var XAxis = new ModelVisual3D();

            var import = new ModelImporter();
            
            lowerJaw = import.Load(System.Configuration.ConfigurationManager.AppSettings["LowerJaw"]);
            upperJaw = import.Load(System.Configuration.ConfigurationManager.AppSettings["UpperJaw"]);

            var myPCamera = new PerspectiveCamera();
            myPCamera.Transform = mouseHelper.Transform;

            // Asign the camera to the viewport
            viewPortTeeth.Camera = myPCamera;
            // Define the lights cast in the scene. Without light, the 3D object cannot 
            // be seen. Note: to illuminate an object from additional directions, create 
            // additional lights.

            //var topLight = new DirectionalLight();
            //topLight.Color = Colors.Red;
            //topLight.Direction = new Vector3D(.5, 0.5, .5);


            var bottomLight = new DirectionalLight();
            bottomLight.Color = Colors.Turquoise;
            bottomLight.Direction = new Vector3D(-0.61, -0.5, -0.61);

            upperJaw.Children.Add(bottomLight);
            lowerJaw.Children.Add(bottomLight);

            // The geometry specifes the shape of the 3D plane. In this sample, a flat sheet 
            // is created.
            // The material specifies the material applied to the 3D object. In this sample a  
            // linear gradient covers the surface of the 3D object.

            // Create a horizontal linear gradient with four stops.   
            var myHorizontalGradient = new LinearGradientBrush();
            myHorizontalGradient.StartPoint = new Point(0, 0.5);
            myHorizontalGradient.EndPoint = new Point(1, 0.5);
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Yellow, 0.0));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Red, 0.25));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Gray, 0.75));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.LimeGreen, 1.0));

            // Define material and apply to the mesh geometries.
            var myMaterial = new DiffuseMaterial(myHorizontalGradient);
            lowerModel.Material = myMaterial;
            upperModel.Material = myMaterial;

            #region Lower Transform

            // Apply multiple transformations to the object. In this sample, a rotation and scale  
            // transform is applied. 

            // Create and apply a transformation that rotates the object.
            var xTransform = new RotateTransform3D();
            var xAngleRotation = new AxisAngleRotation3D { Axis = new Vector3D(1, 0, 0), Angle = 0 };
            xTransform.Rotation = xAngleRotation;

            // Create and apply a transformation that rotates the object.
            var yTransform = new RotateTransform3D();
            var yAngleRotation = new AxisAngleRotation3D { Axis = new Vector3D(0, 1, 0), Angle = 0 };
            yTransform.Rotation = yAngleRotation;

            // Create and apply a transformation that rotates the object.
            var zTransform = new RotateTransform3D();
            var zAngleRotation = new AxisAngleRotation3D { Axis = new Vector3D(0, 0, 1), Angle = 0 };
            zTransform.Rotation = zAngleRotation;

            var jawLengthLower = new TranslateTransform3D();
            jawLengthLower.OffsetX = 0;
            jawLengthLower.OffsetY = 20;


            // Add the rotation transform to a Transform3DGroup
            lowerTransform = new Transform3DGroup();
            lowerTransform.Children.Add(zTransform);
            lowerTransform.Children.Add(yTransform);
            lowerTransform.Children.Add(xTransform);
            lowerTransform.Children.Add(jawLengthLower);

            // Set the Transform property of the GeometryModel to the Transform3DGroup which includes  
            // both transformations. The 3D object now has two Transformations applied to it.
            lowerJaw.Transform = lowerTransform;

            #endregion Lower Transform


            #region Upper Transform

            // Apply multiple transformations to the object. In this sample, a rotation and scale  
            // transform is applied. 

            // Create and apply a transformation that rotates the object.
            xTransform = new RotateTransform3D();
            xAngleRotation = new AxisAngleRotation3D { Axis = new Vector3D(1, 0, 0), Angle = 0 };
            xTransform.Rotation = xAngleRotation;

            // Create and apply a transformation that rotates the object.
            yTransform = new RotateTransform3D();
            yAngleRotation = new AxisAngleRotation3D { Axis = new Vector3D(0, 1, 0), Angle = 0 };
            yTransform.Rotation = yAngleRotation;

            // Create and apply a transformation that rotates the object.
            zTransform = new RotateTransform3D();
            zAngleRotation = new AxisAngleRotation3D { Axis = new Vector3D(0, 0, 1), Angle = 0 };
            zTransform.Rotation = zAngleRotation;


            var jawLengthUpper = new TranslateTransform3D();
            jawLengthUpper.OffsetX = 0;
            jawLengthUpper.OffsetY = 20;

            // Add the rotation transform to a Transform3DGroup
            upperTransform = new Transform3DGroup();
            upperTransform.Children.Add(zTransform);
            upperTransform.Children.Add(yTransform);
            upperTransform.Children.Add(xTransform);
            upperTransform.Children.Add(jawLengthUpper);
                        
            // Set the Transform property of the GeometryModel to the Transform3DGroup which includes  
            // both transformations. The 3D object now has two Transformations applied to it.
            upperJaw.Transform = upperTransform;

            #endregion Upper Transform
            
            //upperJaw.Children.Add(upperModel);

            // Add the group of models to the ModelVisual3d.
            upperVisual3D.Content = upperJaw;
            lowerVisual3D.Content = lowerJaw;
            // 
            viewPortTeeth.Children.Add(upperVisual3D);
            viewPortTeeth.Children.Add(lowerVisual3D);

            btnSetCameraLook_Click(null, null);

            var upperLength = upperTransform.Children[3] as TranslateTransform3D;
            upperLength.OffsetX = m_Config.UpperOffset;

            var lowerLength = lowerTransform.Children[3] as TranslateTransform3D;
            lowerLength.OffsetX = m_Config.LowerOffset;

            var z = lowerTransform.Children[2] as RotateTransform3D;
            z.CenterX = m_Config.LowerOffset;
            var angle = z.Rotation as AxisAngleRotation3D;
            angle.Angle = 0;

            var y = lowerTransform.Children[1] as RotateTransform3D;
            y.CenterX = m_Config.LowerOffset;
            angle = y.Rotation as AxisAngleRotation3D;
            angle.Angle = 0;

            var x = lowerTransform.Children[0] as RotateTransform3D;
            x.CenterX = m_Config.LowerOffset;
            angle = x.Rotation as AxisAngleRotation3D;
            angle.Angle = 0;

        }

        private void btnSetCameraLook_Click(object sender, RoutedEventArgs e)
        {
            mouseHelper.CameraLookPosition = new Vector3D(m_Config.CameraPosition.X, m_Config.CameraPosition.Y, m_Config.CameraPosition.Z);
            mouseHelper.Look();

            var camera = viewPortTeeth.Camera as PerspectiveCamera;

            camera.Position = m_Config.CameraPosition;

            var cameraPoint = camera.Position;
            var zeroPoint = new Point3D(0, 0, 0);

            var look = new Vector3D();
            var lookpoint = zeroPoint - cameraPoint;

            look.X = lookpoint.X;
            look.Y = lookpoint.Y;
            look.Z = lookpoint.Z;

            mouseHelper.CameraLookPosition = look;
            mouseHelper.Look();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            var upperLength = upperTransform.Children[3] as TranslateTransform3D;
            upperLength.OffsetX = m_Config.UpperOffset;

            var lowerLength = lowerTransform.Children[3] as TranslateTransform3D;
            lowerLength.OffsetX = m_Config.LowerOffset;

            var z = lowerTransform.Children[2] as RotateTransform3D;
            var angle = z.Rotation as AxisAngleRotation3D;
            angle.Angle = 0;

            var y = lowerTransform.Children[1] as RotateTransform3D;
            angle = y.Rotation as AxisAngleRotation3D;
            angle.Angle = 0;

            var x = lowerTransform.Children[0] as RotateTransform3D;
            angle = x.Rotation as AxisAngleRotation3D;
            angle.Angle = 0;
        }

        private void btnTransform_Click(object sender, RoutedEventArgs e)
        {
            var z = lowerTransform.Children[2] as RotateTransform3D;
            z.CenterX = m_Config.LowerOffset;
            var angle = z.Rotation as AxisAngleRotation3D;
            angle.Angle = 0;

            var y = lowerTransform.Children[1] as RotateTransform3D;
            y.CenterX = m_Config.LowerOffset;
            angle = y.Rotation as AxisAngleRotation3D;
            angle.Angle = 0;

            var x = lowerTransform.Children[0] as RotateTransform3D;
            x.CenterX = m_Config.LowerOffset;
            angle = x.Rotation as AxisAngleRotation3D;
            angle.Angle = 0;
        }


        private void MoveUpperJaw(ZStarDevice.DataRead data)
        {
            if ( m_Config.MoveUpper )
            {
                if ( m_Config.MoveZ )
                {
                    var z = upperTransform.Children[2] as RotateTransform3D;
                    var angle = z.Rotation as AxisAngleRotation3D;
                    angle.Angle = data.AngleZ;
                }

                if (m_Config.MoveY)
                {
                    var y = upperTransform.Children[1] as RotateTransform3D;
                    var angle = y.Rotation as AxisAngleRotation3D;
                    angle.Angle = data.AngleY;
                }

                if (m_Config.MoveX)
                {
                    var x = upperTransform.Children[0] as RotateTransform3D;
                    var angle = x.Rotation as AxisAngleRotation3D;
                    angle.Angle = data.AngleX;
                }
            }
        }


        private void MoveLowerJaw(ZStarDevice.DataRead data)
        {
            if (m_Config.MoveLower)
            {
                if (m_Config.MoveZ)
                {
                    var z = lowerTransform.Children[2] as RotateTransform3D;
                    if (z != null)
                    {
                        var angle = z.Rotation as AxisAngleRotation3D;
                        if (angle != null)
                        {
                            angle.Angle = data.AngleZ;
                        }
                    }
                }

                if (m_Config.MoveY)
                {
                    var y = lowerTransform.Children[1] as RotateTransform3D;
                    if (y != null)
                    {
                        var angle = y.Rotation as AxisAngleRotation3D;
                        if (angle != null)
                        {
                            angle.Angle = data.AngleY;
                        }
                    }
                }

                if (m_Config.MoveX)
                {
                    var x = lowerTransform.Children[0] as RotateTransform3D;
                    if (x != null)
                    {
                        var angle = x.Rotation as AxisAngleRotation3D;
                        if (angle != null)
                        {
                            angle.Angle = data.AngleX;
                        }
                    }
                }
                txtAngles.Text = string.Format("X:{0} Y:{1} Z:{2} ", data.AngleX, data.AngleY, data.AngleZ);
            }
        }
    }
}
