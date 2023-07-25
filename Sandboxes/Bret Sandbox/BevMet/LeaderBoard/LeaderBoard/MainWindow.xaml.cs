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
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Layout.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Gauges;


namespace LeaderBoard {
    public partial class MainWindow : DXWindow {
        public MainWindow() {
            InitializeComponent();
            
        }
    }

    public class TestData {
		public string Text {get; set;}
		public decimal Number {get; set;}
    }

	public class SourceList : List<TestData> {
    }
}
