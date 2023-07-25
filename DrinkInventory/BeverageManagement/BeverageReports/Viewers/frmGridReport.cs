using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeverageManagement.Controls;
using DevExpress.Charts.Native;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraPrinting;
using HostWCFService;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;
using Jaxis.Utilities.Database;
using SubSonic.Query;
using ReportParameter = BeverageManagement.Controls.ReportParameter;

namespace BeverageManagement
{
    public partial class frmGridReport : DevExpress.XtraEditors.XtraForm
    {
        public frmGridReport( )
        {
            InitializeComponent( );
        }

        public IBLReport ReportItem { get; set; }

        private void frmGridReport_Load( object sender, EventArgs e )
        {
            if( null != ReportItem )
            {
                this.Text = ReportItem.Name;

                if( 0 < ReportItem.Parameters.Count )
                {
                    layoutParams.SuspendLayout();
                    bool Left = false;

                    var Item = ReportItem.Parameters[0];
                    var Param = new ReportParameter( )
                    {
                        Lable = Item.Name,
                        SQLName = Item.SQLName,
                        Value = Item.DefaultValue,
                        ShowComparitor = false
                    };
                    LayoutControlItem item = lcgReportParams.AddItem( );
                    item.Control = Param;
                    item.TextVisible = false;
                    item.ControlAlignment = ContentAlignment.MiddleCenter;
                    LayoutControlItem Previous = item;
                    for( int i = 1; i < ReportItem.Parameters.Count; ++i )
                    {
                        Item = ReportItem.Parameters[i];
                        Param = new ReportParameter( )
                        {
                            Lable = Item.Name,
                            SQLName = Item.SQLName,
                            Value = Item.DefaultValue,
                        };

                        if( true == Left )
                        {
                            item = lcgReportParams.AddItem( );
                            item.Control = Param;
                            item.TextVisible = false;
                            item.ControlAlignment = ContentAlignment.TopCenter;

                            Previous = item;
                        }
                        else
                        {
                            item = new LayoutControlItem( lcgReportParams );
                            item.Control = Param;
                            item.TextVisible = false;
                            item.ControlAlignment = ContentAlignment.TopCenter;

                            item.Move(Previous, InsertType.Right);
                        }
                        Left = !Left;
                    }

                    //foreach( var Item in ReportItem.Parameters )
                    //{
                    //    ReportParameter Param = new ReportParameter( )
                    //    {
                    //        Lable = Item.Name,
                    //        SQLName = Item.SQLName,
                    //        Value = Item.DefaultValue,
                    //    };

                    //    LayoutControlItem item = lcgReportParams.AddItem();

                    //    item.Control = Param;
                    //    item.TextVisible = false;
                    //    item.ControlAlignment = ContentAlignment.MiddleCenter;

                    //    Show = true;
                    //}
                    //lcgReportParams.AddItem( new EmptySpaceItem( ) );
                    layoutParams.ResumeLayout( );
                }
                else
                {
                    layoutParams.Visible = false;
                    btnQuery_Click(null, null);
                }
            }
        }

        private DataTable ProcessCommand( IBLReport ReportItem )
        {
            DataSet rc = new DataSet();
            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["BeverageMonitor"].ConnectionString;

                using (SqlTool DBConn = new SqlTool(ConnectionString))
                {
                    DBConn.ExecuteQuery(ref rc, "ReportData", BuildCommand(ReportItem.Command));
                }
            }
            catch( Exception err )
            {
                Log.WriteException( "frmGridReport::ProcessCommand", err);
            }
            return rc.Tables["ReportData"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string BuildCommand( string _command )
        {
            StringBuilder Builder = new StringBuilder( _command );

            if( !_command.ToUpper( ).Contains( "WHERE " ) )
            {
                Builder.Append( " WHERE 1=1 " );
            }
            foreach( LayoutControlItem item in lcgReportParams.Items )
            {
                ReportParameter param = item.Control as ReportParameter;
                if( null != param )
                {
                    Builder.Append( param.WhereClause );
                }
            }
            return Builder.ToString( );
        }

        private void btnClose_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        private void btnExport_Click( object sender, EventArgs e )
        {
            //if( DialogResult.OK == sfdExport.ShowDialog( this ) )
            {
                //string FileName = sfdExport.FileName;
                //string Suffix = FileName.Substring(FileName.LastIndexOf(".") + 1);

                GridView View = grdReportData.MainView as GridView;
                if( View != null )
                {
                    grdReportData.ShowPrintPreview();
                }
            }
        }

        private void btnQuery_Click( object sender, EventArgs e )
        {
            string Query = ReportItem.Command;

            DataTable Data = ProcessCommand( ReportItem );

            grdReportData.DataSource = Data;
        }

        private void lcgReportParams_Hidden( object sender, EventArgs e )
        {
            LayoutControlGroup rc = sender as LayoutControlGroup;
            if( null != rc )
            {
                if( rc.Expanded == false )
                {
                    layoutParams.Height = 24;
                }
                else
                {
                    layoutParams.Height = rc.Height;
                }
            }
        }
    }
}