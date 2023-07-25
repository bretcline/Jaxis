using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;

namespace RSCReport
{
    public partial class MainPage : UserControl
    {
        const int clickDelta = 200;

        DateTime mouseDownTime;
        bool rotate;
        Point startPosition;

        List<string> m_TopCompanies = new List<string> { "Carry Deck Crane-8.5 Ton", "Skidsteer Track Loader-2000-2600 Lb", "Strmst Forklift-6000Lb-2Wd 14-22", "Light Tower-4-6Kw-4 Lamp-Towable", "Art Boom Lift-60-64'-2Wd" };
        List<string> m_3rdParty = new List<string> { "Air Conditioner, 80 Ton Package", "Generator: 300 KW", "500 BBL mix tank w/coils", "Pump, Acc., Hose,OS&D Dock Hose;HP; 12\" x 20", "Oil Free Air Comp-PTS 916-16" };
        List<string> m_3rdCompanies = new List<string> { "Texas Gulf Refrigeration Inc", "National Pump & Compressor LP", "Abrasive Products & Equipment", "NTS Mikedon LLC", "Rain for Rent" };
        List<ChartControl> m_3rdControls = null;
        ToolTip m_Tooltip = new ToolTip();

        public MainPage()
        {
            InitializeComponent();
            m_3rdControls = new List<ChartControl> { chartTop1, chartTop2, chartTop3, chartTop4, chartTop5 };

            CreateTopFiveChart();
            CreateSpendChart();
            for( int i = 0; i < m_3rdCompanies.Count; ++i )
            {
                CreateSpendByVendorChart( m_3rdCompanies[i], m_3rdControls[i] );
            }
            m_Tooltip.Name = "ttContent";
        }

        private void CreateSpendByVendorChart(string _company, ChartControl _chartControl)
        {

            XYDiagram2D diagram = new XYDiagram2D();
            diagram.AxisY = new AxisY2D {Range = new AxisRange{ MaxValue = 1000, MinValue = 0}, Title = new AxisTitle {Content = "In $1000"}};
            diagram.AxisX = new AxisX2D {GridLinesVisible = true};
            diagram.Visibility = Visibility.Visible;

            var series = new BarSideBySideSeries2D();
            var model = new OutsetBar2DModel();
            series.Model = model;
            series.DisplayName = "Items";
            series.ShowInLegend = false;
            series.ArgumentScaleType = ScaleType.Qualitative;
            series.Visible = true;
            series.ArgumentDataMember = "Argument";
            series.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState;
            series.Label = new SeriesLabel();
            series.Label.Indent = 3;
            series.Label.Visible = false;
            series.Label.ConnectorVisible = false;
            series.Label.ConnectorThickness = 1;

            object max;
            object min;
            
            series.Points.AddRange(GenerateVendorSpendData(_company, out max, out min) ); 

//            diagram.AxisY.Range.MaxValue = max;
//            diagram.AxisY.Range.MinValue = min;
            
            string company = string.Format("{0}", _company);
            _chartControl.Titles.Add(new Title { Content = company, HorizontalAlignment = HorizontalAlignment.Center, FontSize = 12 } );
            
            diagram.Series.Add(series);

            _chartControl.Diagram = diagram;
        }

        private void CreateTopFiveChart()
        {
            topFive.MouseLeftButtonUp += new MouseButtonEventHandler(topFive_MouseLeftButtonUp);
            topFive.MouseLeftButtonDown += new MouseButtonEventHandler(topFive_MouseLeftButtonDown);
            topFive.BorderThickness = new Thickness( 0 );
            topFive.EnableAnimation = true;

            SimpleDiagram2D diagram = new SimpleDiagram2D();
            diagram.Visibility = Visibility.Visible;

            PieSeries2D series = new PieSeries2D();
            series.Model = new SimplePie2DModel();
            series.DisplayName = "Items";
            series.ShowInLegend = false;
            series.ArgumentScaleType = ScaleType.Qualitative;
            series.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState;
            series.HoleRadiusPercent = 0.0;
            series.Rotation = 0.0;
            series.SweepDirection = PieSweepDirection.Clockwise;
            series.Visible = true;
            series.PointAnimation = new Pie2DFlyInAnimation() {Duration = new TimeSpan(0, 0, 0, 1), PointOrder = PointAnimationOrder.Random};
            series.PointOptions = new PointOptions();
            series.PointOptions.ValueNumericOptions = new NumericOptions();
            series.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            series.PointOptions.ValueNumericOptions.Precision = 0;
            series.Label = new SeriesLabel();
            series.Label.Visible = true;
            series.Label.Indent = 3;
            series.Label.RenderMode = LabelRenderMode.RectangleConnectedToCenter;
            series.BorderThickness = new Thickness(2.0, 2.0, 2.0, 2.0);

            diagram.BorderThickness = new Thickness(2.0, 2.0, 2.0, 2.0);

            series.Points.AddRange( GenerateTopFiveData() );

            diagram.Series.Add(series);

            topFive.Diagram = diagram;
        }

        private void CreateSpendChart()
        {
            XYDiagram2D diagram = new XYDiagram2D();
            diagram.AxisY = new AxisY2D();
            diagram.AxisY.Range = new AxisRange();
            diagram.AxisY.Range.MaxValue = 20;
            diagram.AxisY.Range.MinValue = 0;
            diagram.AxisY.Title = new AxisTitle();
            diagram.AxisY.Title.Name = "US Dollars";
            diagram.AxisX = new AxisX2D();
            diagram.AxisX.GridLinesVisible = true;
            diagram.Visibility = Visibility.Visible;

            CandleStickSeries2D series = new CandleStickSeries2D();
            BorderCandleStick2DModel model = new BorderCandleStick2DModel();
            series.Model = model;
            series.DisplayName = "Items";
            series.ShowInLegend = false;
            series.ArgumentScaleType = ScaleType.DateTime;
            series.CandleWidth = 0.3;
            series.Visible = true;
            series.ArgumentDataMember = "Argument";
            series.HighValueDataMember = "HighValue";
            series.LowValueDataMember = "LowValue";
            series.OpenValueDataMember = "OpenValue";
            series.CloseValueDataMember = "CloseValue";
            series.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState;
            series.Label = new SeriesLabel();
            series.Label.Indent = 3;
            series.Label.Visible = false;

            series.DataSource = GenerateSpendData();

            gridData.ItemsSource = series.DataSource;

            diagram.Series.Add(series);

            spendChart.Titles.Add(new Title{ Content = "Top 5 Spend Data", HorizontalAlignment = HorizontalAlignment.Center});

            spendChart.Diagram = diagram;
            series.DataSource = GenerateSpendData();
        }


        bool IsClick(DateTime mouseUpTime)
        {
            return (mouseUpTime - mouseDownTime).TotalMilliseconds < clickDelta;
        }

        void topFive_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseDownTime = DateTime.Now;
            Point position = e.GetPosition(topFive);
            ChartHitInfo hitInfo = topFive.CalcHitInfo(position);
            if (hitInfo != null && hitInfo.SeriesPoint != null)
            {
                rotate = true;
                startPosition = position;
            }
        }

        void topFive_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChartHitInfo hitInfo = topFive.CalcHitInfo(e.GetPosition(topFive));
            rotate = false;
            if (hitInfo == null || hitInfo.SeriesPoint == null || !IsClick(DateTime.Now))
                return;
            double distance = PieSeries.GetExplodedDistance(hitInfo.SeriesPoint);
            Storyboard storyBoard = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 300));
            animation.To = distance > 0 ? 0 : 0.3;
            storyBoard.Children.Add(animation);
            Storyboard.SetTarget(animation, hitInfo.SeriesPoint);
            Storyboard.SetTargetProperty(animation, new PropertyPath(PieSeries.ExplodedDistanceProperty));
            storyBoard.Begin();
        }

        private List<SeriesPoint> GenerateTopFiveData()
        {
            List<SeriesPoint> rc = new List<SeriesPoint>();
            var point = new SeriesPoint {Argument = m_TopCompanies[0], Value = 167050.0};
            rc.Add(point);

            point = new SeriesPoint { Argument = m_TopCompanies[1], Value = 147927.0 };
            rc.Add(point);

            point = new SeriesPoint { Argument = m_TopCompanies[2], Value = 110285.0 };
            rc.Add(point);

            point = new SeriesPoint { Argument = m_TopCompanies[3], Value = 71627.0 };
            rc.Add(point);

            point = new SeriesPoint { Argument = m_TopCompanies[4], Value = 60329.0 };
            rc.Add(point);

            point = new SeriesPoint { Argument = "Other", Value = 2141456.31 };
            rc.Add(point);

            return rc;

        }


        private List<SeriesPoint> GenerateVendorSpendData(string _company, out object _max, out object _min)
        {
            List<SeriesPoint> rc = new List<SeriesPoint>();

            _min = 0;

            if (_company == m_3rdCompanies[0])
            {
                rc.Add(new SeriesPoint { Argument = "Jan 2011", Value = 245});
                rc.Add(new SeriesPoint { Argument = "Feb 2011", Value = 294});
                rc.Add(new SeriesPoint { Argument = "Mar 2011", Value = 203});
            }
            else if (_company == m_3rdCompanies[1])
            {
                rc.Add(new SeriesPoint { Argument = "Jan 2011", Value = 242});
                rc.Add(new SeriesPoint { Argument = "Feb 2011", Value = 243});
                rc.Add(new SeriesPoint { Argument = "Mar 2011", Value = 158});
            }
            else if (_company == m_3rdCompanies[2])
            {
                rc.Add(new SeriesPoint { Argument = "Jan 2011", Value = 550});
                rc.Add(new SeriesPoint { Argument = "Feb 2011", Value = 840});
                rc.Add(new SeriesPoint { Argument = "Mar 2011", Value = 615});
            }
            else if (_company == m_3rdCompanies[3])
            {
                rc.Add(new SeriesPoint { Argument = "Jan 2011", Value = 580});
                rc.Add(new SeriesPoint { Argument = "Feb 2011", Value = 590});
                rc.Add(new SeriesPoint { Argument = "Mar 2011", Value = 380});
            }
            else if (_company == m_3rdCompanies[4])
            {
                rc.Add(new SeriesPoint { Argument = "Jan 2011", Value = 560});
                rc.Add(new SeriesPoint { Argument = "Feb 2011", Value = 385});
                rc.Add(new SeriesPoint { Argument = "Mar 2011", Value = 530});
            }

            _max = rc.Max(f => f.Value)*1.1;

            return rc;
        }

        private static List<FinancialPoint> GenerateSpendData()
        {
            List<FinancialPoint> rc = new List<FinancialPoint>();

            Random rnd = new Random();
            DateTime time = DateTime.Now;
            for (int i = 0; i < 10; ++i)
            {
                List<double> values = new List<double> { rnd.NextDouble() * 10, rnd.NextDouble() * 10, rnd.NextDouble() * 10, rnd.NextDouble() * 10 };

                FinancialPoint point = new FinancialPoint
                {
                    Argument =
                        string.Format("{0}/{1}/{2}", time.Month, time.Day - 10 + i, time.Year),
                    LowValue = values.Min(),
                    HighValue = values.Max()
                };
                point.OpenValue = point.LowValue * 1.5;
                point.CloseValue = point.LowValue * 2.5;
                rc.Add(point);
            }
            return rc;
        }

        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(spendChart);
            ChartHitInfo hitInfo = spendChart.CalcHitInfo(position);
            if (hitInfo != null && hitInfo.SeriesPoint != null)
            {
                m_Tooltip.Content = string.Format("Date = {0}\nLow = {1}\nHigh = {2}\nOpen = {3}\nClose = {4}",
                    hitInfo.SeriesPoint.Argument, Math.Round(CandleStickSeries2D.GetLowValue(hitInfo.SeriesPoint), 2),
                    Math.Round(CandleStickSeries2D.GetHighValue(hitInfo.SeriesPoint), 2),
                    Math.Round(CandleStickSeries2D.GetOpenValue(hitInfo.SeriesPoint), 2),
                    Math.Round(CandleStickSeries2D.GetCloseValue(hitInfo.SeriesPoint), 2));
                m_Tooltip.HorizontalOffset = position.X;
                m_Tooltip.VerticalOffset = position.Y;
                m_Tooltip.IsOpen = true;
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void chart_MouseLeave(object sender, MouseEventArgs e)
        {
            m_Tooltip.IsOpen = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            topFive.Animate();
            spendChart.Animate();
            foreach (var ctrl in m_3rdControls)
            {
                ctrl.Animate();
            }
        }

        private void NavBarItem_Click(object sender, EventArgs e)
        {
            SetVisibility( System.Windows.Visibility.Collapsed );
        }

        private void SetVisibility(System.Windows.Visibility visibility)
        {
            chartTop1.Visibility = visibility;
            chartTop2.Visibility = visibility;
            chartTop3.Visibility = visibility;
            chartTop4.Visibility = visibility;
            chartTop5.Visibility = visibility;

            spendChart.Visibility = visibility;
            topFive.Visibility = visibility;
            gridData.Visibility = visibility;
        }

        private void TopRSC_Click(object sender, EventArgs e)
        {
            SetVisibility(System.Windows.Visibility.Visible );
        }
        
    }
}
