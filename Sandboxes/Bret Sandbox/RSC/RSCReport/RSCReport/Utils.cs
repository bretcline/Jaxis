using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Resources;
using System.Xml.Linq;
using DevExpress.Charts.Native;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Bars;
using System.Globalization;

namespace RSCReport
{

    public static class DataLoader
    {
        public static XDocument LoadXmlFromResources(string fileName)
        {
            try
            {
                return XDocument.Load("/ChartsDemo;component" + fileName);
            }
            catch
            {
                return null;
            }
        }
    }

    public static class ResolveOverlappingModeHelper
    {
        public static void PrepareListBox(ListBoxEdit listBox, int index)
        {
            listBox.Items.Add("None");
            listBox.Items.Add("Default");
            listBox.Items.Add("Hide Overlapped");
            listBox.Items.Add("Justify Around Point");
            listBox.Items.Add("Justify All Around Point");
            listBox.SelectedIndex = index;
        }

        public static ResolveOverlappingMode GetMode(ListBoxEdit listBox)
        {
            switch (listBox.SelectedIndex)
            {
                case 0:
                    return ResolveOverlappingMode.None;
                case 1:
                    return ResolveOverlappingMode.Default;
                case 2:
                    return ResolveOverlappingMode.HideOverlapped;
                case 3:
                    return ResolveOverlappingMode.JustifyAroundPoint;
                case 4:
                    return ResolveOverlappingMode.JustifyAllAroundPoint;
                default:
                    return ResolveOverlappingMode.None;
            }
        }
    }

    public static class Marker2DModelKindHelper
    {
        public static Marker2DKind FindActualMarker2DModelKind(Type modelType)
        {
            IEnumerable<Marker2DKind> marker2DKinds = Marker2DModel.GetPredefinedKinds();
            foreach (Marker2DKind marker2DKind in marker2DKinds)
            {
                if (Equals(marker2DKind.Type, modelType))
                    return marker2DKind;
            }
            return null;
        }
    }

    public static class Pie2DModelKindHelper
    {
        public static Pie2DKind FindActualPie2DModelKind(Type modelType)
        {
            IEnumerable<Pie2DKind> pie2DKinds = Pie2DModel.GetPredefinedKinds();
            foreach (Pie2DKind pie2DType in pie2DKinds)
            {
                if (Equals(pie2DType.Type, modelType))
                    return pie2DType;
            }
            return null;
        }
    }

    public static class Bar2DModelKindHelper
    {
        public static Bar2DKind FindActualBar2DModelKind(Type modelType)
        {
            IEnumerable<Bar2DKind> bar2DKinds = Bar2DModel.GetPredefinedKinds();
            foreach (Bar2DKind bar2DKind in bar2DKinds)
            {
                if (Equals(bar2DKind.Type, modelType))
                    return bar2DKind;
            }
            return null;
        }
    }

    public static class Stock2DModelKindHelper
    {
        public static Stock2DKind FindActualStock2DModelKind(Type modelType)
        {
            IEnumerable<Stock2DKind> stock2DKinds = Stock2DModel.GetPredefinedKinds();
            foreach (Stock2DKind stock2DKind in stock2DKinds)
            {
                if (Equals(stock2DKind.Type, modelType))
                    return stock2DKind;
            }
            return null;
        }
    }

    public static class CandleStick2DModelKindHelper
    {
        public static CandleStick2DKind FindActualCandleStick2DModelKind(Type modelType)
        {
            IEnumerable<CandleStick2DKind> candleStick2DKinds = CandleStick2DModel.GetPredefinedKinds();
            foreach (CandleStick2DKind candleStick2DKind in candleStick2DKinds)
            {
                if (Equals(candleStick2DKind.Type, modelType))
                    return candleStick2DKind;
            }
            return null;
        }
    }

    public class FinancialPoint : DependencyObject
    {
        public string Argument { get; set; }

        public double HighValue { get; set; }

        public double LowValue { get; set; }

        public double OpenValue { get; set; }

        public double CloseValue { get; set; }
    }

    public class IndustryBubblePoint : DependencyObject
    {
        public static readonly DependencyProperty NameProperty;
        public static readonly DependencyProperty NumberOfCasesProperty;
        public static readonly DependencyProperty RateProperty;

        static IndustryBubblePoint()
        {
            Type ownerType = typeof(IndustryBubblePoint);
            NameProperty = DependencyProperty.Register("Name", typeof(string), ownerType,
                                                       new PropertyMetadata(String.Empty));
            NumberOfCasesProperty = DependencyProperty.Register("NumberOfCases", typeof(int), ownerType,
                                                                new PropertyMetadata(0));
            RateProperty = DependencyProperty.Register("Rate", typeof(double), ownerType, new PropertyMetadata(0.0));
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public int NumberOfCases
        {
            get { return (int)GetValue(NumberOfCasesProperty); }
            set { SetValue(NumberOfCasesProperty, value); }
        }

        public double Rate
        {
            get { return (double)GetValue(RateProperty); }
            set { SetValue(RateProperty, value); }
        }
    }

    public class SeriesTypeItem
    {
        private readonly Type diagramType;
        private readonly int seriesCount;
        private readonly string seriesName;
        private readonly Type seriesType;

        public SeriesTypeItem(Type diagramType, Type seriesType, string seriesName)
            : this(diagramType, seriesType, seriesName, 1)
        {
        }

        public SeriesTypeItem(Type diagramType, Type seriesType, string seriesName, int seriesCount)
        {
            this.diagramType = diagramType;
            this.seriesType = seriesType;
            this.seriesName = seriesName;
            this.seriesCount = seriesCount;
        }

        public Type DiagramType
        {
            get { return diagramType; }
        }

        public Type SeriesType
        {
            get { return seriesType; }
        }

        public int SeriesCount
        {
            get { return seriesCount; }
        }

        public override string ToString()
        {
            return seriesName;
        }
    }

    public class DemoValuesProvider
    {
        public IEnumerable<Bubble2DLabelPosition> Bubble2DLabelPositions
        {
            get { return EnumHelper.GetValues(typeof(Bubble2DLabelPosition)).Cast<Bubble2DLabelPosition>(); }
        }

        public IEnumerable<Bar2DLabelPosition> Bar2DLabelPositions
        {
            get { return EnumHelper.GetValues(typeof(Bar2DLabelPosition)).Cast<Bar2DLabelPosition>(); }
        }

        public IEnumerable<Bar2DKind> PredefinedBar2DKinds
        {
            get { return Bar2DModel.GetPredefinedKinds(); }
        }

        public IEnumerable<Marker2DKind> PredefinedMarker2DKinds
        {
            get { return Marker2DModel.GetPredefinedKinds(); }
        }

        public IEnumerable<CandleStick2DKind> PredefinedCandleStick2DKinds
        {
            get { return CandleStick2DModel.GetPredefinedKinds(); }
        }

        public IEnumerable<Stock2DKind> PredefinedStock2DKinds
        {
            get { return Stock2DModel.GetPredefinedKinds(); }
        }

        public IEnumerable<Pie2DKind> PredefinedPie2DKinds
        {
            get { return Pie2DModel.GetPredefinedKinds(); }
        }

        public IEnumerable<ScrollBarAlignment> ScrollBarAlignments
        {
            get { return EnumHelper.GetValues(typeof(ScrollBarAlignment)).Cast<ScrollBarAlignment>(); }
        }
    }

    public class Bar2DKindToTickmarksLengthConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bar2DKind = value as Bar2DKind;
            if (bar2DKind != null)
            {
                switch (bar2DKind.Name)
                {
                    case "Glass Cylinder":
                        return 18;
                    case "Quasi-3D Bar":
                        return 9;
                    default:
                        return 5;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class Bar2DKindToBar2DModelConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bar2DKind = value as Bar2DKind;
            if (bar2DKind != null)
                return Activator.CreateInstance(bar2DKind.Type);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class Marker2DKindToMarker2DModelConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var marker2DKind = value as Marker2DKind;
            if (marker2DKind != null)
                return Activator.CreateInstance(marker2DKind.Type);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class CandleStick2DKindToCandleStick2DModelConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var candleStick2DKind = value as CandleStick2DKind;
            if (candleStick2DKind != null)
                return Activator.CreateInstance(candleStick2DKind.Type);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class Stock2DKindToStock2DModelConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stock2DKind = value as Stock2DKind;
            if (stock2DKind != null)
                return Activator.CreateInstance(stock2DKind.Type);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class Pie2DKindToPie2DModelConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pie2DKind = value as Pie2DKind;
            if (pie2DKind != null)
                return Activator.CreateInstance(pie2DKind.Type);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class MarkerSizeToLabelIndentConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value) / 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

    public class IsCheckedToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

}