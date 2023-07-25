using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

public class MouseHelper<T> where T : ProjectionCamera
{
    private Viewport3D _viewport3;
    private FrameworkElement _eventSource;
    private Point _position;

    private Transform3DGroup _transform;
    private TranslateTransform3D _translate
        = new TranslateTransform3D();

    public MouseHelper( Viewport3D viewport )
    {
        _viewport3 = viewport;
        _transform = new Transform3DGroup();
        _transform.Children.Add(_translate);
    }

    public Vector3D CameraLookPosition { get; set; }
    public Vector3D CameraPosition { get; set; }

    public Transform3D Transform
    {
        get { return _transform; }
    }

    public string CurrentLook
    {
        get
        {
            var camera = _viewport3.Camera as T;
            return string.Format("{0} {1} {2}", camera.LookDirection.X, camera.LookDirection.Y, camera.LookDirection.Z);
        }
    }

    public void Reset()
    {
        var camera = _viewport3.Camera as T;

        camera.LookDirection = CameraLookPosition;
    }

    public void Look()
    {
        var camera = _viewport3.Camera as T;

        camera.LookDirection = CameraLookPosition;
    }

    public FrameworkElement EventSource
    {
        get { return _eventSource; }

        set
        {
            if (_eventSource != null)
            {
                _eventSource.MouseDown -= this.OnMouseDown;
                _eventSource.MouseUp -= this.OnMouseUp;
                _eventSource.MouseMove -= this.OnMouseMove;
                _eventSource.MouseWheel -= this.OnMouseWheelMove;
            }

            _eventSource = value;

            _eventSource.MouseDown += this.OnMouseDown;
            _eventSource.MouseUp += this.OnMouseUp;
            _eventSource.MouseMove += this.OnMouseMove;
            _eventSource.MouseWheel += this.OnMouseWheelMove;
        }
    }

    private void OnMouseWheelMove(object _sender, MouseWheelEventArgs _e)
    {
        double zoomValue;
        double zoomFactor = 1.1;
        if (_e.Delta < 0)
        {
            zoomValue = zoomFactor;
        }
        else
        {
            zoomValue = 1 / zoomFactor;
        }
        if (_viewport3 != null)
        {
            if (_viewport3.Camera != null)
            {
                if (_viewport3.Camera.IsFrozen)
                {
                    _viewport3.Camera = _viewport3.Camera.Clone();
                }
                var transform3DGroup = new Transform3DGroup();
                transform3DGroup.Children.Add(_viewport3.Camera.Transform);
                //Vector3D projectToTrackball = ProjectToTrackball(ActualWidth, ActualHeight, e.GetPosition(this));
                //transform3DGroup.Children.Add(new ScaleTransform3D(new Vector3D(zoomValue, zoomValue, zoomValue), new Point3D(projectToTrackball.X, projectToTrackball.Y, projectToTrackball.Z)));
                //viewport3D.Camera.Transform = transform3DGroup;
            }
        }

    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        Mouse.Capture(EventSource, CaptureMode.Element);
        _position = e.GetPosition(EventSource);
    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
        Mouse.Capture(EventSource, CaptureMode.None);
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        Point currentPosition = e.GetPosition(EventSource);

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            _translate.OffsetZ -= (_position.Y - currentPosition.Y) * 0.2;
            _translate.OffsetY -= (_position.X - currentPosition.X) * 0.2;
        }
        else if (e.RightButton == MouseButtonState.Pressed)
        {
            _translate.OffsetX += (_position.X - currentPosition.X);
        }

        _position = currentPosition;
    }
}

