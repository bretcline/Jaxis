using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ElevationControl
{
    public partial class MapElevation : UserControl
    {
        public MapElevation()
        {
            InitializeComponent();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            //var rec = new Rectangle();
            //rec.Height = 100;
            //rec.Width = 25;

            //var solidBlack = new SolidColorBrush(Colors.Black);
            //var solidRed = new SolidColorBrush(Colors.Red);

            //rec.StrokeThickness = 4;
            //rec.Stroke = solidBlack;
            //rec.Fill = solidRed;
        }
    }
}
