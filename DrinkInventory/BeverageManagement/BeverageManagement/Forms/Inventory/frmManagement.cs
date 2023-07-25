using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Forms;
using BeverageManagement.Forms.UPC;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.DataAccess;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using JaxisExtensions;
using System.Collections;

namespace BeverageManagement
{
    public partial class frmManagement : frmScannerCommands
    {
        private StringBuilder m_KeyBuffer = new StringBuilder();

        private IBLUPCItem m_CurrentUPC = null;

        BindingList<IBLAssociatedBottle> m_Add = new BindingList<IBLAssociatedBottle>( );
        BindingList<IBLAssociatedBottle> m_Remove = new BindingList<IBLAssociatedBottle>( );
        private GridView m_CurrentGrid;
        private IBLStandardNozzle m_StandardNozzle;


        public frmManagement( )
        {
            InitializeComponent( );
            
            blFillLevel.OnFillLevelChanged = SetBottleLevel;
            blFillLevel.OnNozzleDiameterChanged = SetNozzleDiameter;
            m_ScannerCommands.Commands.Add('R', () => this.bbtnRemove_ItemClick(null, null));
            m_ScannerCommands.Commands.Add('S', () => this.bbtnSave_ItemClick(null, null));
        }

        private void frmManagement_Load( object sender, EventArgs e )
        {
            Log.Time( "frmManagement_Load", LogType.Debug, true, ( ) =>
            {
                //var locations = BLManagerFactory.Get().ManageLocations().GetAll().OrderBy(n => n.Name).ToList();
                List<IBLLocation> locations = null;
                Log.Time("GetStorageLocations", LogType.Debug, true, () =>
                {
                    locations = BLManagerFactory.Get().ManageLocations
                            ().GetStorageLocations().OrderBy(
                                n => n.Name).ToList();
                });

                lueToLocation.Properties.DataSource = locations;
                lueToLocation.Properties.DisplayMember = "Name";
                lueToLocation.Properties.ValueMember = "LocationID";
                lueToLocation.Properties.Columns.Add(
                    new DevExpress.XtraEditors.Controls.
                        LookUpColumnInfo("Name"));

//                locations = BLManagerFactory.Get().ManageLocations().GetAll().OrderBy(n => n.Name).ToList();
                Log.Time("GetStorageLocations", LogType.Debug, true, () =>
                {
                    locations = BLManagerFactory.Get().ManageLocations
                            ().GetStorageLocations().OrderBy(
                                n => n.Name).ToList();
                });
                var newInventory = BLManagerFactory.Get().ManageLocations().NewInventoryLocation;
                locations.Add( newInventory );
                lueFromLocation.Properties.DataSource = locations;
                lueFromLocation.Properties.DisplayMember = "Name";
                lueFromLocation.Properties.ValueMember = "LocationID";
                lueFromLocation.Properties.Columns.Add(
                    new DevExpress.XtraEditors.Controls.
                        LookUpColumnInfo( "Name" ) );

                if (1 == locations.Count)
                {
                    lueToLocation.ItemIndex = 0;
                }
                else
                {
                    lueToLocation.EditValue = Guid.Empty;
                }
                grdRemove.DataSource = m_Remove;
                gvRemove.CustomizeView(false);
                grdAdd.DataSource = m_Add;
                gvBottles.CustomizeView( false );
                //var font = new Font(lblAddLocation.Font.FontFamily, 14, FontStyle.Bold);
                //lblAddLocation.Font = font;
                //lblRemoveLocation.Font = font;
            } );
        }


        private bool AllowKeyboardEntry()
        {
            try
            {
                bool rc = chkAllowKeyboardEntry.Checked;
                if (false == rc)
                {
                    var view = gvBottles as GridView;
                    if (view != null)
                    {
                        var item = view.GetFocusedRow() as IBLBrandedBottle;
                        if (null != item)
                        {
                            var bottleCount = view.Columns["BottleCount"];
                            var price = view.Columns["Price"];
                            if (view.FocusedColumn == price && item.NewInventory == true)
                            {
                                rc = true;
                            }
                            else if (view.FocusedColumn == bottleCount)
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
        
        private void frmManagement_KeyPress( object sender, KeyPressEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (false == ProcessKey(e))
                {
                    if (!AllowKeyboardEntry())
                    {
                        if (!txtTagID.Focused)
                        {
                            if ((char)Keys.Enter == e.KeyChar || (char)Keys.Tab == e.KeyChar )
                            {
                                if (true == ProcessTag(m_KeyBuffer.ToString()))
                                {
                                    m_KeyBuffer.Clear();
                                    txtTagID.Text = string.Empty;
                                    txtTagID.Focus();
                                    e.Handled = true;
                                }
                            }
                            else if (char.IsLetterOrDigit(e.KeyChar))
                            {
                                m_KeyBuffer.Append(e.KeyChar);
                            }
                            else if ((char)Keys.Back == e.KeyChar)
                            {
                                m_KeyBuffer.Remove(m_KeyBuffer.Length - 1, 1);
                            }
                        }
                    }
                }
                return true;
            });
        }

        private bool ProcessTag(string _tagIdValue )
        {
            return Log.MsgWrap(false, () =>
            {
                string value = m_KeyBuffer.ToString();
                m_KeyBuffer.Clear();

                bool rc = true;
                if (!string.IsNullOrWhiteSpace(_tagIdValue))
                {
                    var bottleMan = BLManagerFactory.Get().ManageAssociatedBottles();
                    IBLAssociatedBottle bottle = bottleMan.Create();

                    if (value.ToString().StartsWith("045") && 10 == value.Length)
                    {
                        rc = ProcessPourTag(_tagIdValue, bottle);
                    }
                    else
                    {
                        ProcessUPCTag(_tagIdValue, bottle);
                    }
                }
                return rc;
            });
        }


        protected IBLUPCItem ValidateUPC(string _upcValue)
        {
            IBLUPCItem item = null;
            Log.MsgWrap(false, () =>
            {
                item = BLManagerFactory.Get().ManageUPCs().GetUPCByUPCNumber(_upcValue);
                if (null == item)
                {
                    Int64 value;
                    if (Int64.TryParse(_upcValue, out value))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            var upcLookup = new OnlineUPCLookup();
                            item = upcLookup.GetOnlineUPCData(_upcValue);
                            using (var upc = new frmUPC() { Item = item })
                            {
                                if (System.Windows.Forms.DialogResult.OK == upc.ShowDialog())
                                {
                                    item = upc.Item;
                                }
                            }
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
            return item;
        }

        private void ProcessUPCTag(string _upcNumber, IBLAssociatedBottle bottle)
        {
            var fromId = (Guid)( lueFromLocation.EditValue ?? Guid.Empty );
            if (null == m_CurrentUPC || m_CurrentUPC.ItemNumber != _upcNumber)
            {
                m_CurrentUPC = ValidateUPC(_upcNumber);    
            }
            
            if (null != m_CurrentUPC)
            {
                if (fromId == BLManagerFactory.Get().ManageLocations().NewInventoryLocation.LocationID)
                {
                    bottle.NewInventory = true;
                    bottle.FromLocation = BLManagerFactory.Get().ManageLocations().NewInventoryLocation;
                }
                else
                {
                    var count = BLManagerFactory.Get().ManageInventory().CheckAvailableInventory(m_CurrentUPC.UPCID, fromId);
                    if (count > m_Add.Count(b => b.UPC.UPCID == m_CurrentUPC.UPCID))
                    {
                        bottle.FromLocation =
                            BLManagerFactory.Get().ManageLocations().Get(
                                (Guid) (lueFromLocation.EditValue ?? Guid.Empty));
                    }
                    else
                    {
                        bottle.NewInventory = true;
                        bottle.FromLocation = BLManagerFactory.Get().ManageLocations().NewInventoryLocation;
                    }
                }

                bottle.UPC = m_CurrentUPC;
                bottle.ToLocation = BLManagerFactory.Get().ManageLocations().Get((Guid) lueToLocation.EditValue);
                bottle.Quantity = m_CurrentUPC.Size;
                bottle.BottleCount = 1;
                bottle.Price = Convert.ToDecimal(m_CurrentUPC.UnitPrice);

                var item = m_Add.Where(b => b.FromLocation.LocationID == bottle.FromLocation.LocationID 
                    && b.ToLocation.LocationID == bottle.ToLocation.LocationID 
                    && b.UPC.UPCID == bottle.UPC.UPCID
                    && b.NewInventory == bottle.NewInventory).FirstOrDefault();
                if (null != item)
                {
                    item.BottleCount++;
                }
                else
                {
                    m_Add.Insert(0, bottle);
                }
                grdAdd.RefreshDataSource();
            }
        }

        private bool ProcessPourTag(string _tagIdValue, IBLAssociatedBottle bottle)
        {
            return Log.Time( "ProcessPourTag", LogType.Debug, false, ( ) => 
            { 
                bool rc = true;

                var tagMan = BLManagerFactory.Get().ManageTags();

                IBLTag tag = tagMan.GetTagWithInventory( _tagIdValue ).FirstOrDefault();

                if (null != tag)
                {
                    if (false == DoubleScanRemove(tag))
                    {
                        bottle.Price = tag.CurrentInventory.Cost;
                        if (null != tag.CurrentInventory.Location)
                        {
                            bottle.Tag = tag;
                            bottle.UPC = tag.UPC;
                            if (null == tag.Nozzle && null == tag.UPC.Nozzle)
                            {
                                bottle.Nozzle.SetValues(m_StandardNozzle);
                            }
                            else if (null != tag.Nozzle)
                            {
                                bottle.Nozzle.SetValues(tag.Nozzle);
                            }
                            else
                            {
                                bottle.Nozzle.SetValues(tag.UPC.Nozzle);
                            }
                            if ((Guid?)lueToLocation.EditValue == tag.CurrentInventory.LocationID)
                            {
                                bottle.FromLocation = tag.CurrentLocation;
                                tag.CurrentLocation = null;
                                tag.Quantity = 0;
                                m_Remove.Add(bottle);
                            }
                            else
                            {
                                var currentLocation =
                                    lueToLocation.GetSelectedDataRow() as IBLLocation;
                                if (null == currentLocation)
                                {
                                    MessageBox.Show(
                                        global::BeverageManagement.Properties.Resources.
                                            SelectEvent);
                                    rc = false;
                                }
                                else
                                {
                                    bottle.FromLocation = tag.CurrentInventory.Location;
                                    bottle.ToLocation = currentLocation;
                                    tag.CurrentLocation = currentLocation;
                                    m_Add.Insert(0, bottle);
                                }
                            }
                        }
                        else
                        {
                            tag.LocationID = (Guid) lueToLocation.EditValue;
                            bottle.Tag = tag;
                            bottle.UPC = tag.UPC;
                            m_Add.Insert( 0, bottle);
                        }
                        gvBottles.CustomizeView(false);
                    }
                }
                else
                {
                    rc = false;
                    if (System.Windows.Forms.DialogResult.Yes ==
                        MessageBox.Show(this,
                                        string.Format(
                                            global::BeverageManagement.Properties.Resources.
                                                TagNotBranded, _tagIdValue), string.Empty,
                                        MessageBoxButtons.YesNo))
                    {
                        using (var frm = new frmBranding())
                        {
                            frm.SelectedFromLocation = lueFromLocation.GetSelectedDataRow() as IBLLocation;
                            frm.SelectedToLocation = lueToLocation.GetSelectedDataRow() as IBLLocation;
                            frm.SelectedUPCItem = m_CurrentUPC;
                            frm.ShowDialog();
                            rc = true;
                        }
                    }

                } 
                return rc;
            });
        }

        private bool DoubleScanRemove(IBLTag tag)
        {
            return Log.MsgWrap(false, () =>
            {
                bool rc = false;

                IBLAssociatedBottle bottle = m_Add.Where(B => null != B.Tag && B.Tag.TagID == tag.TagID).FirstOrDefault();
                if (null != bottle)
                {
                    rc = m_Add.Remove(bottle);
                }
                else
                {
                    bottle = m_Remove.Where(B => null != B.Tag && B.Tag.TagID == tag.TagID).FirstOrDefault();
                    if (null != bottle)
                    {
                        rc = m_Remove.Remove(bottle);
                    }
                }
                return rc;
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
                        if (string.IsNullOrWhiteSpace(m_KeyBuffer.ToString()))
                        {
                            m_KeyBuffer.Append( txtTagID.Text );
                        }
                        if( true == ProcessTag( m_KeyBuffer.ToString( ) ) )
                        {
                            m_KeyBuffer.Clear();
                            txtTagID.Text = string.Empty;
                            txtTagID.Focus();
                        }
                    }
                }
                return true;
            });
        }

        private void bbtnSave_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                try
                {
                    Log.Debug(string.Format("Management - Save - Add {0} Items, Remove {1} Items", m_Add.Count, m_Remove.Count));

                    Cursor.Current = Cursors.WaitCursor;
                    var man = BLManagerFactory.Get().ManageTags();
                    var invMan = BLManagerFactory.Get().ManageInventory();

                    if (gvBottles.PostEditor() && gvBottles.UpdateCurrentRow())
                    {
                        gvBottles.CloseEditor();
                    }


                    foreach (var bottle in m_Add)
                    {
                        AddBottle(bottle, man, invMan);
                    }
                    foreach (var bottle in m_Remove)
                    {
                        bottle.Tag.LocationID = Guid.Empty;
                        man.Save(bottle.Tag);
                        invMan.RemoveTagFromInventory(bottle.Tag.TagID, ExitReasons.Dispensed);
                    }

                    m_Add.Clear();
                    m_Remove.Clear();
                    Log.Debug(string.Format("Management - Save - Complete"));
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
                return true;
            });
        }

        private void AddBottle(IBLAssociatedBottle bottle, ITagBLManager man, IInventoryBLManager invMan)
        {
            var tag = bottle.Tag;
            if (null != tag)
            {
                if (null == tag.CurrentInventory)
                {
                    var fromID = (bottle.FromLocation != null)
                                 ? bottle.FromLocation.LocationID
                                 : BLManagerFactory.Get().ManageLocations().NewInventoryLocation.LocationID;
                    invMan.MoveUnTaggedInventory(bottle.UPC.UPCID, fromID, bottle.ToLocation.LocationID);
                }
                var nozzleMan = BLManagerFactory.Get().ManageStandardNozzles();

                if (null != bottle.Nozzle)
                {
                    if (bottle.Nozzle.StandardNozzleID != Guid.Empty &&
                        !bottle.Nozzle.CheckNozzle(tag.UPC.Nozzle))
                    {
                        nozzleMan.Save(bottle.Nozzle);
                        tag.Nozzle = bottle.Nozzle;
                    }
                }
                // TODO: If nozzle is different from Standard Nozzle or the UPC's nozzle, save it as well...associated with the tag.
                man.Save(bottle.Tag);
                //invMan.TagInventory(bottle);
            }
            else // Unbranded Inventory added to the location.
            {
                int newBottleCount = 0;
                Guid newInventoryLocationID = BLManagerFactory.Get().ManageLocations().NewInventoryLocation.LocationID;

                var fromID = (bottle.FromLocation != null)
                                 ? bottle.FromLocation.LocationID
                                 : newInventoryLocationID;

                if (fromID != newInventoryLocationID )
                {
                    var available = BLManagerFactory.Get().ManageInventory().CheckAvailableInventory(bottle.UPC.UPCID, fromID);
                    if (available < bottle.BottleCount)
                    {
                        newBottleCount = bottle.BottleCount - available;
                        for (int i = 0; i < newBottleCount; ++i)
                        {
                            invMan.MoveUnTaggedInventory(bottle.UPC.UPCID, newInventoryLocationID, bottle.FromLocation.LocationID);
                        }
                    }
                }

                for (int i = 0; i < bottle.BottleCount; ++i)
                {
                    invMan.MoveUnTaggedInventory(bottle.UPC.UPCID, fromID, bottle.ToLocation.LocationID);
                }
            }
        }

        private void frmManagement_Shown( object sender, EventArgs e )
        {
            txtTagID.Select( );
            if (txtTagID.CanFocus)
            {
                txtTagID.Select();
                txtTagID.Focus();
            }
            else
            {
                grdAdd.Focus();
            }
            m_StandardNozzle = BLManagerFactory.Get().ManageStandardNozzles().Get(Guid.Empty);
        }

        private void chkUPCKeyboard_CheckedChanged( object sender, EventArgs e )
        {
            txtTagID.Enabled = chkAllowKeyboardEntry.Checked;
        }

        private void lueEvent_EditValueChanged( object sender, EventArgs e )
        {
            txtTagID.Select();
            if (txtTagID.CanFocus)
            {
                txtTagID.Select();
                txtTagID.Focus();
            }
            else
            {
                grdAdd.Focus();
            }
            //lblAddLocation.Text = lueLocation.Text;
            //lblRemoveLocation.Text = lueLocation.Text;
        }


        private void SetBottleValue( Action<IBLBrandedBottle> _process )
        {
            Log.MsgWrap(false, () =>
            {
                if (null != m_CurrentGrid)
                {
                    int[] rowIndex = m_CurrentGrid.GetSelectedRows();

                    foreach (var i in rowIndex)
                    {
                        IBLBrandedBottle row = m_CurrentGrid.GetRow(i) as IBLBrandedBottle;
                        _process(row);
                    }
                    m_CurrentGrid.RefreshData();
                }
                return true;
            });
        }

        private void SetBottleLevel( )
        {
            SetBottleValue( ( IBLBrandedBottle _row ) => { _row.Quantity = ( blFillLevel.FillLevel / 100.0 ) * _row.UPC.Size; } );
        }

        private void SetNozzleDiameter( )
        {
            SetBottleValue( ( IBLBrandedBottle _row ) => _row.Nozzle.SetValues( blFillLevel.BottleNozzle ));
        }

        private void GvBottlesRowClick( object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                m_CurrentGrid = sender as GridView;
                if (null != m_CurrentGrid)
                {
                    int[] selectedRows = m_CurrentGrid.GetSelectedRows();
                    if (0 < selectedRows.Count())
                    {
                        var bottle = m_CurrentGrid.GetRow(selectedRows[0]) as IBLBrandedBottle;
                        if (null != bottle)
                        {
                            double quantity = bottle.Quantity ?? 0.0;
                            blFillLevel.FillLevel = (int) (quantity/bottle.UPC.Size*100);
                            blFillLevel.SetValues(bottle.Nozzle);
                        }
                    }
                }
                return true;
            });
        }

        private void bbtnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] selectedRows = m_CurrentGrid.GetSelectedRows();
                if (0 < selectedRows.Count())
                {
                    var bottle = m_CurrentGrid.GetRow(selectedRows[0]) as IBLAssociatedBottle;
                    if (null != bottle)
                    {
                        m_Add.Remove(bottle);
                    }
                }

            }
            catch (Exception err)
            {
                Log.WriteException( "bbtnRemove_ItemClick", err);
            }
        }


        private void gvBottles_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view != null)
                {
                    var item = view.GetFocusedRow() as IBLBrandedBottle;
                    var bottleCount = view.Columns["BottleCount"];
                    var price = view.Columns["Price"];
                    if (view.FocusedColumn != price || item.Tag != null )
                    {
                        e.Cancel = true;
                    }
                    if( view.FocusedColumn == bottleCount )
                    {
                        e.Cancel = false;
                    }
                }
            }
            catch (Exception err)
            {
                Log.WriteException("gvBottles_ShowingEditor", err);
                throw;
            }
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            var frm = new frmSummary<IBLAssociatedBottle>();
            var button = sender as SimpleButton;
            if (null != button && button.Name == "btnRemoveSummary")
            {
                var data = m_Remove.GroupBy(u => u.UPC);

                frm.DataItems = m_Remove;
            }
            else
            {
                var data = m_Add.GroupBy(u => u.UPC).ToList();
                int count = data.Count();

                frm.DataItems = m_Add;
            }
            frm.Show();
        }

        private void btnUPCSearch_Click(object sender, EventArgs e)
        {
            using (var frm = new frmUPCSearch())
            {
                if (DialogResult.OK == frm.ShowDialog())
                {
                    ProcessTag(frm.UPCItem.ItemNumber);
                }
            }

        }

        private void frmManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                if (0 < m_Add.Count || 0 < m_Remove.Count)
                {
                    DialogResult result = MessageBox.Show(this, global::BeverageManagement.Properties.Resources.SaveInventory, global::BeverageManagement.Properties.Resources.Save, MessageBoxButtons.YesNoCancel);
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
    }
}