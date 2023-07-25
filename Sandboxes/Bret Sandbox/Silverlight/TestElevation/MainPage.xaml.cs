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

namespace TestElevation
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void recGradient_Loaded(object sender, RoutedEventArgs e)
        {
            var solidBlack = new LinearGradientBrush();
            var solidRed = new LinearGradientBrush();

            solidRed.SetValue(LinearGradientBrush.StartPointProperty, ("0,0"));
            solidRed.GradientStops[0].Offset = 0.5;

            solidRed.SetValue(LinearGradientBrush.StartPointProperty, ("1,1"));
            solidRed.GradientStops[0].Color = Colors.Red;
            solidRed.GradientStops[1].Color = Colors.Blue;

            recGradient.Fill = solidRed;

        }
    }
}
