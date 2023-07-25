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
using Jaxis.Inventory.Data.BusinessLogic.BLObjects;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using JaxisExtensions;
using System.Globalization;

namespace BeverageManagement.Forms
{
    public partial class frmInventoryAdd : DevExpress.XtraEditors.XtraForm
    {
        private IBLManagerFactory m_Factory = BLManagerFactory.Get( );
        private BindingList<IBLInventory> m_Inventory = new BindingList<IBLInventory>( );
        private frmUPCManagement.NodeInfo m_NodeInfo;
        private StringBuilder m_KeyBuffer = new StringBuilder( );
        private IBLUPCItem m_Item = null;

        public frmInventoryAdd( )
        {
            InitializeComponent();
            txtUPC.Enabled = false;
            treeUPC.CustomDrawNodeCell += CommonUI.CustomDrawNodeCell;
        }

        void treeUPC_FocusedNodeChanged( object sender, FocusedNodeChangedEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (null != e.Node.Tag)
                {
                    var level = (CategoryLevel)e.Node.Level;

                    m_NodeInfo = UPCUI.FilterUPCGrid(e.Node, level, gvUPCItems);
                }
                else
                {
                    m_NodeInfo = null;
                }

                return true;
            });
        }

        private string BuildFilter(TreeListNode node, StringBuilder builder )
        {
            return Log.MsgWrap(false, () =>
            {
                string rc = string.Empty;
                if (node.Tag is IBLLocation || node.Tag is IBLOrganization)
                {
                    var loc = node.Tag as IBLLocation;
                    if (null != loc)
                    {
                        rc = string.Format(" ( Location = '{0}' ) ", loc.Name);
                    }
                    foreach (TreeListNode child in node.Nodes)
                    {
                        builder.Append(String.Format(" OR ({0}) ", BuildFilter(child, builder)));
                    }
                }
                return rc;
            });
        }

        private void frmInventoryAdd_Load( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                Cursor = Cursors.WaitCursor;

                InventoryUI.LoadInventoryTree(treeLocation);
                treeLocation.ExpandAll();

                UPCUI.LoadUPCTree(treeUPC);
                UPCUI.LoadUPCGrid(gvUPCItems);

                grdNewItems.DataSource = m_Inventory;

                Cursor = Cursors.Default;

                return true;
            });
        }

        private void btnAddItem_Click( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if ("0.00" == txtCost.Text)
                {
                    MessageBox.Show(this, string.Format(global::BeverageManagement.Properties.Resources.NoInventoryCost),
                                    "", MessageBoxButtons.OK);
                }
                else
                {
                    if (xtabUPCSelection.SelectedTabPage == tabSelectUPC)
                    {
                        m_Item = gvUPCItems.GetFocusedRow() as IBLUPCItem;
                    }
                    if (null != m_Item)
                    {
                        var Loc = treeLocation.Selection[0].Tag as IBLLocation;
                        if (null != Loc)
                        {
                            IInventoryBLManager Man = BLManagerFactory.Get().ManageInventory();
                            for (int i = 0; i < Convert.ToInt32(txtQuantity.Text); i++)
                            {
                                var Item = Man.Create(m_Item, Loc, Convert.ToDecimal(txtCost.Text));
                                m_Inventory.Add(Item);
                            }
                            txtQuantity.Text = "1";
                        }
                        else
                        {
                            MessageBox.Show(this, global::BeverageManagement.Properties.Resources.InvalidLocation);
                        }
                    }
                }

                return true;
            });
        }

        private void txtUPC_KeyPress( object sender, KeyPressEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (chkUPCKeyboard.Checked)
                {
                    if ((char) Keys.Enter == e.KeyChar || (char) Keys.Tab == e.KeyChar)
                    {
                        m_Item = ValidateUPC(txtUPC.Text);
                    }
                }
                return true;
            });
        }

        protected IBLUPCItem ValidateUPC( string upcValue )
        {
            return Log.MsgWrap(false, () =>
            {
                var man = BLManagerFactory.Get().ManageUPCs();
                var rc = man.GetAll().Where(U => U.ItemNumber == upcValue).FirstOrDefault() as IBLUPCItem;
                if (null == rc)
                {
                    var item = man.Create();
                    item.ItemNumber = upcValue;
                    using (frmUPC upc = new frmUPC() { Item = item })
                    {
                        if (System.Windows.Forms.DialogResult.OK == upc.ShowDialog())
                        {
                            rc = upc.Item;
                        }
                    }
                }
                if (rc != null)
                {
                    txtManufacturer.Text = rc.Manufacturer;
                    txtDescription.Text = rc.Name;
                    txtCost.Text = rc.UnitPrice.HasValue ? rc.UnitPrice.Value.ToString("F2") : "0.00";
                }
                return rc;
            });
        }


        private void frmInventoryAdd_KeyPress( object sender, KeyPressEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (!chkUPCKeyboard.Checked)
                {
                    if (txtQuantity.IsEditorActive )
                    {
                    }
                    else
                    {
                        if ((char) Keys.Enter == e.KeyChar || (char) Keys.Tab == e.KeyChar)
                        {
                            if ("0.00" == txtCost.Text)
                            {
                                MessageBox.Show(this, string.Format(global::BeverageManagement.Properties.Resources.NoInventoryCost),
                                                "", MessageBoxButtons.OK);
                            }
                            else
                            {
                                txtUPC.Text = m_KeyBuffer.ToString();
                                m_Item = ValidateUPC(txtUPC.Text);

                                m_KeyBuffer.Clear();
                            }
                        }
                        else if (char.IsLetterOrDigit(e.KeyChar))
                        {
                            m_KeyBuffer.Append(e.KeyChar);
                            txtUPC.Select();
                            if (txtUPC.CanFocus)
                            {
                                txtUPC.Select();
                                txtUPC.Focus();
                            }
                            else
                            {
                                grdUPCItems.Focus();
                            }
                        }
                    }
                }
                return true;
            });
        }

        private void chkUPCKeyboard_CheckedChanged( object sender, EventArgs e )
        {
            txtUPC.Enabled = chkUPCKeyboard.Checked;
        }

        private void btnOK_Click( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                IInventoryBLManager Man = BLManagerFactory.Get().ManageInventory();
                foreach (var Inv in m_Inventory)
                {
                    Man.AddToInventory(Inv, 1);
                }
                this.Close();
                return true;
            });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtQuantity.Text = (1 + (Convert.ToInt32(txtQuantity.Text))).ToString();
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(txtQuantity.Text);
            if (0 < value)
            {
                txtQuantity.Text = ((Convert.ToInt32(txtQuantity.Text)) - 1).ToString();
            }
        }

        private void sbSubtractCost_Click(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(txtCost.Text);
            if (0 < value)
            {
                txtCost.Text = ((Convert.ToDouble(txtCost.Text)) - .01).ToString("F2");
            }
        }

        private void sbAddCost_Click(object sender, EventArgs e)
        {
            txtCost.Text = (.01 + (Convert.ToDouble(txtCost.Text))).ToString("F2");
        }

        //private void txtUPC_EditValueChanged(object sender, EventArgs e)
        //{
        //    IInventoryBLManager Man = BLManagerFactory.Get().ManageInventory();
        //    var Item = Man.GetAll().Where(I => I.UPC.UPCID.ToString() == txtUPC.Text);
        //    if (null != Item && 0 != Item.Count() && Item.FirstOrDefault().UPC.UnitPrice.HasValue)
        //    {
        //        txtCost.Text = Item.FirstOrDefault().UPC.UnitPrice.Value.ToString("F2");
        //    }
        //}

        private void gvUPCItems_Click(object sender, EventArgs e)
        {
            // MLF need to make this where you can only select one grid item
        }

        private void grdUPCItems_Click(object sender, EventArgs e)
        {
            int[] rowIndex = gvUPCItems.GetSelectedRows();
            foreach (var i in rowIndex)
            {
                IBLUPCItem row = gvUPCItems.GetRow(i) as IBLUPCItem;
                if (row != null)
                {
                    txtCost.Text = row.UnitPrice.HasValue ? row.UnitPrice.Value.ToString("F2") : "0.00";
                }
            }
        }

        private void frmInventoryAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            var items = gvUPCItems.DataSource as BindingList<IUIUPCItem>;
            if (null != items)
            {
                items.Clear();
            }
        }

    }
}