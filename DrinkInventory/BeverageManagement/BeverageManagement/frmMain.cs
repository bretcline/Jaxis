using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Windows.Forms;
//using BeverageManagement.Forms.Activity;
using BeverageManagement.Forms.Administration;
using BeverageManagement.Forms.Reconcile;
using BeverageManagement.Properties;
using BeverageManagement.Reports;
using BeverageReports;
using DevExpress.XtraEditors;
using System.Threading.Tasks;
using DevExpress.XtraNavBar;
using HostWCFService;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace BeverageManagement
{
    public partial class frmMain : frmScannerCommands
    {
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        //private System.Threading.Thread m_ServiceTrd = null;
        //ServiceHost m_Host;

        Dictionary<Type, Form> m_FormList = new Dictionary<Type, Form>( );
        private Process m_ActivityThread = null;

        private Task m_FormLoader = null;

        public frmMain( )
        {
            InitializeComponent( );
            Text = "Beverage Monitor";
            m_FormLoader = Task.Factory.StartNew( PreloadForms );

            ScannerCommand.Navigation.Add('B', () => this.nbiBranding_LinkClicked(null, null));
            ScannerCommand.Navigation.Add('A', () => this.nbiActivity_LinkClicked(null, null));
            ScannerCommand.Navigation.Add('Z', () => this.nbiAdministration_LinkClicked(null, null));
            ScannerCommand.Navigation.Add('M', () => this.nbiBarManagement_LinkClicked(null, null));
            ScannerCommand.Navigation.Add('I', () => this.nbiInventory_LinkClicked(null, null));
            ScannerCommand.Navigation.Add('P', () => this.nbiReconcile_LinkClicked(null, null));
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }


        //public void StartWCFService( )
        //{
        //    if( m_Host != null )
        //    {
        //        m_Host.Close( );
        //    }
        //    try
        //    {
        //        m_Host = new ServiceHost( typeof( PourEngineService ) );
        //        m_ServiceTrd = new System.Threading.Thread( m_Host.Open );
        //        m_ServiceTrd.Start( );
        //    }
        //    catch( Exception exp )
        //    {
        //        Log.WriteException( "HostSimForm.StartWCFService()", exp );
        //    }
        //}

        private void PreloadForms( )
        {
            Log.Time("PreloadForms", LogType.Debug, true, () =>
            {
                //Form frm = new frmActivity();
                //m_FormList.Add(frm.GetType(), frm);

                Form frm = new frmBranding();
                m_FormList.Add( frm.GetType( ), frm );

                frm = new frmInventory();
                m_FormList.Add( frm.GetType( ), frm );

                frm = new frmManagement();
                m_FormList.Add( frm.GetType( ), frm );

                //frm = new frmUPCManagement();
                //m_FormList.Add( frm.GetType( ), frm );

                frm = new frmAdmin( );
                m_FormList.Add( frm.GetType( ), frm );

                //StartWCFService( );
            });
        }

        private void frmMain_Load( object sender, EventArgs e )
        {
            try
            {
                this.Show();
                frmLogin login = new frmLogin();
                if( DialogResult.OK == login.ShowDialog( ) )
                {
                    while( !m_FormLoader.IsCompleted )
                    {
                        System.Threading.Thread.Sleep( 100 );
                    }
                    Text = String.Format( Resources.MainFormTitle, login.UserName );
                    GetChildWindow<frmInventory>( );

                    LoadReports(BLManagerFactory.Get().UserSession);

                    LoadExternalLinks();
                }
                else 
                {
                    this.Close();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void LoadExternalLinks()
        {
            try
            {
                string links = ConfigurationManager.AppSettings["ExternalLinks"];
                if( !string.IsNullOrWhiteSpace( links ) )
                {
                    NavBarGroup apps = new NavBarGroup( "Applications" );

                    string[] items = links.Split( 'ß' );
                    foreach( string item in items )
                    {
                        string[] values = item.Split( '|' );
                        NavBarItem Item = new NavBarItem( );
                        Item.Caption = values[1];
                        Item.SmallImage = global::BeverageManagement.Properties.Resources.AddCategory_48;
                        Item.Name = "nbi" + values[1];
                        Item.LinkClicked += this.nbiExternalLink_LinkClicked;
                        Item.Tag = values[0];
                        apps.ItemLinks.Add( Item );
                    }
                    apps.Expanded = true;
                    navBar.Groups.Insert( 0, apps );
                }
            }
            catch (Exception err)
            {
                Log.WriteException( "LoadExternalLinks", err );
            }
        }

        private void LoadReports(IBLUserSession userSession)
        {
            try
            {
                var summaryItem = new NavBarItem();
                //summaryItem.Caption = "Summary Report";
                //summaryItem.LargeImage = global::BeverageManagement.Properties.Resources.Branding_48;
                //summaryItem.Name = "nbiSummaryReport";
                //summaryItem.Tag = ReportTypes.SummaryReport;
                //summaryItem.LinkClicked += ShowDateBoundReport;
                //nbgReports.ItemLinks.Add(summaryItem);

                summaryItem = new NavBarItem();
                summaryItem.Caption = "Pours by UPC";
                summaryItem.LargeImage = global::BeverageManagement.Properties.Resources.Branding_48;
                summaryItem.Name = "nbiPourSummaryReport";
                summaryItem.Tag = ReportTypes.PourSummaryByUPC;
                summaryItem.LinkClicked += ShowDateBoundReport;
                nbgReports.ItemLinks.Add(summaryItem);

                summaryItem = new NavBarItem();
                summaryItem.Caption = "Pours by Tag";
                summaryItem.LargeImage = global::BeverageManagement.Properties.Resources.Branding_48;
                summaryItem.Name = "nbiTagSummaryReport";
                summaryItem.Tag = ReportTypes.PourSummaryByTag;
                summaryItem.LinkClicked += ShowDateBoundReport;
                nbgReports.ItemLinks.Add(summaryItem);

                summaryItem = new NavBarItem();
                summaryItem.Caption = "Alerts by Location";
                summaryItem.LargeImage = global::BeverageManagement.Properties.Resources.Branding_48;
                summaryItem.Name = "nbiAlertsByLocation";
                summaryItem.Tag = ReportTypes.AlertSummary;
                summaryItem.LinkClicked += ShowDateBoundReport;
                nbgReports.ItemLinks.Add(summaryItem);

                var reports = BLManagerFactory.Get().ManageReport().GetReportsByUser(userSession.SessionID);

                var dateBound = reports.Where(r => r.DateBound == true).OrderBy(r => r.Name);
                foreach (var blReport in dateBound)
                {
                    NavBarItem Item = new NavBarItem();
                    Item.Caption = blReport.Name;
                    Item.LargeImage = global::BeverageManagement.Properties.Resources.Branding_48;
                    Item.Name = "nbi" + blReport.Name;
                    Item.LinkClicked += ShowDateBoundReport;
                    Item.Tag = blReport;
                    nbgReports.ItemLinks.Add(Item);
                }


                var gridReports = reports.Where(r => r.DateBound == false).OrderBy( r => r.Name );
                foreach (var blReport in gridReports)
                {
                    NavBarItem Item = new NavBarItem( );
                    Item.Caption = blReport.Name;
                    Item.LargeImage = global::BeverageManagement.Properties.Resources.Branding_48;
                    Item.Name = "nbi" + blReport.Name;
                    Item.LinkClicked += this.nbiReport_LinkClicked;
                    Item.Tag = blReport;
                    nbgReports.ItemLinks.Add( Item );
                }
            }
            catch (Exception err)
            {
                Log.WriteException( "LoadReports", err );
            }
        }


        private void ShowDateBoundReport(object _sender, NavBarLinkEventArgs _e)
        {
            Log.MsgWrap(false, () =>
            {
                var item = _sender as NavBarItem;
                if( null != item )
                {
                    ReportTypes reportType = ReportTypes.DateBound;
                    var frm = new frmDateBoundReport();
                    if (item.Tag is ReportTypes)
                    {
                        reportType = (ReportTypes) item.Tag;
                    }
                    else
                    {
                        frm.ReportItem = (IBLReport) item.Tag;
                    }
                    frm.ReportType = reportType;
                    frm.MdiParent = this;
                    frm.Text = item.Caption;
                    frm.Show();
                }
                return true;
            });
        }



        private void nbiActivity_LinkClicked( object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e )
        {
            var processlist = Process.GetProcesses();

            var process = processlist.Where(p => p.ProcessName == "BeverageActivity").FirstOrDefault();

            if (null != process)
            {
                m_ActivityThread = process;
            }

            if (null == m_ActivityThread || m_ActivityThread.HasExited )
            {
                var fullPath = Application.StartupPath;
                var path = Path.GetFullPath(fullPath) + "\\BeverageActivity.exe";
                if( File.Exists( path ))
                    m_ActivityThread = Process.Start(path);
            }
            else
            {
                SetForegroundWindow(m_ActivityThread.MainWindowHandle);
            }
        }

        private void nbiExternalLink_LinkClicked( object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e )
        {
            try
            {
                Process.Start( (string)e.Link.Item.Tag );
            }
            catch (Exception err)
            {
                Log.WriteException( "Launch External App", err );
            }
        }

        private void nbiReport_LinkClicked( object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e )
        {
            ShowReport( e.Link.Item.Tag as IBLReport );
        }

        private void ShowReport(IBLReport blReport)
        {
            Log.MsgWrap(false, () =>
            {
                frmGridReport report = new frmGridReport();
                report.ReportItem = blReport;
                report.MdiParent = this;
                report.Show();

                return true;
            });
        }

        private void nbiBranding_LinkClicked( object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e )
        {
            GetChildWindow<frmBranding>( );
        }

        private void nbiInventory_LinkClicked( object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e )
        {
            GetChildWindow<frmInventory>();
        }

        private void nbiBarManagement_LinkClicked( object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e )
        {
            GetChildWindow<frmManagement>( );
        }

        private void nbiUPCManagement_LinkClicked( object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e )
        {
            GetChildWindow<frmUPCManagement>( );
        }

        private void nbiAdministration_LinkClicked( object sender, NavBarLinkEventArgs e )
        {
            GetChildWindow<frmAdmin>( );
        }

        private Form GetChildWindow<T>( ) where T : Form, new( )
        {
            Form rc = null;
            Type formType = typeof(T);
            Log.Time(string.Format("GetChildWindow - {0}", formType.Name ), LogType.Debug, true, () =>
            {
                if (!m_FormList.ContainsKey(formType))
                {
                    rc = new T {MdiParent = this};
                    m_FormList.Add(formType, rc);
                    rc.Show();
                }
                else
                {
                    rc = m_FormList[formType];
                    rc.MdiParent = this;
                    rc.Show();
                }
                rc.BringToFront();
            });
            return rc;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void xtraTabbedMdiManager1_SelectedPageChanged(object sender, EventArgs e)
        {
            if (xtraTabbedMdiManager1.SelectedPage.MdiChild is frmInventory)
            {
                var page = xtraTabbedMdiManager1.SelectedPage.MdiChild as frmInventory;
                page.ReloadData();
            }
            else if (xtraTabbedMdiManager1.SelectedPage.MdiChild is frmUPCManagement)
            {
                var page = xtraTabbedMdiManager1.SelectedPage.MdiChild as frmUPCManagement;
                page.ReloadData();
            }
        }

        private void nbiReconcile_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            GetChildWindow<frmReconcile>();
        }
    }
}
