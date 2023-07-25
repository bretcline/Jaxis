using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace BeverageManagement.Forms
{
    public partial class frmInventoryMove : DevExpress.XtraEditors.XtraForm
    {
        private BindingList<IBLInventoryItem> m_Inventory = new BindingList<IBLInventoryItem>( );
        private BindingList<IUITag> m_Tags = new BindingList<IUITag>( );
        private frmUPCManagement.NodeInfo m_NodeInfo;
        private StringBuilder m_KeyBuffer = new StringBuilder( );
        private IBLInventoryItem m_Item = null;
        private IUITag m_Tag = null;

        public frmInventoryMove( )
        {
            InitializeComponent( );
            txtTagID.Enabled = false;
            treeLocation.CustomDrawNodeCell += CommonUI.CustomDrawNodeCell;
            treeNewLocation.CustomDrawNodeCell += CommonUI.CustomDrawNodeCell;
        }
        
        private void frmInventoryAdd_Load( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                InventoryUI.LoadInventoryTree(treeNewLocation);
                treeNewLocation.ExpandAll();
                InventoryUI.LoadInventoryTree(treeLocation);
                treeLocation.ExpandAll();

                grdNewItems.DataSource = m_Inventory;
                return true;
            });
        }


        void treeUPC_FocusedNodeChanged( object sender, FocusedNodeChangedEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (null != e.Node.Tag)
                {
                    StringBuilder builder = new StringBuilder();
                    string Filter = InventoryUI.BuildFilter(e.Node, builder);
                    gvInventory.ActiveFilterString = string.Format("{0} {1}", Filter, builder.ToString());
                }
                else
                {
                    m_NodeInfo = null;
                }

                return true;
            });
        }

        protected IUITag ValidateTagID( string _TagID )
        {
            return Log.MsgWrap(false, () =>
            {
                IUITag rc = null;
                rc = TagUI.ValidateTagID(txtTagID.Text, m_Tags);
                if (null != rc)
                {
                    txtDescription.Text = rc.UPC.Name;
                    txtManufacturer.Text = rc.UPC.Manufacturer;
                }
                return rc;
            });
        }

        private void txtTagID_KeyPress( object sender, KeyPressEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (chkKeyboard.Checked)
                {
                    if ((char)Keys.Enter == e.KeyChar || (char)Keys.Tab == e.KeyChar)
                    {
                        m_Tag = ValidateTagID(txtTagID.Text);
                    }
                }

                return true;
            });
        }

        private void frmInventoryMove_KeyPress( object sender, KeyPressEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (!chkKeyboard.Checked)
                {
                    if ((char)Keys.Enter == e.KeyChar || (char)Keys.Tab == e.KeyChar)
                    {
                        txtTagID.Text = m_KeyBuffer.ToString();
                        m_Tag = ValidateTagID(txtTagID.Text);

                        m_KeyBuffer.Clear();
                    }
                    else if (char.IsLetterOrDigit(e.KeyChar))
                    {
                        m_KeyBuffer.Append(e.KeyChar);
                        txtTagID.Select();
                        if (txtTagID.CanFocus)
                        {
                            txtTagID.Select();
                            txtTagID.Focus();
                        }
                        else
                        {
                            grdInventory.Focus();
                        }
                    }
                }

                return true;
            });
        }

        private void chkUPCKeyboard_CheckedChanged( object sender, EventArgs e )
        {
            txtTagID.Enabled = chkKeyboard.Checked;
        }

        private void btnOK_Click( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                var Man = BLManagerFactory.Get().ManageInventoryItems();
                foreach (var inventory in m_Inventory)
                {
                    Man.Save(inventory);
                }

                //Man.CleanupInventory();
                this.Close();

                return true;
            });
        }

        private void btnMove_Click( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                IBLInventoryItem item = null;
                var Loc = treeNewLocation.Selection[0].Tag as IBLLocation;
                if (xtabUPCSelection.SelectedTabPage == tabSelectUPC)
                {
                    var focused = gvInventory.GetFocusedRow();
                    m_Item = focused as IBLInventoryItem;
                    m_Item.Location = Loc;
                    item = m_Item;
                }
                else
                {


                    // MLF not sure what this is doing.. it may need to change for the new inv table

                    m_Tag.CurrentLocation = Loc;
                    item = BLManagerFactory.Get().ManageInventoryItems().Create(m_Tag as IBLTag);
                }
                m_Inventory.Add(item);

                return true;
            });
        }

        //private void treeLocation_CustomDrawNodeCell( object sender, CustomDrawNodeCellEventArgs e )
        //{
        //    // obtaining brushes for cells of focused and unfocused nodes
        //    Brush backBrush, foreBrush;
        //    if( e.Node != ( sender as TreeList ).FocusedNode )
        //    {
        //        backBrush = new LinearGradientBrush( e.Bounds, Color.Empty, Color.Empty,
        //          LinearGradientMode.Horizontal );
        //        foreBrush = new SolidBrush( Color.Black );
        //    }
        //    else
        //    {
        //        backBrush = new SolidBrush( Color.SteelBlue );
        //        foreBrush = new SolidBrush( Color.White );
        //    }
        //    // filling the background
        //    e.Graphics.FillRectangle( backBrush, e.Bounds );
        //    // painting node value
        //    e.Graphics.DrawString( e.CellText, e.Appearance.Font, foreBrush, e.Bounds,
        //      e.Appearance.GetStringFormat( ) );

        //    // prohibiting default painting
        //    e.Handled = true;
        //}

        private void xtabUPCSelection_SelectedPageChanged( object sender, DevExpress.XtraTab.TabPageChangedEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (xtabUPCSelection.SelectedTabPage == tabSelectUPC)
                {
                    InventoryUI.LoadGrid(grdInventory, false);
                }
                return true;
            });
        }

        private void frmInventoryMove_FormClosing(object sender, FormClosingEventArgs e)
        {
            var items = gvInventory.DataSource as BindingList<IUIInventoryItemView>;
            if (null != items)
            {
                items.Clear();
            }
        }
    }
}

