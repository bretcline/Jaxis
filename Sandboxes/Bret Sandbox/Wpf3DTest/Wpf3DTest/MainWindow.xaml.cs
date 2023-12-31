﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Wpf3DTest {

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		private GeometryModel3D mGeometry;
		private bool mDown;
		private Point mLastPos;

		public MainWindow() {
			InitializeComponent();

			BuildSolid();
		}

		private void BuildSolid() {
            // Define 3D mesh object
            MeshGeometry3D mesh = new MeshGeometry3D();

            mesh.Positions.Add(new Point3D(-0.5, -0.5, 1));
            //mesh.Normals.Add(new Vector3D(0, 0, 1));
            mesh.Positions.Add(new Point3D(0.5, -0.5, 1));
            //mesh.Normals.Add(new Vector3D(0, 0, 1));
            mesh.Positions.Add(new Point3D(0.5, 0.5, 1));
            //mesh.Normals.Add(new Vector3D(0, 0, 1));
            mesh.Positions.Add(new Point3D(-0.5, 0.5, 1));
            //mesh.Normals.Add(new Vector3D(0, 0, 1));

            mesh.Positions.Add(new Point3D(-1, -1, -1));
            //mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Positions.Add(new Point3D(1, -1, -1));
            //mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Positions.Add(new Point3D(1, 1, -1));
            //mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Positions.Add(new Point3D(-1, 1, -1));
            //mesh.Normals.Add(new Vector3D(0, 0, -1));

            // Front face
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);

            // Back face
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(6);

            // Right face
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(2);

            // Top face
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(7);

            // Bottom face
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(5);

            // Right face
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(4);

            // Geometry creation
            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.YellowGreen));
            mGeometry.Transform = new Transform3DGroup();


            var import = new ModelImporter();
            group = import.Load(@"C:\Source\Jaxis\trunk\Sandboxes\Bret Sandbox\Wpf3DTest\Teeth.obj");


			group.Children.Add(mGeometry);
		}

		private void Grid_MouseWheel(object sender, MouseWheelEventArgs e) {
			camera.Position = new Point3D(camera.Position.X, camera.Position.Y, camera.Position.Z - e.Delta / 250D);
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			camera.Position = new Point3D(camera.Position.X, camera.Position.Y, 5);
			mGeometry.Transform = new Transform3DGroup();
		}

		private void Grid_MouseMove(object sender, MouseEventArgs e) {
			if(mDown) {
				Point pos = Mouse.GetPosition(viewport);
				Point actualPos = new Point(pos.X - viewport.ActualWidth / 2, viewport.ActualHeight / 2 - pos.Y);
				double dx = actualPos.X - mLastPos.X, dy = actualPos.Y - mLastPos.Y;

				double mouseAngle = 0;
				if(dx != 0 && dy != 0) {
					mouseAngle = Math.Asin(Math.Abs(dy) / Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2)));
					if(dx < 0 && dy > 0) mouseAngle += Math.PI / 2;
					else if(dx < 0 && dy < 0) mouseAngle += Math.PI;
					else if(dx > 0 && dy < 0) mouseAngle += Math.PI * 1.5;
				}
				else if(dx == 0 && dy != 0) mouseAngle = Math.Sign(dy) > 0 ? Math.PI / 2 : Math.PI * 1.5;
				else if(dx != 0 && dy == 0) mouseAngle = Math.Sign(dx) > 0 ? 0 : Math.PI;

				double axisAngle = mouseAngle + Math.PI / 2;

				Vector3D axis = new Vector3D(Math.Cos(axisAngle) * 4, Math.Sin(axisAngle) * 4, 0);

				double rotation = 0.01 * Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));

				Transform3DGroup group = mGeometry.Transform as Transform3DGroup;
				QuaternionRotation3D r = new QuaternionRotation3D(new Quaternion(axis, rotation * 180 / Math.PI));
				group.Children.Add(new RotateTransform3D(r));

				mLastPos = actualPos;
			}
		}

		private void Grid_MouseDown(object sender, MouseButtonEventArgs e) {
			if(e.LeftButton != MouseButtonState.Pressed) return;
			mDown = true;
			Point pos = Mouse.GetPosition(viewport);
			mLastPos = new Point(pos.X - viewport.ActualWidth / 2, viewport.ActualHeight / 2 - pos.Y);
		}

		private void Grid_MouseUp(object sender, MouseButtonEventArgs e) {
			mDown = false;
		}

	}

}
