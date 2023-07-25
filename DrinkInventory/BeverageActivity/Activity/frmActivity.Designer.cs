using BeverageActivity.Properties;

namespace BeverageActivity.Forms.Activity
{
    partial class frmActivity
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.circularGauge2 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            this.arcScaleBackgroundLayerComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.arcScaleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.arcScaleNeedleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
            this.arcScaleSpindleCapComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent();
            this.circularGauge3 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            this.arcScaleBackgroundLayerComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.arcScaleComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.arcScaleNeedleComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
            this.arcScaleSpindleCapComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent();
            this.ttcTooltips = new DevExpress.Utils.ToolTipController();
            this.docActivities = new DevExpress.XtraBars.Docking.DockManager();
            this.dpnlWidgets = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer4 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.clstWidgets = new DevExpress.XtraEditors.CheckedListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleSpindleCapComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleSpindleCapComponent3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.docActivities)).BeginInit();
            this.dpnlWidgets.SuspendLayout();
            this.controlContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clstWidgets)).BeginInit();
            this.SuspendLayout();
            // 
            // circularGauge2
            // 
            this.circularGauge2.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[] {
            this.arcScaleBackgroundLayerComponent2});
            this.circularGauge2.Bounds = new System.Drawing.Rectangle(6, 6, 501, 138);
            this.circularGauge2.Name = "circularGauge2";
            this.circularGauge2.Needles.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent[] {
            this.arcScaleNeedleComponent2});
            this.circularGauge2.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[] {
            this.arcScaleComponent2});
            this.circularGauge2.SpindleCaps.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent[] {
            this.arcScaleSpindleCapComponent2});
            // 
            // arcScaleBackgroundLayerComponent2
            // 
            this.arcScaleBackgroundLayerComponent2.ArcScale = this.arcScaleComponent2;
            this.arcScaleBackgroundLayerComponent2.Name = "bg1";
            this.arcScaleBackgroundLayerComponent2.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5F, 0.815F);
            this.arcScaleBackgroundLayerComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularHalf_Style3;
            this.arcScaleBackgroundLayerComponent2.Size = new System.Drawing.SizeF(244F, 152F);
            this.arcScaleBackgroundLayerComponent2.ZOrder = 1000;
            // 
            // arcScaleComponent2
            // 
            this.arcScaleComponent2.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 12F);
            this.arcScaleComponent2.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#FF8000");
            this.arcScaleComponent2.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 165F);
            this.arcScaleComponent2.EndAngle = 0F;
            this.arcScaleComponent2.MajorTickCount = 7;
            this.arcScaleComponent2.MajorTickmark.FormatString = "{0:F0}";
            this.arcScaleComponent2.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style3_4;
            this.arcScaleComponent2.MajorTickmark.TextOffset = -18F;
            this.arcScaleComponent2.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.arcScaleComponent2.MaxValue = 80F;
            this.arcScaleComponent2.MinorTickCount = 4;
            this.arcScaleComponent2.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style3_3;
            this.arcScaleComponent2.MinValue = 20F;
            this.arcScaleComponent2.Name = "scale1";
            this.arcScaleComponent2.RadiusX = 104F;
            this.arcScaleComponent2.RadiusY = 104F;
            this.arcScaleComponent2.StartAngle = -180F;
            this.arcScaleComponent2.Value = 20F;
            // 
            // arcScaleNeedleComponent2
            // 
            this.arcScaleNeedleComponent2.ArcScale = this.arcScaleComponent2;
            this.arcScaleNeedleComponent2.EndOffset = -8F;
            this.arcScaleNeedleComponent2.Name = "needle1";
            this.arcScaleNeedleComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style3;
            this.arcScaleNeedleComponent2.ZOrder = -50;
            // 
            // arcScaleSpindleCapComponent2
            // 
            this.arcScaleSpindleCapComponent2.ArcScale = this.arcScaleComponent2;
            this.arcScaleSpindleCapComponent2.Name = "cap1";
            this.arcScaleSpindleCapComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.SpindleCapShapeType.CircularFull_Style3;
            this.arcScaleSpindleCapComponent2.ZOrder = -100;
            // 
            // circularGauge3
            // 
            this.circularGauge3.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[] {
            this.arcScaleBackgroundLayerComponent3});
            this.circularGauge3.Bounds = new System.Drawing.Rectangle(6, 6, 501, 138);
            this.circularGauge3.Name = "circularGauge3";
            this.circularGauge3.Needles.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent[] {
            this.arcScaleNeedleComponent3});
            this.circularGauge3.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[] {
            this.arcScaleComponent3});
            this.circularGauge3.SpindleCaps.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent[] {
            this.arcScaleSpindleCapComponent3});
            // 
            // arcScaleBackgroundLayerComponent3
            // 
            this.arcScaleBackgroundLayerComponent3.ArcScale = this.arcScaleComponent3;
            this.arcScaleBackgroundLayerComponent3.Name = "bg1";
            this.arcScaleBackgroundLayerComponent3.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5F, 0.815F);
            this.arcScaleBackgroundLayerComponent3.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularHalf_Style3;
            this.arcScaleBackgroundLayerComponent3.Size = new System.Drawing.SizeF(244F, 152F);
            this.arcScaleBackgroundLayerComponent3.ZOrder = 1000;
            // 
            // arcScaleComponent3
            // 
            this.arcScaleComponent3.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 12F);
            this.arcScaleComponent3.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#FF8000");
            this.arcScaleComponent3.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 165F);
            this.arcScaleComponent3.EndAngle = 0F;
            this.arcScaleComponent3.MajorTickCount = 7;
            this.arcScaleComponent3.MajorTickmark.FormatString = "{0:F0}";
            this.arcScaleComponent3.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style3_4;
            this.arcScaleComponent3.MajorTickmark.TextOffset = -18F;
            this.arcScaleComponent3.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.arcScaleComponent3.MaxValue = 80F;
            this.arcScaleComponent3.MinorTickCount = 4;
            this.arcScaleComponent3.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style3_3;
            this.arcScaleComponent3.MinValue = 20F;
            this.arcScaleComponent3.Name = "scale1";
            this.arcScaleComponent3.RadiusX = 104F;
            this.arcScaleComponent3.RadiusY = 104F;
            this.arcScaleComponent3.StartAngle = -180F;
            this.arcScaleComponent3.Value = 20F;
            // 
            // arcScaleNeedleComponent3
            // 
            this.arcScaleNeedleComponent3.ArcScale = this.arcScaleComponent3;
            this.arcScaleNeedleComponent3.EndOffset = -8F;
            this.arcScaleNeedleComponent3.Name = "needle1";
            this.arcScaleNeedleComponent3.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style3;
            this.arcScaleNeedleComponent3.ZOrder = -50;
            // 
            // arcScaleSpindleCapComponent3
            // 
            this.arcScaleSpindleCapComponent3.ArcScale = this.arcScaleComponent3;
            this.arcScaleSpindleCapComponent3.Name = "cap1";
            this.arcScaleSpindleCapComponent3.ShapeType = DevExpress.XtraGauges.Core.Model.SpindleCapShapeType.CircularFull_Style3;
            this.arcScaleSpindleCapComponent3.ZOrder = -100;
            // 
            // docActivities
            // 
            this.docActivities.Form = this;
            this.docActivities.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpnlWidgets});
            this.docActivities.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dpnlWidgets
            // 
            this.dpnlWidgets.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.dpnlWidgets.Appearance.Options.UseBackColor = true;
            this.dpnlWidgets.Controls.Add(this.controlContainer4);
            this.dpnlWidgets.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpnlWidgets.ID = new System.Guid("4abc808c-967b-4c88-923b-6d72c73805ba");
            this.dpnlWidgets.Location = new System.Drawing.Point(0, 0);
            this.dpnlWidgets.Name = "dpnlWidgets";
            this.dpnlWidgets.Options.FloatOnDblClick = false;
            this.dpnlWidgets.Options.ShowCloseButton = false;
            this.dpnlWidgets.Options.ShowMaximizeButton = false;
            this.dpnlWidgets.OriginalSize = new System.Drawing.Size(126, 200);
            this.dpnlWidgets.Size = new System.Drawing.Size(126, 612);
            this.dpnlWidgets.Text = "Widgets";
            // 
            // controlContainer4
            // 
            this.controlContainer4.Controls.Add(this.clstWidgets);
            this.controlContainer4.Location = new System.Drawing.Point(4, 23);
            this.controlContainer4.Name = "controlContainer4";
            this.controlContainer4.Size = new System.Drawing.Size(118, 585);
            this.controlContainer4.TabIndex = 0;
            // 
            // clstWidgets
            // 
            this.clstWidgets.CheckOnClick = true;
            this.clstWidgets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clstWidgets.Location = new System.Drawing.Point(0, 0);
            this.clstWidgets.Name = "clstWidgets";
            this.clstWidgets.Size = new System.Drawing.Size(118, 585);
            this.clstWidgets.TabIndex = 0;
            this.clstWidgets.Click += new System.EventHandler(this.clstWidgets_Click);
            // 
            // frmActivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 612);
            this.Controls.Add(this.dpnlWidgets);
            this.Name = "frmActivity";
            this.Text = "Bar Activity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmActivity_FormClosing);
            this.Load += new System.EventHandler(this.FrmActivityLoad);
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleSpindleCapComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleSpindleCapComponent3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.docActivities)).EndInit();
            this.dpnlWidgets.ResumeLayout(false);
            this.controlContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clstWidgets)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent arcScaleNeedleComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent arcScaleSpindleCapComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge3;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent3;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent3;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent arcScaleNeedleComponent3;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent arcScaleSpindleCapComponent3;
        private DevExpress.Utils.ToolTipController ttcTooltips;
        private DevExpress.XtraBars.Docking.DockManager docActivities;
        private DevExpress.XtraBars.Docking.DockPanel dpnlWidgets;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer4;
        private DevExpress.XtraEditors.CheckedListBoxControl clstWidgets;




    }
}