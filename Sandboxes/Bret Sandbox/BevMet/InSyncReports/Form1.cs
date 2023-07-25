using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InSyncReports.svcContextService;
using InSyncReports.svcInSyncReports;
using System.Management;
using System.Printing;

namespace InSyncReports
{
    public partial class Form1 : Form
    {

        StringBuilder Builder = new StringBuilder( );

        public Form1( )
        {
            InitializeComponent( );
        }

        private void button1_Click( object sender, EventArgs e )
        {
            try
            {
                var svcContext = new svcContextService.ContextReportServiceWSService();

                var map = new string2stringMapEntry();
                map.key = "site";
                map.value = "Hyatt";
                //var reportData = svcContext.query("bottleinfo", "LXR", new string2stringMapEntry[] { map });
                var reportData = svcContext.query(txtChannelName.Text, "Hyatt", new string2stringMapEntry[] { map });

                //foreach (ResultDataComposer t in reportData)
                //{
                //    foreach (DataComposer t1 in t.dataList)
                //    {
                //        var data = t1.value;
                //    }
                //}
                var table = CreateDataTable(reportData);
                gridControl1.DataSource = table;
                gridView1.PopulateColumns(table);
                //gridControl1.DataSource = Comp.materialTypes;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        DataTable CreateDataTable( ResultDataComposer[] _data )
        {
            DataTable rc = new DataTable();

            foreach( var column in _data[0].dataList )
            {
                rc.Columns.Add(column.name);
            }


            foreach (ResultDataComposer t in _data)
            {
                var row = rc.NewRow();
                foreach (DataComposer t1 in t.dataList)
                {
                    row[t1.name] = t1.value;
                }
                rc.Rows.Add(row);
            }

            return rc;
        }


        private void button2_Click( object sender, EventArgs e )
        {
            Builder.Clear( );

            PrintServer Server = new PrintServer( );
            PrintQueueCollection myPrintQueues = Server.GetPrintQueues( );

            foreach( PrintQueue pq in myPrintQueues )
            {
                Builder.Append( "Queue " + pq.Name + System.Environment.NewLine );
                pq.Refresh( );
                PrintJobInfoCollection jobs = pq.GetPrintJobInfoCollection( );
                foreach( PrintSystemJobInfo job in jobs )
                {
                    Builder.Append( "Job " + job.JobName + System.Environment.NewLine );

                    byte[] buffer = new byte[job.JobStream.Length];
                    job.JobStream.Read( buffer, 0, (int)job.JobStream.Length );

                    ProcessBuffer( buffer );
                }// end for each print job    

            }// end for each print queue
            txtPrintQueues.Text = Builder.ToString( );
        }

        private void ProcessBuffer( byte[] _Data )
        {
            Builder.Append( Convert.ToBase64String( _Data ) );
        }

        private void button3_Click( object sender, EventArgs e )
        {
            Builder.Clear( );

            Builder.Append( "Retrieving printer queue information using WMI" + System.Environment.NewLine );
            Builder.Append( "==================================" + System.Environment.NewLine );
            //Query printer queue
            System.Management.ObjectQuery oq = new System.Management.ObjectQuery( "SELECT * FROM Win32_PrintJob" );
            ManagementObjectSearcher query1 = new ManagementObjectSearcher( oq );
            ManagementObjectCollection queryCollection1 = query1.Get( );
            foreach( ManagementObject mo in queryCollection1 )
            {
                Builder.Append( "Printer Driver : " + mo["DriverName"].ToString( ) + System.Environment.NewLine );
                Builder.Append( "Document Name : " + mo["Document"].ToString( ) + System.Environment.NewLine );
                Builder.Append( "Document Owner : " + mo["Owner"].ToString( ) + System.Environment.NewLine );
                Builder.Append( "==================================" );
            }

            txtPrintQueues.Text = Builder.ToString( );
        }
    }
}
