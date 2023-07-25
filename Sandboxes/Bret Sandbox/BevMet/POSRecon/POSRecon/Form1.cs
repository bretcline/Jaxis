using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace POSRecon
{
    public struct checklist
    {
        static checklist( )
        {
            
        }
    }
    public partial class Form1 : Form
    {
        List<PourRecord> m_PourRecords = new List<PourRecord>( );
        List<POSRecord> m_POSRecords = new List<POSRecord>( );
        List<Reconciled> m_Reconciled = new List<Reconciled>( );

        public Form1( )
        {
            InitializeComponent( );

            for( int i = 0; i < 10; ++i )
            {
                m_POSRecords.Add( new POSRecord( string.Format( "POS {0}", i ) ) );
                m_PourRecords.Add( new PourRecord( string.Format( "Pour {0}", i ) ) );
            }

        }

        private void Form1_Load( object sender, EventArgs e )
        {
            grdPOSData.DataSource = m_POSRecords;
            grdPourData.DataSource = m_PourRecords;
        }

        private void btnAccept_Click( object sender, EventArgs e )
        {
            Reconciled Rec = new Reconciled( );

            GridView View = grdPOSData.MainView as GridView;
            Rec.POS = View.GetRow( View.GetSelectedRows( )[0] ) as POSRecord;
            View.DeleteSelectedRows( );

            View = grdPourData.MainView as GridView;
            Rec.Pour = View.GetRow( View.GetSelectedRows( )[0] ) as PourRecord;
            View.DeleteSelectedRows( );

            clbReconciledItems.Items.Add( Rec );//  string.Format( "{0} - {1}", Rec.POS, Rec.Pour ) );


        }

        private void btnRemove_Click( object sender, EventArgs e )
        {
            checklist
            Reconciled Rec = clbReconciledItems.SelectedItem as Reconciled;

            clbReconciledItems.Items.Remove( Rec );

            m_POSRecords.Add( Rec.POS );
            m_PourRecords.Add( Rec.Pour );
        }
    }
}
