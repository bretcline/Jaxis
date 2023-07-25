using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LFI.RFID.Format;
using LFI.Mobile.Controls.Picklist.Grid;
using LFI.Mobile.Controls.Picklist;
using LFI.Mobile.Controls;
using LFI.Mobile.Controls.Picklist.Standard;

namespace MobileInterrogator
{
    public partial class frmViewTag : Form
    {
        LFI.Mobile.Controls.Picklist.Picklist lstDataRows = null;
        List<TagData> m_Tags = new List<TagData>( 5 );
        TagData m_ActiveItem = null;

        public TagData ActiveItem
        {
            get{ return m_ActiveItem; }
            set
            {
                m_ActiveItem = value;
                RebuildPickList( );
            }
        }

        public frmViewTag( )
        {
            InitializeComponent( );
        }

        private void sbtnViewTag_Click( object sender, EventArgs e )
        {

        }

        private void frmViewTag_Load( object sender, EventArgs e )
        {

        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Rebuilds the pick list.
        /// </summary>
        private void RebuildPickList( )
        {
            if( lstDataRows != null )
            {
                if( pnlPickList.Controls.Contains( lstDataRows ) )
                    pnlPickList.Controls.Remove( lstDataRows );

                lstDataRows.SelectedIndexChanged -= OnPagedGridViewSelectedIndexChanged;
            }

            StandardPicklistProperties properties = new StandardPicklistProperties { AlternateBackgroundGradientDirection = Direction.Horizontal, BackgroundGradientDirection = Direction.Horizontal, AlternateBackgroundGradientEndColor = Color.Wheat, AlternateBackgroundGradientStartColor = Color.FromArgb( 220, 197, 154 ), BackgroundGradientEndColor = Color.White, BackgroundGradientStartColor = Color.FromArgb( 230, 230, 230 ) };

            lstDataRows = new Picklist( properties );

            foreach( DataRow Row in ActiveItem.DataRows )
            {
                string RowData = string.Empty;
                foreach( string Key in Row.Values.Keys )
                {
                    RowData += " " + Row.Values[Key];
                }
                StandardPicklistItem item = PicklistItemFactory.CreateStandardPicklistItem( properties, RowData, RowData );
                lstDataRows.AddItem( item );
            }

            //if( ApplicationSettings.AllowDoubleClick )
            {
                lstDataRows.DoubleClick -= OnTagSelected;
                lstDataRows.DoubleClick += OnTagSelected;
            }

            lstDataRows.Dock = DockStyle.Fill;
            lstDataRows.SelectedIndexChanged -= OnPagedGridViewSelectedIndexChanged;
            lstDataRows.SelectedIndexChanged += OnPagedGridViewSelectedIndexChanged;
            lstDataRows.SelectedIndex = -1;
            pnlPickList.Controls.Add( lstDataRows );
        }


        //----------------------------------------------------------------------
        /// <summary>
        /// Called when [well selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnTagSelected( object sender, EventArgs e )
        {
            if( lstDataRows.SelectedIndex != -1 )
            {
                using( frmViewTag Viewer = new frmViewTag { ActiveItem = m_Tags[lstDataRows.SelectedIndex] } )
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