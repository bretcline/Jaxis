using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;

namespace PieChart
{
    public partial class Form1 : Form
    {

        public class DataPoints
        {
            public string Name { get; set; }
            public double Value { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            chartControl1.Series[0].Points.Add(new SeriesPoint("Beer", 564.54));
            chartControl1.Series[0].Points.Add(new SeriesPoint("Wine", 454.54));
            chartControl1.Series[0].Points.Add(new SeriesPoint("Liquor", 164.54));

        }
    }
}
