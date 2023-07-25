using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Forms;
//using BeverageManagement.Forms.Activity.Widgets;
using BeverageManagement.Forms.UPC;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using HostWCFService;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.DataAccess;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;
using JaxisExtensions;
using System.Collections;

namespace BeverageManagement
{
    public partial class frmBranding : frmScannerCommands //, IActivityControl
    {
        private IBLTag m_Tag = null;
        private bool m_KeyEntryHandled = false;
        private StringBuilder m_KeyBuffer = new StringBuilder();
        public object ControlTag { get; set; }

        private IBLStandardNozzle m_DefaultNozzle = null;
        private IBLLocation m_EmptyLocation = null;
        BindingList<IBLBrandedBottle> m_Bottles = new BindingList<IBLBrandedBottle>( );
        private IBLStandardNozzle m_StandardNozzle;

        public frmBranding( )
        {
            InitializeComponent( );

            //chkOnAttach.CheckedChanged += new EventHandler( chkOnAttach_CheckedChanged );

            m_ScannerCommands.Commands.Add('R', () => this.bbtnRemove_ItemClick(null, null));
            m_ScannerCommands.Commands.Add('S', () => this.bbtnSave_ItemClick(null, null));
            m_ScannerCommands.Commands.Add('C', () => this.bbtnClose_ItemClick(null, null));

            //MessageType = new List<Type> {typeof (DataTagActivity)};
        }

        private IBLUPCItem m_UpcItem;
        public IBLUPCItem UpcItem
        {
            get { return m_UpcItem; } 
            set
            {
                m_UpcItem = value;
                if( null != m_UpcItem )
                {
                    btlFillLevel.SetValues(((IUPCItem)m_UpcItem ).Nozzle ?? m_StandardNozzle);
                    btnEditUPC.Text = m_UpcItem.Name;
                    txtUPC.Text = m_UpcItem.ItemNumber;
                }
            }
        }


        public IBLLocation SelectedToLocation { get; set; }
        public IBLLocation SelectedFromLocation { get; set; }
        public IBLUPCItem SelectedUPCItem { get; set; }

        //void chkOnAttach_CheckedChanged( object sender, EventArgs e )
        //{
        //    if( chkOnAttach.Checked )
        //    {
        //        LiveDataStore.Get( ).AddData += AddActivityItem;
        //    }
        //    else
        //    {
        //        LiveDataStore.Get( ).AddData -= AddActivityItem;
        //    }
        //}

        private void frmBranding_Load( object sender, EventArgs e )
        {
            if( !DesignMode )
            {
                Log.MsgWrap(false, () =>
                {
                    m_DefaultNozzle = BLManagerFactory.Get().ManageStandardNozzles().Create();
                    m_DefaultNozzle.SetValues(BLManagerFactory.Get().GetDefaultNozzle());
                    btlFillLevel.SetValues(m_DefaultNozzle);

                    txtUPC.Enabled = txtTagID.Enabled = chkAllowKeyboardEntry.Checked = false;
                    grdBottles.DataSource = m_Bottles;
                    gvBottles.CustomizeView(false);

                    if (null != gvBottles.Columns.ColumnByFieldName("Price"))
                    {
                        gvBottles.Columns["Price"].Summary.Add(SummaryItemType.Count);
                        gvBottles.GroupSummary.Add(SummaryItemType.Count, "Price");
                    }


                    btlFillLevel.OnFillLevelChanged = SetBottleLevel;
                    btlFillLevel.OnNozzleDiameterChanged = SetNozzleDiameter;

//                    var locations = BLManagerFactory.Get().ManageLocations().GetAll().ToList();
                    var locations = BLManagerFactory.Get().ManageLocations().GetStorageLocations().OrderBy(n => n.Name).ToList();
                    lueToLocation.Properties.DataSource = locations;
                    lueToLocation.Properties.DisplayMember = "Name";
                    lueToLocation.Properties.ValueMember = "LocationID";
                    lueToLocation.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name"));


                    //                    var locations = BLManagerFactory.Get().ManageLocations().GetAll().ToList();
                    locations = BLManagerFactory.Get().ManageLocations().GetStorageLocations().OrderBy(n => n.Name).ToList();
                    lueFromLocation.Properties.DataSource = locations;
                    lueFromLocation.Properties.DisplayMember = "Name";
                    lueFromLocation.Properties.ValueMember = "LocationID";
                    lueFromLocation.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name"));

                    if (null != SelectedUPCItem)
                    {
                        UpcItem = SelectedUPCItem;
                    }
                    if (null != SelectedToLocation)
                    {
                        lueToLocation.EditValue = SelectedToLocation.ObjectID;
                    }
                    if (null != SelectedFromLocation)
                    {
                        lueFromLocation.EditValue = SelectedFromLocation.ObjectID;
                    }
                    m_EmptyLocation = BLManagerFactory.Get().ManageLocations().Get(Guid.Empty);

                    if (txtUPC.CanFocus)
                    {
                        txtUPC.Select();
                        txtUPC.Focus();
                    }
                    else
                    {
                        grdBottles.Focus();
                    }

                    if (null != this.MdiParent)
                    {
                        bbtnClose.Visibility = BarItemVisibility.Never;
                    }
                    
                    return true;
                });
            }
        }

        private bool AllowKeyboardEntry()
        {
            try
            {
                bool rc = chkAllowKeyboardEntry.Checked;
                if( false == rc )
                {
                    var view = gvBottles as GridView;
                    if (view != null)
                    {
                        var item = view.GetFocusedRow() as IBLBrandedBottle;
                        if( null != item )
                        {
                            var price = view.Columns["Price"];
                            if (view.FocusedColumn == price && item.NewInventory == true)
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



        private void SetNozzleDiameter()
        {
            Log.MsgWrap(false, () =>
            {
                int[] rowIndex = gvBottles.GetSelectedRows( );

                foreach( var i in rowIndex )
                {
                    BLBrandedBottle row = gvBottles.GetRow( i ) as BLBrandedBottle;
                    if (row != null)
                    {
                        row.Nozzle.SetValues( btlFillLevel.BottleNozzle );
                    }
                }
                gvBottles.RefreshData( );
                return true;
            });
        }

        protected void ValidateUPC( string _upcValue )
        {
            Log.MsgWrap(false, () =>
            {
                var Man = BLManagerFactory.Get( ).ManageUPCs();
                UpcItem = Man.GetUPCByUPCNumber(_upcValue );
                if( null == UpcItem )
                {
                    Int64 value;
                    if( Int64.TryParse(_upcValue, out value) )
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            var upcLookup = new OnlineUPCLookup();
                            IBLUPCItem item = upcLookup.GetOnlineUPCData(_upcValue);
                            ModifyUPC(item);
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
                return true;
            });
        }


        private void ModifyUPC(IBLUPCItem _item )
        {
            Log.MsgWrap(false, () =>
            {
                if (null == _item)
                {
                    _item = BLManagerFactory.Get().ManageUPCs().Create();
                }
                using (var upc = new frmUPC() { Item = _item })
                {
                    if (System.Windows.Forms.DialogResult.OK == upc.ShowDialog())
                    {
                        UpcItem = upc.Item;
                    }
                }
                return true;
            }); 
        }

        protected void ValidateTagID( string _tagIdValue )
        {
            Log.Time("ValidateTagID", LogType.Debug, false, () =>
            {
                if (lueToLocation.Text == string.Empty && lueFromLocation.Text == string.Empty)
                {
                    MessageBox.Show("Please select a TO and FROM location.");
                }
                else if (lueToLocation.Text == string.Empty)
                {
                    MessageBox.Show("Please select a TO location.");
                }
                else if (lueFromLocation.Text == string.Empty)
                {
                    MessageBox.Show("Please select a FROM location.");
                }
                else
                {
                    var Man = BLManagerFactory.Get().ManageTags();
                    m_Tag = Man.GetTagByTagNumber( _tagIdValue );
                    if (false == DoubleScanRemove(_tagIdValue))
                    {
                        if (null == m_Tag)
                        {
                            Int64 value;
                            if (Int64.TryParse(_tagIdValue, out value))
                            {
                                IBLTag tag = Man.Create();
                                tag.TagNumber = _tagIdValue;
                                tag.CurrentLocation = m_EmptyLocation;
                                m_Tag = tag;
                            }
                        }

                        var inventory = BLManagerFactory.Get().ManageInventory().GetInventoryByTag(m_Tag.TagID);
                        if (null != inventory)
                        {
                            if (DialogResult.Yes ==
                                MessageBox.Show(
                                    string.Format(global::BeverageManagement.Properties.Resources.RebrandTag,
                                                  m_Tag.UPC.Name, m_Tag.TagNumber), "", MessageBoxButtons.YesNo))
                            {
                                Man.UnBrand(m_Tag, ExitReasons.Rebrand );
                                AddBrandedItem();
                            }
                        }
                        else
                        {
                            AddBrandedItem();
                        }
                    }
                }
                return true;
            });
        }

        private void AddBrandedItem()
        {
            Log.Time( "AddBrandedItem", LogType.Debug, false, () =>
            {
                #region if you use keyboard to cut and paste the Validate doesn't always get called
                if (null == m_Tag)
                {
                    ValidateTagID(txtTagID.Text);
                }
                if (null == UpcItem)
                {
                    ValidateUPC(txtUPC.Text);
                }
                #endregion

                if ( null != m_Tag && null != UpcItem )
                {
                    if( null == rdoInventoryFrom.EditValue )
                    {
                        var fromLocation = lueFromLocation.GetSelectedDataRow() as IBLLocation;
                        var count = BLManagerFactory.Get().ManageInventory().CheckAvailableInventory(UpcItem.UPCID,
                                                                                         fromLocation.LocationID);
                        if (0 < count)
                        {
                            if (System.Windows.Forms.DialogResult.Yes ==
                                MessageBox.Show(this, global::BeverageManagement.Properties.Resources.BrandFrom,
                                                string.Empty, MessageBoxButtons.YesNo))
                            {
                                rdoInventoryFrom.EditValue = 1; // Existing
                                rdoInventoryFrom.SelectedIndex = 1;
                            }
                            else
                            {
                                rdoInventoryFrom.EditValue = 0; // New
                                rdoInventoryFrom.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            rdoInventoryFrom.EditValue = 0; // New
                            rdoInventoryFrom.SelectedIndex = 0;
                        }
                    }
                    { 
                        BrandBottle();
                    }
                }

                return true;
            });
        }

        private void BrandBottle()
        {
            Log.Time("BrandBottle", LogType.Debug, false, () =>
            {
                try
                {
                    bool hasBottle = m_Bottles.Any(B => B.Tag.TagID == m_Tag.TagID && B.UPC.UPCID == UpcItem.UPCID);
                    if (!hasBottle)
                    {
                        double qty = (btlFillLevel.FillLevel / 100.0f) * UpcItem.Size;
                        var bottle = BLManagerFactory.Get().ManageBrandedBottles().Create();
                        //m_Tag.UPC = m_Item;
                        Log.Time("setup bottle", LogType.Debug, true, () =>
                                                                          {
                                                                              bottle.FromLocation =
                                                                                  lueFromLocation.GetSelectedDataRow()
                                                                                  as IBLLocation;
                                                                              bottle.ToLocation =
                                                                                  lueToLocation.GetSelectedDataRow() as
                                                                                  IBLLocation;
                                                                              bottle.Tag = m_Tag;
                                                                              bottle.UPC = UpcItem;
                                                                              bottle.Quantity = qty;
                                                                              bottle.Nozzle.SetValues(
                                                                                  btlFillLevel.BottleNozzle);
                                                                          });
                        //var count = BLManagerFactory.Get().ManageInventory().GetAll().Count(I => I.UPCID == m_Item.UPCID && I.LocationID == bottle.FromLocation.LocationID);)
                        int invCount = BLManagerFactory.Get().ManageInventory().GetInventoryCount(UpcItem.UPCID, bottle.FromLocation.LocationID);
                        int btlCount = m_Bottles.Count(b => b.UPC.UPCID == UpcItem.UPCID && b.FromLocation.LocationID == bottle.FromLocation.LocationID);
                        if (0 >= invCount - btlCount)
                        {
                            bottle.NewInventory = true;
                        }
                        if (1 == (short)rdoInventoryFrom.EditValue && 0 == invCount)
                        {
                            bottle.NewInventory = true;
                        }
                        if (0 == (short)rdoInventoryFrom.EditValue) // Need to add new bottle to Inventory on save
                        {
                            bottle.NewInventory = true;
                        }

                        m_Bottles.Insert(0, bottle);
                        Log.Time( "CustomizeView", LogType.Debug, true, () => gvBottles.CustomizeView(false) );
                    }

                }
                catch (Exception err)
                {
                    Log.WriteException("frmBranding::BrandBottle", err);
                }
            });
        }


        private void txtUPC_KeyPress( object sender, KeyPressEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (AllowKeyboardEntry( ))
                {
                    if ((char) Keys.Enter == e.KeyChar || (char) Keys.Tab == e.KeyChar)
                    {
                        ValidateUPC(txtUPC.Text);
                        txtTagID.Focus( );
                    }
                }
                return true;
            });
        }


        private void txtTagID_KeyPress( object sender, KeyPressEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (AllowKeyboardEntry())
                {
                    if( (char)Keys.Enter == e.KeyChar || (char)Keys.Tab == e.KeyChar )
                    {
                        ValidateTagID(txtTagID.Text);
                        txtUPC.Focus( );
                    }
                }
                return true;
            });
        }

        private void frmBranding_KeyPress( object sender, KeyPressEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (false == ProcessKey(e))
                {
                    if (!AllowKeyboardEntry( ))
                    {
                        if (txtUPC.CanFocus)
                        {
                            txtUPC.Select();
                            txtUPC.Focus();
                        }
                        else
                        {
                            grdBottles.Focus();
                        }
                        if ((char) Keys.Enter == e.KeyChar || (char) Keys.Tab == e.KeyChar)
                        {
                            if (0 < m_KeyBuffer.Length)
                            {
                                if (m_KeyBuffer.ToString().StartsWith("045") && 10 == m_KeyBuffer.Length)
                                {
                                    txtTagID.Text = m_KeyBuffer.ToString();
                                    ValidateTagID(txtTagID.Text);
                                }
                                else
                                {
                                    txtUPC.Text = m_KeyBuffer.ToString();
                                    ValidateUPC(txtUPC.Text);
                                }
                                m_KeyBuffer.Clear();
                            }
                        }
                        else if (char.IsLetterOrDigit(e.KeyChar))
                        {
                            m_KeyBuffer.Append(e.KeyChar);
                        }
                    }
                }
                return true;
            }); 
        }

        private void chkUPCKeyboard_CheckedChanged( object sender, EventArgs e )
        {
            txtUPC.Enabled = txtTagID.Enabled = chkAllowKeyboardEntry.Checked;
        }


        private void SetBottleLevel( )
        {
            Log.MsgWrap(false, () =>
            {
                int[] rowIndex = gvBottles.GetSelectedRows();

                foreach (var i in rowIndex)
                {
                    IBLBrandedBottle row = gvBottles.GetRow(i) as IBLBrandedBottle;
                    row.Quantity = ( btlFillLevel.FillLevel / 100.0 ) * row.UPC.Size;
                }
                gvBottles.RefreshData();
                return true;
            });
        }

        private void bbtnRemove_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            gvBottles.DeleteSelectedRows();
        }

        private void bbtnSave_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                Log.Debug(string.Format("Branding - Save - {0} Items", m_Bottles.Count ));

                if (gvBottles.PostEditor() && gvBottles.UpdateCurrentRow())
                {
                    gvBottles.CloseEditor();
                }
                
                var man = BLManagerFactory.Get().ManageBrandedBottles();
                man.SaveBrandedBottles(m_Bottles.ToList());

                txtUPC.Text = string.Empty;
                btnEditUPC.Text = string.Empty;
                txtTagID.Text = string.Empty;

                m_Bottles.Clear();
                Log.Debug(string.Format("Branding - Save - Complete"));

                return true;
            });
        }

        private void gvBottles_SelectionChanged( object sender, DevExpress.Data.SelectionChangedEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                int[] selectedRows = gvBottles.GetSelectedRows();
                if( 0 < selectedRows.Count( ) )
                {
                    IBLBrandedBottle bottle = gvBottles.GetRow(selectedRows[0]) as IBLBrandedBottle;
                    if( null != bottle )
                    {
                        double quantity = bottle.Quantity ?? 0.0;
                        btlFillLevel.FillLevel = (int)(quantity/bottle.UPC.Size*100);
                        btlFillLevel.SetValues( bottle.Nozzle );
                    }
                }
                return true;
            });
        }


        private bool DoubleScanRemove( string _tagNumber )
        {
            return Log.MsgWrap(false, () =>
            {
                bool rc = false;
                IBLBrandedBottle bottle = m_Bottles.Where(B => B.Tag.TagNumber == _tagNumber).FirstOrDefault();
                if (null != bottle)
                {
                    rc = m_Bottles.Remove(bottle);
                }
                return rc;

            });
        }

        private void grdBottles_Click( object sender, EventArgs e )
        {

        }

        private void bbtnClose_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            this.Close();
        }

        private void lueEvent_EditValueChanged(object sender, EventArgs e)
        {
            if (txtUPC.CanFocus)
            {
                txtUPC.Select();
                txtUPC.Focus();
            }
            else
            {
                grdBottles.Focus();
            }
        }


        private void btnEditUPC_Click( object sender, EventArgs e )
        {
            ModifyUPC(UpcItem);
        }

        //#region IActivityControl Members

        //public List<Type> MessageType { get; protected set; }

        //public bool AddActivityItem( object _item )
        //{
        //    bool rc = true;
        //    if( InvokeRequired )
        //    {
        //        if( this.Created )
        //        {
        //            Invoke( (MethodInvoker)( ( ) => AddActivityItem( _item ) ) );
        //        }
        //    }
        //    else
        //    {
        //        rc = Log.MsgWrap(false, () =>
        //            {
        //                if (_item is DataTagActivity && this.Visible)
        //                {
        //                    Application.DoEvents();
        //                    DataTagActivity activity = _item as DataTagActivity;
        //                    IBLTag tag = BLManagerFactory.Get().ManageTags().Get(activity.TagID);
        //                    if (null != tag)
        //                    {
        //                        if (false == DoubleScanRemove( tag.TagNumber))
        //                        {
        //                            if (activity.ActivityType == (int)TagPhaseType.Connect)
        //                            {
        //                                m_Tag = tag;
        //                                AddBrandedItem();
        //                            }
        //                            // TODO: Check the validity of this...should it just be removed on disconnect?
        //                            if (activity.ActivityType == (int)TagPhaseType.Disconnect)
        //                            {
        //                                AddBrandedItem();
        //                            }
        //                        }

        //                    }
        //                    Application.DoEvents();
        //                }
        //                return true;
        //            });
        //    }
        //    return rc;
        //}

        //public string DisplayName { get { return "Branding"; } }
        //public Guid DisplayID { get { return new Guid("54E89245-9039-435A-828C-5745BF18BEF9"); } }


        //#endregion

        private void frmBranding_Shown(object sender, EventArgs e)
        {
            m_StandardNozzle = BLManagerFactory.Get().ManageStandardNozzles().Get(Guid.Empty);
            lciOnAttach.Visibility = LayoutVisibility.Never;
        }

        private void lueLocation_MouseLeave(object sender, EventArgs e)
        {
            //if (txtUPC.CanFocus)
            //{
            //    txtUPC.Select();
            //    txtUPC.Focus();
            //}
            //else
            //{
            //    grdBottles.Focus();
            //}
        }

        private void gvBottles_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view != null)
                {
                    var item = view.GetFocusedRow() as IBLBrandedBottle;
                    var price = view.Columns["Price"];
                    var newInv = view.Columns["NewInventory"];
                    if (view.FocusedColumn == newInv)
                    {
                        
                    }
                    else if (view.FocusedColumn == price && item.NewInventory == true)
                    {
                        
                    }
                    else
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

        private void gvBottles_HiddenEditor(object sender, EventArgs e)
        {
            //m_KeyBuffer.Clear();
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            var summary = new frmSummary<IBLBrandedBottle>();
            summary.DataItems = m_Bottles;
            summary.Show();
        }

        private void btnUPCSearch_Click(object sender, EventArgs e)
        {
            using( var frm = new frmUPCSearch() )
            {
                if (DialogResult.OK == frm.ShowDialog())
                {
                    this.UpcItem = frm.UPCItem;
                }
            }
        }

        private void frmBranding_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                if (0 < m_Bottles.Count)
                {
                    DialogResult result = MessageBox.Show(this, global::BeverageManagement.Properties.Resources.SaveBranding, global::BeverageManagement.Properties.Resources.Save, MessageBoxButtons.YesNoCancel);
                    if (DialogResult.Yes == result)
                    {
                        bbtnSave_ItemClick(sender, null);
                    }
                    else if (DialogResult.Cancel == result)
                    {
                        e.Cancel = true;
                    }
                }
                return true;
            });
        }



        //private void txtUPC_EditValueChanged(object sender, EventArgs e)
        //{
        //    IInventoryBLManager Man = BLManagerFactory.Get().ManageInventory();
        //    var Item = Man.GetAll().Where(I => I.UPC.UPCID.ToString() == txtUPC.Text);
        //    if (null != Item)
        //    {
        //        txtCost.Text = Item.FirstOrDefault().UPC.UnitPrice.ToString();
        //    }
        //}

        //private void sbSubtractCost_Click(object sender, EventArgs e)
        //{
        //    double value = Convert.ToDouble(txtCost.Text);
        //    if (0 < value)
        //    {
        //        txtCost.Text = ((Convert.ToDouble(txtCost.Text)) - .01).ToString("F2");
        //    }
        //}

        //private void sbAddCost_Click(object sender, EventArgs e)
        //{
        //    txtCost.Text = (.01 + (Convert.ToDouble(txtCost.Text))).ToString("F2");
        //}
    }
}