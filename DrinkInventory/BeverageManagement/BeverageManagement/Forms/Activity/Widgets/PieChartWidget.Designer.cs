namespace BeverageManagement.Forms.Activity.Widgets
{
    partial class PieChartWidget
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel2 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
            this.chartCurrentPours = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.chartCurrentPours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
            this.SuspendLayout();
            // 
            // chartCurrentPours
            // 
            this.chartCurrentPours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartCurrentPours.EmptyChartText.Text = "There is no data to be displayd.";
            this.chartCurrentPours.Location = new System.Drawing.Point(0, 0);
            this.chartCurrentPours.Name = "chartCurrentPours";
            this.chartCurrentPours.PaletteName = "Verve";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            pieSeriesLabel1.LineVisible = true;
            series1.Label = pieSeriesLabel1;
            series1.Name = "Volume";
            pieSeriesView1.RuntimeExploding = false;
            series1.View = pieSeriesView1;
            this.chartCurrentPours.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            pieSeriesLabel2.LineVisible = true;
            this.chartCurrentPours.SeriesTemplate.Label = pieSeriesLabel2;
            pieSeriesView2.RuntimeExploding = false;
            this.chartCurrentPours.SeriesTemplate.View = pieSeriesView2;
            this.chartCurrentPours.Size = new System.Drawing.Size(258, 254);
            this.chartCurrentPours.TabIndex = 0;
            // 
            // PieChartWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartCurrentPours);
            this.Name = "PieChartWidget";
            this.Size = new System.Drawing.Size(258, 254);
            this.Load += new System.EventHandler(this.PieChartWidget_Load);
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCurrentPours)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraCharts.ChartControl chartCurrentPours;




    }
}
