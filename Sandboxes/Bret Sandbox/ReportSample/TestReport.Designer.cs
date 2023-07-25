namespace ReportSample
{
    partial class TestReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand( );
            this.wellAdapter1 = new ReportSample.TestReportDataTableAdapters.WellAdapter( );
            this.testReportData1 = new ReportSample.TestReportData( );
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle( );
            this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle( );
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle( );
            this.DataField = new DevExpress.XtraReports.UI.XRControlStyle( );
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel( );
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine( );
            this.pageFooterBand1 = new DevExpress.XtraReports.UI.PageFooterBand( );
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo( );
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo( );
            this.reportHeaderBand1 = new DevExpress.XtraReports.UI.ReportHeaderBand( );
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel( );
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand( );
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand( );
            ( (System.ComponentModel.ISupportInitialize)( this.testReportData1 ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this ) ).BeginInit( );
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange( new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel2,
            this.xrLabel3,
            this.xrLabel4,
            this.xrLabel5,
            this.xrLabel6,
            this.xrLabel7,
            this.xrLabel8,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel11,
            this.xrLabel12,
            this.xrLine1} );
            this.Detail.HeightF = 152F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo( 0, 0, 0, 0, 100F );
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // wellAdapter1
            // 
            this.wellAdapter1.ClearBeforeFill = true;
            // 
            // testReportData1
            // 
            this.testReportData1.DataSetName = "TestReportData";
            this.testReportData1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.White;
            this.Title.BorderColor = System.Drawing.SystemColors.ControlText;
            this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Title.BorderWidth = 1;
            this.Title.Font = new System.Drawing.Font( "Times New Roman", 20F, System.Drawing.FontStyle.Bold );
            this.Title.ForeColor = System.Drawing.Color.Maroon;
            this.Title.Name = "Title";
            // 
            // FieldCaption
            // 
            this.FieldCaption.BackColor = System.Drawing.Color.White;
            this.FieldCaption.BorderColor = System.Drawing.SystemColors.ControlText;
            this.FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.FieldCaption.BorderWidth = 1;
            this.FieldCaption.Font = new System.Drawing.Font( "Arial", 10F, System.Drawing.FontStyle.Bold );
            this.FieldCaption.ForeColor = System.Drawing.Color.Maroon;
            this.FieldCaption.Name = "FieldCaption";
            // 
            // PageInfo
            // 
            this.PageInfo.BackColor = System.Drawing.Color.White;
            this.PageInfo.BorderColor = System.Drawing.SystemColors.ControlText;
            this.PageInfo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.PageInfo.BorderWidth = 1;
            this.PageInfo.Font = new System.Drawing.Font( "Times New Roman", 10F, System.Drawing.FontStyle.Bold );
            this.PageInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PageInfo.Name = "PageInfo";
            // 
            // DataField
            // 
            this.DataField.BackColor = System.Drawing.Color.White;
            this.DataField.BorderColor = System.Drawing.SystemColors.ControlText;
            this.DataField.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.DataField.BorderWidth = 1;
            this.DataField.Font = new System.Drawing.Font( "Times New Roman", 10F );
            this.DataField.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DataField.Name = "DataField";
            this.DataField.Padding = new DevExpress.XtraPrinting.PaddingInfo( 2, 2, 0, 0, 100F );
            // 
            // xrLabel1
            // 
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat( 6F, 9F );
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.SizeF = new System.Drawing.SizeF( 162F, 18F );
            this.xrLabel1.StyleName = "FieldCaption";
            this.xrLabel1.Text = "Well ID";
            // 
            // xrLabel2
            // 
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat( 6F, 33F );
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.SizeF = new System.Drawing.SizeF( 162F, 18F );
            this.xrLabel2.StyleName = "FieldCaption";
            this.xrLabel2.Text = "Lease ID";
            // 
            // xrLabel3
            // 
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat( 6F, 57F );
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.SizeF = new System.Drawing.SizeF( 162F, 18F );
            this.xrLabel3.StyleName = "FieldCaption";
            this.xrLabel3.Text = "Field ID";
            // 
            // xrLabel4
            // 
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat( 6F, 81F );
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.SizeF = new System.Drawing.SizeF( 162F, 18F );
            this.xrLabel4.StyleName = "FieldCaption";
            this.xrLabel4.Text = "Name";
            // 
            // xrLabel5
            // 
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat( 6F, 105F );
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.SizeF = new System.Drawing.SizeF( 162F, 18F );
            this.xrLabel5.StyleName = "FieldCaption";
            this.xrLabel5.Text = "Identifier";
            // 
            // xrLabel6
            // 
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat( 6F, 129F );
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.SizeF = new System.Drawing.SizeF( 162F, 18F );
            this.xrLabel6.StyleName = "FieldCaption";
            this.xrLabel6.Text = "Customer ID";
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange( new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Well.WellID")} );
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat( 174F, 9F );
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.SizeF = new System.Drawing.SizeF( 470F, 18F );
            this.xrLabel7.StyleName = "DataField";
            // 
            // xrLabel8
            // 
            this.xrLabel8.DataBindings.AddRange( new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Well.LeaseID")} );
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat( 174F, 33F );
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.SizeF = new System.Drawing.SizeF( 470F, 18F );
            this.xrLabel8.StyleName = "DataField";
            // 
            // xrLabel9
            // 
            this.xrLabel9.DataBindings.AddRange( new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Well.FieldID")} );
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat( 174F, 57F );
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.SizeF = new System.Drawing.SizeF( 470F, 18F );
            this.xrLabel9.StyleName = "DataField";
            // 
            // xrLabel10
            // 
            this.xrLabel10.DataBindings.AddRange( new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Well.Name")} );
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat( 174F, 81F );
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.SizeF = new System.Drawing.SizeF( 470F, 18F );
            this.xrLabel10.StyleName = "DataField";
            // 
            // xrLabel11
            // 
            this.xrLabel11.DataBindings.AddRange( new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Well.Identifier")} );
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat( 174F, 105F );
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.SizeF = new System.Drawing.SizeF( 470F, 18F );
            this.xrLabel11.StyleName = "DataField";
            // 
            // xrLabel12
            // 
            this.xrLabel12.DataBindings.AddRange( new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Well.CustomerID")} );
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat( 174F, 129F );
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.SizeF = new System.Drawing.SizeF( 470F, 18F );
            this.xrLabel12.StyleName = "DataField";
            // 
            // xrLine1
            // 
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat( 6F, 3F );
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF( 638F, 2F );
            // 
            // pageFooterBand1
            // 
            this.pageFooterBand1.Controls.AddRange( new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrPageInfo2} );
            this.pageFooterBand1.HeightF = 29F;
            this.pageFooterBand1.Name = "pageFooterBand1";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat( 6F, 6F );
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF( 313F, 23F );
            this.xrPageInfo1.StyleName = "PageInfo";
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Format = "Page {0} of {1}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat( 331F, 6F );
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF( 313F, 23F );
            this.xrPageInfo2.StyleName = "PageInfo";
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // reportHeaderBand1
            // 
            this.reportHeaderBand1.Controls.AddRange( new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13} );
            this.reportHeaderBand1.HeightF = 51F;
            this.reportHeaderBand1.Name = "reportHeaderBand1";
            // 
            // xrLabel13
            // 
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat( 6F, 6F );
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.SizeF = new System.Drawing.SizeF( 638F, 33F );
            this.xrLabel13.StyleName = "Title";
            this.xrLabel13.Text = "TestReport";
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // TestReport
            // 
            this.Bands.AddRange( new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.pageFooterBand1,
            this.reportHeaderBand1,
            this.topMarginBand1,
            this.bottomMarginBand1} );
            this.DataAdapter = this.wellAdapter1;
            this.DataMember = "Well";
            this.DataSource = this.testReportData1;
            this.StyleSheet.AddRange( new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField} );
            this.Version = "10.1";
            ( (System.ComponentModel.ISupportInitialize)( this.testReportData1 ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this ) ).EndInit( );

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private ReportSample.TestReportDataTableAdapters.WellAdapter wellAdapter1;
        private TestReportData testReportData1;
        private DevExpress.XtraReports.UI.XRControlStyle Title;
        private DevExpress.XtraReports.UI.XRControlStyle FieldCaption;
        private DevExpress.XtraReports.UI.XRControlStyle PageInfo;
        private DevExpress.XtraReports.UI.XRControlStyle DataField;
        private DevExpress.XtraReports.UI.PageFooterBand pageFooterBand1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.UI.ReportHeaderBand reportHeaderBand1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
