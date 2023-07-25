using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Properties;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using JaxisExtensions;

namespace BeverageManagement.Forms
{
    public partial class frmAdjustStockInventory : frmScannerCommands
    {
        public class UnbrandedInventory
        {
            protected IBLLocation m_Location = null;
            public UnbrandedInventory(IBLUPCItem _item, IBLLocation _location )
            {
                UPCItem = _item;
                m_Location = _location;
            }
            public UnbrandedInventory(IBLTag _item, IBLLocation _location)
            {
                TagItem = _item;
                m_Location = _location;
            }

            public IBLTag TagItem { get; set; }
            public IBLUPCItem UPCItem { get; set; }
            public IBLLocation Location { get { return m_Location; } }
            public int Count { get; set; }
            public ExitReasons Reason { get; set; }
            public string Memo { get; set; }
        }

        private ExitReasons m_LastExitReason = ExitReasons.Dispensed;

        private BindingList<UnbrandedInventory> m_UnbrandedItems = new BindingList<UnbrandedInventory>();
        private StringBuilder m_KeyBuffer = new StringBuilder();

        public frmAdjustStockInventory()
        {
            InitializeComponent( );
        }


        private void frmAdjustStockInventory_Load(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                grdNewItems.DataSource = m_UnbrandedItems;
                gvNewItems.CustomizeView( false );

                if (null != gvNewItems.Columns.ColumnByFieldName("Memo"))
                {
                    gvNewItems.Columns["Memo"].Summary.Add(SummaryItemType.Count);
                    gvNewItems.GroupSummary.Add(SummaryItemType.Count, "Memo");
                }

                if (null != gvNewItems.Columns.ColumnByFieldName("Count"))
                {
                    gvNewItems.Columns["Count"].Summary.Add(SummaryItemType.Count);
                    gvNewItems.GroupSummary.Add(SummaryItemType.Sum, "Count");
                }

                var editor = new RepositoryItemLookUpEdit
                {
                    ValueMember = "EnumValue",
                    DisplayMember = "EnumName",
                    NullText = string.Empty,
                    DataSource = CommonUI.GetEnumItems<ExitReasons>()
                };
                editor.Columns.Add(new LookUpColumnInfo("EnumName", "Reason") { SortOrder = ColumnSortOrder.Ascending });
                editor.EditValueChanged += new EventHandler(editor_EditValueChanged);
                
                editor.NullText = "";
                var column = gvNewItems.Columns["Reason"];
                column.ColumnEdit = editor;
                editor.Name = "Reason";
                grdNewItems.RepositoryItems.Add(editor);

                var locations = BLManagerFactory.Get().ManageLocations().GetStorageLocations().OrderBy(n => n.Name).ToList();
                lueLocation.Properties.DataSource = locations;
                lueLocation.Properties.DisplayMember = "Name";
                lueLocation.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name"));

                return true;
            });
        }

        void editor_EditValueChanged(object sender, EventArgs e)
        {
            var editor = sender as LookUpEdit;
            if (null != editor)
            {
                m_LastExitReason = (ExitReasons)editor.EditValue;
            }
        }



        private bool AllowKeyboardEntry()
        {
            try
            {
                bool rc = chkAllowKeyboardEntry.Checked;
                if (false == rc)
                {
                    var view = gvNewItems as GridView;
                    if (view != null)
                    {
                        var item = view.GetFocusedRow() as UnbrandedInventory;
                        if (null != item)
                        {
                            if (view.FocusedColumn == view.Columns["Memo"] ||
                                view.FocusedColumn == view.Columns["Reason"] ||
                                view.FocusedColumn == view.Columns["Count"])
                            {
                                rc = true;
                            }
                        }
                    }
                }
                return rc;
            }
            catch (Exception err)
            {
                Log.WriteException("gvBottles_ShowingEditor", err);
                throw;
            }
        }

        private void txtTagID_KeyPress( object sender, KeyPressEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (AllowKeyboardEntry())
                {
                    if ((char)Keys.Enter == e.KeyChar || (char)Keys.Tab == e.KeyChar)
                    {
                        ValidateUPC(txtUPCNumber.Text);
                        grdNewItems.RefreshDataSource();
                    }
                }

                return true;
            });
        }

        protected UnbrandedInventory ValidateUPC(string _itemNumber)
        {
            return Log.MsgWrap(false, () =>
            {
                var rc = ValidateUPC(_itemNumber, m_UnbrandedItems);
                if (null != rc)
                {
                    UpdateUIElements(rc);
                }
                return rc;
            });
        }

        public UnbrandedInventory ValidateUPC(string _itemId, BindingList<UnbrandedInventory> _items)
        {
            UnbrandedInventory rc = Log.MsgWrap(false, () =>
            {
                UnbrandedInventory item = null;
                var existing = _items.Where(u => u.UPCItem.ItemNumber == _itemId).FirstOrDefault();
                if (null != existing)
                {
                    existing.Count++;
                }
                else
                {
                    if (_itemId.ToString().StartsWith("045") && 10 == _itemId.Length)
                    {
                        var man = BLManagerFactory.Get().ManageTags();
                        var tag = man.GetTagByTagNumber(_itemId);
                        if (null == tag || null == tag.CurrentInventory )
                        {
                            MessageBox.Show(string.Format("{0} is not a associated with a bottle.", _itemId));
                        }
                        else
                        {
                            var location = tag.CurrentInventory.Location ?? tag.CurrentLocation ?? lueLocation.EditValue as IBLLocation;

                            item = new UnbrandedInventory(tag, location) { Count = 1, Reason = m_LastExitReason };
                            item.UPCItem = tag.UPC;
                            _items.Add(item);
                        }
                    }
                    else
                    {
                        if (null == lueLocation.EditValue)
                        {
                            MessageBox.Show("Please select a location.");
                        }
                        else
                        {
                            var man = BLManagerFactory.Get().ManageUPCs();
                            var upc = man.GetUPCByUPCNumber(_itemId);
                            if (null == upc)
                            {
                                MessageBox.Show(string.Format("{0} is not a valid UPC.", _itemId));
                            }
                            else
                            {
                                var location = lueLocation.EditValue as IBLLocation;
                                //if( null != location )
                                //{
                                //    var count = BLManagerFactory.Get().ManageInventory().GetInventoryCount(upc.UPCID, location.LocationID);
                                //}

                                item = new UnbrandedInventory(upc, location)
                                           {Count = 1, Reason = m_LastExitReason};
                                _items.Add(item);
                            }
                        }
                    }
                }
                return item;
            });
            return rc;
        }

        private void UpdateUIElements(UnbrandedInventory _tag)
        {
            Log.MsgWrap(false, () =>
            {
                if (null == _tag.UPCItem)
                {
                    txtUPCNumber.Text = _tag.TagItem.UPC.ItemNumber;
                    txtDescription.Text = _tag.TagItem.UPC.Name;
                }
                else
                {
                    txtUPCNumber.Text = _tag.UPCItem.ItemNumber;
                    txtDescription.Text = _tag.UPCItem.Name;
                }
                return true;
            } );
        }


        private void frmAdjustStockInventory_KeyPress( object sender, KeyPressEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (!AllowKeyboardEntry())
                {
                    if ((char)Keys.Enter == e.KeyChar || (char)Keys.Tab == e.KeyChar)
                    {
                        txtUPCNumber.Text = m_KeyBuffer.ToString();
                        ValidateUPC(txtUPCNumber.Text);

                        m_KeyBuffer.Clear();
                    }
                    else if (char.IsLetterOrDigit(e.KeyChar))
                    {
                        m_KeyBuffer.Append(e.KeyChar);
                        txtUPCNumber.Select();
                        if (txtUPCNumber.CanFocus)
                        {
                            txtUPCNumber.Select();
                            txtUPCNumber.Focus();
                        }
                        else
                        {
                            grdNewItems.Focus();
                        }
                    }
                }

                return true;
            });
        }

        private void btnRemove_Click( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                var item = gvNewItems.GetFocusedRow() as UnbrandedInventory;
                if (null != item)
                {
                    m_UnbrandedItems.Remove(item);
                }

                return true;
            });
        }

        private void btnOK_Click( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                Log.Debug(string.Format("AdjustStockInventory - OK - {0} Items", m_UnbrandedItems.Count));

                if(gvNewItems.PostEditor() && gvNewItems.UpdateCurrentRow())
                {
                    gvNewItems.CloseEditor();
                }

                var tagMan = BLManagerFactory.Get().ManageTags();
                var taggedItems = m_UnbrandedItems.Where(i => i.TagItem != null);

                foreach (var item in taggedItems)
                {
                    for (int i = 0; i < item.Count; ++i)
                    {
                        item.TagItem.Memo = item.Memo;
                        tagMan.UnBrand(item.TagItem, item.Reason);
                    }
                }

                var untaggedItems = m_UnbrandedItems.Where(i => i.TagItem == null);
                var Man = BLManagerFactory.Get().ManageInventory();

                foreach (var item in untaggedItems)
                {
                    Man.RemoveUnTaggedItemsFromInventory(item.UPCItem.UPCID, item.Location.LocationID, item.Count, item.Reason, item.Memo);

                    //for (int i = 0; i < item.Count; ++i )
                    //{
                    //    Man.RemoveUnTaggedFromInventory(item.UPCItem.UPCID, item.Location.LocationID, item.Reason, item.Memo );
                    //}
                }

                m_UnbrandedItems.Clear();

                Log.Debug(string.Format("AdjustStockInventory - OK - Complete"));

                return true;
            });
        }

        private void chkUPCKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            txtUPCNumber.Enabled = chkAllowKeyboardEntry.Checked;
        }

        private void gvNewItems_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                var item = gvNewItems.GetFocusedRow() as UnbrandedInventory;
                if (null != item)
                {
                    UpdateUIElements( item );
                }

                return true;
            });
        }


        private void gvNewItems_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view != null)
                {
                    var item = view.GetFocusedRow() as UnbrandedInventory;
                    if (view.FocusedColumn != view.Columns["Memo"] &&
                        view.FocusedColumn != view.Columns["Reason"] &&
                        view.FocusedColumn != view.Columns["Count"])
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception err)
            {
                Log.WriteException("gvNewItems_ShowingEditor", err);
                throw;
            }
        }

        private void frmAdjustStockInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                if (0 < m_UnbrandedItems.Count)
                {
                    DialogResult result = MessageBox.Show(this, global::BeverageManagement.Properties.Resources.SaveUnbrandedItems, global::BeverageManagement.Properties.Resources.Save, MessageBoxButtons.YesNoCancel);
                    if (DialogResult.Yes == result)
                    {
                        btnOK_Click(sender, null);
                    }
                    else if (DialogResult.Cancel == result)
                    {
                        e.Cancel = true;
                    }
                }
                return true;
            });
        }

        private void lueLocation_EditValueChanged(object sender, EventArgs e)
        {
            if (txtUPCNumber.CanFocus)
            {
                txtUPCNumber.Select();
                txtUPCNumber.Focus();
            }
            else
            {
                grdNewItems.Focus();
            }
        }
    }
}