using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LFI.RFID.Format;
using LFI.Mobile.Controls.Picklist.Standard;
using LFI.Mobile.Controls;
using LFI.Mobile.Controls.Picklist;
using Jaxis.RFID.Readers;

namespace MobileInterrogator
{
    public partial class frmMain : Form
    {
        private IRFIDReader Reader = null;

        LFI.Mobile.Controls.Picklist.Picklist lstTags = null;
        List<TagData> m_Tags = new List<TagData>( 5 );

        public frmMain( )
        {
            InitializeComponent( );
        }

        private void sbtnScan_Click( object sender, EventArgs e )
        {
            try
            {
                m_Tags = Reader.ReadTags( );
                RebuildPickList( );
            }
            catch( Exception err )
            {
                MessageBox.Show( err.Message );
            }
        }

        private void sbtnViewTag_Click( object sender, EventArgs e )
        {
            try
            {
                using( frmViewTag Viewer = new frmViewTag { ActiveItem = m_Tags[lstTags.SelectedIndex] } )
                {
                    Viewer.ShowDialog( );
                }
            }
            catch( Exception err )
            {
                MessageBox.Show( err.Message );
            }
        }

        private void frmMain_Load( object sender, EventArgs e )
        {
            Reader = RFIDReaderManager.GetReader( RFIDReaderTypes.MockReader );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Rebuilds the pick list.
        /// </summary>
        private void RebuildPickList( )
        {
            if( lstTags != null )
            {
                if( pnlPickList.Controls.Contains( lstTags ) )
                    pnlPickList.Controls.Remove( lstTags );

                lstTags.SelectedIndexChanged -= OnPagedGridViewSelectedIndexChanged;
            }

            StandardPicklistProperties properties = new StandardPicklistProperties { AlternateBackgroundGradientDirection = Direction.Horizontal, BackgroundGradientDirection = Direction.Horizontal, AlternateBackgroundGradientEndColor = Color.Wheat, AlternateBackgroundGradientStartColor = Color.FromArgb( 220, 197, 154 ), BackgroundGradientEndColor = Color.White, BackgroundGradientStartColor = Color.FromArgb( 230, 230, 230 ) };

            lstTags = new Picklist( properties );

            foreach( TagData Tag in m_Tags )
            {
                StandardPicklistItem item = PicklistItemFactory.CreateStandardPicklistItem( properties, Tag.HeaderRow.Values["TagID"], Tag.HeaderRow.Values["TagID"] );
                lstTags.AddItem( item );
            }

            //if( ApplicationSettings.AllowDoubleClick )
            {
                lstTags.DoubleClick -= OnTagSelected;
                lstTags.DoubleClick += OnTagSelected;
            }

            lstTags.Dock = DockStyle.Fill;
            lstTags.SelectedIndexChanged -= OnPagedGridViewSelectedIndexChanged;
            lstTags.SelectedIndexChanged += OnPagedGridViewSelectedIndexChanged;
            lstTags.SelectedIndex = -1;
            pnlPickList.Controls.Add( lstTags );
        }


        //----------------------------------------------------------------------
        /// <summary>
        /// Called when [well selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnTagSelected( object sender, EventArgs e )
        {
            if( lstTags.SelectedIndex != -1 )
            {
                using( frmViewTag Viewer = new frmViewTag { ActiveItem = m_Tags[lstTags.SelectedIndex] } )
                {
                    Viewer.ShowDialog( );
                }
            }
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Called when [paged grid view selected index changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnPagedGridViewSelectedIndexChanged( object sender, EventArgs e )
        {
            //if( lstTags.SelectedIndex != -1 )
            //{
            //    okMenuButton.Text = Resources.Button_OK;
            //    okMenuButton.Enabled = true;
            //    currentIndex = picklist.SelectedIndex;
            //    Invalidate( );
            //}

            //else
            //    okMenuButton.Enabled = false;
        }


    }
}