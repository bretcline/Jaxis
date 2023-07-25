using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System.ComponentModel.Design;

namespace ReportManager
{
    public partial class EditReport : UserControl
    {
        public EditReport()
        {
            InitializeComponent();
            BarItem item = new XtraReportsDemos.BarLookAndFeelListItem(DevExpress.LookAndFeel.UserLookAndFeel.Default);
            xrDesignBarManager1.Items.Add(item);
            bsiLookAndFeel.AddItem(item);
        }
    }
}
