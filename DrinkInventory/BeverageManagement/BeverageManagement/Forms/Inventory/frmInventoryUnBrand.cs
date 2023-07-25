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
    public partial class frmInventoryUnBrand : frmScannerCommands
    {
        public class UnbrandedItem
        {
            public UnbrandedItem( IBLTag _tag )
            {
                TagItem = _tag;
            }

            public IBLTag TagItem { get; set; }
            public string UPC { get { return (null != TagItem ? TagItem.UPC.Name : null); } }
            public string Location { get { return (null != TagItem ? TagItem.CurrentInventory.Location.Name : null); } }
            public ExitReasons Reason { get; set; }
            public string Memo { get; set; }
        }



        private StringBuilder m_KeyBuffer = new StringBuilder( );
        private BindingList<UnbrandedItem> m_UnbrandedItems = new BindingList<UnbrandedItem>();
        
        public frmInventoryUnBrand( )
        {
            InitializeComponent( );
        }


        private void frmInventoryUnBrand_Load(object sender, EventArgs e)
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


                var editor = new RepositoryItemLookUpEdit
                {
                    ValueMember = "EnumValue",
                    DisplayMember = "EnumName",
                    NullText = string.Empty,
                    DataSource = CommonUI.GetEnumItems<ExitReasons>()
                };
                editor.Columns.Add(new LookUpColumnInfo("EnumName", "Reason") { SortOrder = ColumnSortOrder.Ascending }); 
                
                editor.NullText = "";
                var column = gvNewItems.Columns["Reason"];
                column.ColumnEdit = editor;
                editor.Name = "Reason";
                grdNewItems.RepositoryItems.Add(editor);

                return true;
            });
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
                        var item = view.GetFocusedRow() as UnbrandedItem;
                        if (null != item)
                        {
                            if (view.FocusedColumn == view.Columns["Memo"] ||
                                view.FocusedColumn == view.Columns["Reason"])
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
                        ValidateTagID(txtTagID.Text);
                    }
                }

                return true;
            });
        }

        protected UnbrandedItem ValidateTagID( string _tagNumber )
        {
            return Log.MsgWrap(false, () =>
            {
                var rc = ValidateTagID(txtTagID.Text, m_UnbrandedItems);
                if (null != rc)
                {
                    UpdateUIElements(rc);
                }
                return rc;
            });
        }

        private void UpdateUIElements(UnbrandedItem _tag)
        {
            Log.MsgWrap(false, () =>
            {
                txtTagID.Text = _tag.TagItem.TagNumber;
                txtDescription.Text = _tag.UPC;
                return true;
            } );
        }


        private void frmInventoryUnBrand_KeyPress( object sender, KeyPressEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (!AllowKeyboardEntry())
                {
                    if ((char)Keys.Enter == e.KeyChar || (char)Keys.Tab == e.KeyChar)
                    {
                        txtTagID.Text = m_KeyBuffer.ToString();
                        ValidateTagID(txtTagID.Text);

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
                var item = gvNewItems.GetFocusedRow() as UnbrandedItem;
                if (null != item)
                {
                    DoubleScanRemove(item.TagItem.TagNumber, m_UnbrandedItems);
                }

                return true;
            });
        }
        private void btnOK_Click( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if(gvNewItems.PostEditor() && gvNewItems.UpdateCurrentRow())
                {
                    gvNewItems.CloseEditor();
                }

                var Man = BLManagerFactory.Get().ManageTags();

                foreach (var item in m_UnbrandedItems)
                {
                    item.TagItem.Memo = item.Memo;
                    Man.UnBrand(item.TagItem, item.Reason );
                }

                return true;
            });
        }

        private void chkUPCKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            txtTagID.Enabled = chkAllowKeyboardEntry.Checked;
        }

        private void gvNewItems_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                var item = gvNewItems.GetFocusedRow() as UnbrandedItem;
                if (null != item)
                {
                    UpdateUIElements( item );
                }

                return true;
            });
        }
        
        public static UnbrandedItem ValidateTagID(string _tagId, BindingList<UnbrandedItem> _tags)
        {
            UnbrandedItem rc = Log.MsgWrap(false, () =>
            {
                UnbrandedItem item = null;
                if (!DoubleScanRemove(_tagId, _tags))
                {
                    var man = BLManagerFactory.Get().ManageTags();
                    var tag = man.GetTagWithInventory(_tagId).FirstOrDefault();
                    if (null == tag)
                    {
                        MessageBox.Show(string.Format("Tag {0} is not currently associated with a bottle.", _tagId));
                    }
                    else
                    {
                        item = new UnbrandedItem(tag);
                        _tags.Add(item);
                    }
                }
                return item;
            });
            return rc;
        }

        public static bool DoubleScanRemove(string _tagId, BindingList<UnbrandedItem> _tags)
        {
            return Log.MsgWrap(false, () =>
            {
                var rc = false;
                var bottle = _tags.Where(B => B.TagItem.TagNumber == _tagId).FirstOrDefault();
                if (null != bottle)
                {
                    rc = _tags.Remove(bottle);
                }

                return rc;
            });
        }

        private void gvNewItems_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view != null)
                {
                    var item = view.GetFocusedRow() as UnbrandedItem;
                    if (view.FocusedColumn != view.Columns["Memo"] &&
                        view.FocusedColumn != view.Columns["Reason"])
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception err)
            {
                Log.WriteException("gvBottles_ShowingEditor", err);
                throw;
            }
        }
    }
}