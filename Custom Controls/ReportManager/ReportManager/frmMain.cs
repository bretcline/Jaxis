using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraPrinting;
using XtraReportsDemos;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;

namespace ReportManager
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
            //This sets up the circle drop-down menu. I've done this using the properties in the designer.
            //this.ribbon.ApplicationButtonDropDownControl = this.applicationMenu1;
        }

        private void btnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            CustomDesignForm designer = new CustomDesignForm();
            designer.Show();
        }

        private void btnEditOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            string Filename = string.Empty;
            if (DialogResult.OK == ofdOpen.ShowDialog())
            {
                Filename = ofdOpen.FileName;
                CustomDesignForm designer = new CustomDesignForm();
                designer.OpenReport(Filename);
                designer.MdiParent = this;
                designer.Show();
            }
        }

        private void btnEditNewInclosed_ItemClick(object sender, ItemClickEventArgs e)
        {
            CustomDesignForm CDF = new CustomDesignForm();
            CDF.MdiParent = this;
            CDF.Show();
        }

        private void btnViewOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewForm View = new ViewForm();
            XtraReport Report = new XtraReport();

            string Filename = string.Empty;
            if (DialogResult.OK == ofdOpen.ShowDialog())
            {
                Filename = ofdOpen.FileName;
                Report.LoadLayout(Filename);
                Report.PrintingSystem = psReportView;
                View.MdiParent = this;
                DevExpress.XtraPrinting.Control.PrintControl ReportView;
                Control[] ViewControls = View.Controls.Find("pcReportView", true);
                ReportView = (DevExpress.XtraPrinting.Control.PrintControl)ViewControls[0];
                ReportView.PrintingSystem = psReportView;
                Report.CreateDocument();
                View.Show();
            }
        }
    }
}