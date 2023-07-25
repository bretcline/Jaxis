using System;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Forms;
using BeverageManagement.Forms.Inventory;
using BeverageManagement.Reports;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IUIDataItems;
using Jaxis.Util.Log4Net;
using HScrollBar = DevExpress.XtraEditors.HScrollBar;

namespace BeverageManagement
{
    public partial class frmInventory : frmScannerCommands
    {
        private IBLManagerFactory m_Factory = BLManagerFactory.Get( );

        private Dictionary<string, IUIInventoryItemView> m_ParChanges = new Dictionary<string, IUIInventoryItemView>();

        public frmInventory( )
        {
            Log.MsgWrap(false, () =>
            {
                InitializeComponent();
                treeInventory.OptionsBehavior.Editable = false;

                m_ScannerCommands.Commands.Add('X', () => this.bbtnBranding_ItemClick(null, null));
                m_ScannerCommands.Commands.Add('U', () => this.bbtnUnBrandBottle_ItemClick(null, null));
                m_ScannerCommands.Commands.Add('E', () => this.bbtnEditUPC_ItemClick(null, null));

                return true;
            });
        }

        private void frmInventory_Load( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                BuildLocationTree();
                return true;
            });
        }

        private void BuildLocationTree()
        {
            Log.MsgWrap(false, () =>
            {
                treeInventory.CustomDrawNodeCell -= CommonUI.CustomDrawNodeCell;
                treeInventory.FocusedNodeChanged -= new FocusedNodeChangedEventHandler(treeInventory_FocusedNodeChanged);

                treeInventory.Nodes.Clear();
                InventoryUI.LoadInventoryTree(treeInventory);

                treeInventory.FocusedNodeChanged += new FocusedNodeChangedEventHandler(treeInventory_FocusedNodeChanged);
                treeInventory.CustomDrawNodeCell += CommonUI.CustomDrawNodeCell;

                return true;
            });
        }

        void treeInventory_FocusedNodeChanged( object sender, FocusedNodeChangedEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (null != e.Node.Tag)
                {
                    using (CursorManager.Create())
                    {
                        StringBuilder builder = new StringBuilder();
                        string Filter = InventoryUI.BuildFilter(e.Node, builder);
                        gvInventoryItems.ActiveFilterString = string.Format("{0} {1}", Filter, builder.ToString());
                    }
                }

                return true;
            });
        }

        private void bbtnAddInventory_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                using (var frm = new frmInventoryAdd())
                {
                    Log.Debug(string.Format("Inventory - AddInventory - Complete"));
                    frm.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == frm.ShowDialog(this))
                    {
                        Log.Debug(string.Format("Inventory - AddInventory - Complete"));
                        ReloadData();
                    }
                    else
                    {
                        Log.Debug(string.Format("Inventory - AddInventory - Complete"));
                    }
                }

                return true;
            });
        }

        private void bbtnAddLocation_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                using (var frm = new frmLocationAdd())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                    BuildLocationTree();
                }
                return true;
            });
        }

        private void bbtnMoveInventory_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                using (var frm = new frmInventoryMove())
                {
                    Log.Debug(string.Format("Inventory - MoveInventory - Complete"));
                    frm.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == frm.ShowDialog(this))
                    {
                        Log.Debug(string.Format("Inventory - MoveInventory - Complete"));
                        ReloadData();
                    }
                    else
                    {
                        Log.Debug(string.Format("Inventory - MoveInventory - Complete"));
                    }
                }

                return true;
            });
        }

        private void bbtnBranding_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                using (var frm = new frmBranding())
                {
                    Log.Debug(string.Format("Inventory - Branding - Complete"));

                    frm.SelectedToLocation = treeInventory.Selection[0].Tag as IBLLocation;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == frm.ShowDialog(this))
                    {
                        Log.Debug(string.Format("Inventory - Branding - Complete"));
                        ReloadData();
                    }
                    else
                    {
                        Log.Debug(string.Format("Inventory - Branding - Complete"));
                    }
                }
                return true;
            });
            
        }

        private void bbtnRefresh_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            ReloadData();
        }


        public void ReloadData()
        {
            Log.Debug(string.Format("Inventory - ReloadData"));

            InventoryUI.LoadGrid(grdInventoryItems, true);

            if ( null != gvInventoryItems.Columns["TaggedQuantity"] &&  0 == gvInventoryItems.Columns["TaggedQuantity"].Summary.Count)
            {
                gvInventoryItems.Columns["TaggedQuantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
                gvInventoryItems.Columns["StockQuantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
                gvInventoryItems.Columns["TotalQuantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
                gvInventoryItems.Columns["TotalCost"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);

                gvInventoryItems.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalQuantity");
            }
            Log.Debug(string.Format("Inventory - ReloadData - Complete"));
        }

        private void bbtnUnBrandBottle_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                using (frmInventoryUnBrand frm = new frmInventoryUnBrand())
                {
                    Log.Debug(string.Format("Inventory - UnbrandBottle"));
                    frm.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == frm.ShowDialog(this))
                    {
                        Log.Debug(string.Format("Inventory - UnbrandBottle - Complete"));
                        ReloadData();
                    }
                    else
                    {
                        Log.Debug(string.Format("Inventory - UnbrandBottle - Complete"));
                    }
                }
                return true;
            });
        }


        private void bbtnReceiveMove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                using (var frm = new frmManagement())
                {
                    Log.Debug(string.Format("Inventory - ReceiveMove"));
                    frm.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == frm.ShowDialog(this))
                    {
                        Log.Debug(string.Format("Inventory - ReceiveMove - Complete"));
                        ReloadData();
                    }
                    else
                    {
                        Log.Debug(string.Format("Inventory - ReceiveMove - Complete"));
                    }
                }
                return true;
            });
        }

        private void frmInventory_Shown(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void bbtnEditUPC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                var item = gvInventoryItems.GetFocusedRow() as IBLInventoryItemView;
                if (null != item)
                {
                    var upc = BLManagerFactory.Get().ManageUPCs().Get(item.UPCID);
                    using (var frm = new frmUPC() {Item = upc}) //, CurrentNodeInfo = m_NodeInfo } )
                    {
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.ShowDialog(this);
                    }
                }
                return true;
            });
        }

        private void gvInventoryItems_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view != null)
                {

                    var allowModify = view.Columns["StockQuantity"];
                    GridColumn isActiveColumn = view.Columns["ParBottleCount"];
                    if (view.FocusedColumn != isActiveColumn)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        var item = view.GetFocusedRow() as IUIInventoryItemView;
                        if (null != item)
                        {
                            m_ParChanges[item.LocationName + item.Name] = item;
                            bbtnSave.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Log.WriteException( "gvInventoryItem_ShowingEditor", err);
                throw;
            }
        }

        private void bbtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Log.Debug( string.Format( "Inventory - bbtSave_ItemClick - {0} items", m_ParChanges.Count ) );
                if (gvInventoryItems.PostEditor() && gvInventoryItems.UpdateCurrentRow())
                {
                    gvInventoryItems.CloseEditor();
                }

                foreach (var item in m_ParChanges.Values)
                {
                    BLManagerFactory.Get().ManageInventoryItemViews().Save(item as IBLInventoryItemView);
                }

                frmPopup.ShowPopup( "Saved Par Levels.");

            }
            catch (Exception err)
            {
                Log.WriteException("bbtnSave_ItemClick", err);
            }
            finally
            {
                bbtnSave.Enabled = false;
                m_ParChanges.Clear();
            }
        }

        private void gvInventoryItems_RowStyle(object sender, RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle < 0 || view == null) return;
            var row = view.GetRow(e.RowHandle);
            if (row is IBLInventoryItemView)
            {
                var itemView = (IBLInventoryItemView)row;
                if (itemView.ParBottleCount > itemView.TotalQuantity)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.BackColor2 = Color.White;
                }
                if (itemView.ParBottleCount < itemView.TotalQuantity)
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.BackColor2 = Color.White;
                }
            }
        }

        private void bbtnModifyStockItems_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmAdjustStockInventory())
            {
                if (DialogResult.OK == frm.ShowDialog())
                {
                    this.ReloadData();
                }
            }
        }

        private void bbtnReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView View = grdInventoryItems.MainView as GridView;
            if (View != null)
            {
                ReportFomat.Format(grdInventoryItems, printingSystem1, "Inventory");
            }
        }
    }
}