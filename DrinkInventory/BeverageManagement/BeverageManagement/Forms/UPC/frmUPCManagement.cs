using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using JaxisExtensions;
using Jaxis.Util.Log4Net;

namespace BeverageManagement
{
    public partial class frmUPCManagement : frmScannerCommands
    {
        //private ICategoryBLManager m_CategoryManager;
        //private IUPCItemBLManager m_UPCItemManager;
        //private IBLManagerFactory m_Factory = BLManagerFactory.Get( );
        private IBLUPCItem m_SelectedUPC;
        private NodeInfo m_NodeInfo;
        private BindingList<IUIUPCItem> m_UPCItems = null;

        public class NodeInfo
        {
            public List<object> NodeObjects { get; set; }
            public NodeInfo()
            {
                NodeObjects = new List< object >( );
            }
        }

        public frmUPCManagement( )
        {
            InitializeComponent( );
            treeUPC.OptionsBehavior.Editable = false;
            treeUPC.CustomDrawNodeCell += CommonUI.CustomDrawNodeCell;
            
            bbtnAddUPC.Enabled = false;
            bbtnUpdateUPC.Enabled = false;
            gvUPCItems.FocusedRowChanged += new FocusedRowChangedEventHandler( gridView1_FocusedRowChanged );

            bbtnAddSubCategory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler( bbtnAddSubCategory_ItemClick );
            bbtnAddCategory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler( bbtnAddCategory_ItemClick );
        }


        protected override void OnLoad( EventArgs e )
        {
            Log.Time( "frmUPCManagement_Load", LogType.Debug, true, ( ) =>
            {
                base.OnLoad( e );
                bbtnRefresh_ItemClick( null, null );
            } );
        }


        void gridView1_FocusedRowChanged( object sender, FocusedRowChangedEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (e.FocusedRowHandle != GridControl.InvalidRowHandle)
                {
                    m_SelectedUPC = ((ColumnView)sender).GetFocusedRow() as IBLUPCItem;
                    bbtnUpdateUPC.Enabled = true;
                }
                else
                {
                    m_SelectedUPC = null;
                    bbtnUpdateUPC.Enabled = false;
                }

                return true;
            });
        }

        void treeUPC_FocusedNodeChanged( object sender, FocusedNodeChangedEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if (null != e.Node && null != e.Node.Tag)
                {
                    bbtnAddUPC.Enabled = (e.Node.Level > (int)CategoryLevel.Category);
                    CategoryLevel level = (CategoryLevel)e.Node.Level;

                    m_NodeInfo = UPCUI.FilterUPCGrid(e.Node, level, gvUPCItems);
                }
                else
                {
                    m_NodeInfo = null;
                }

                return true;
            });
        }


        private void bbtnAddUPC_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                var t = treeUPC.FocusedNode.Tag as IBLCategory;
                if (null != t)
                {
                    var item = BLManagerFactory.Get().ManageUPCs().Create();
                    item.CategoryID = t.CategoryID;
                    item.RootCategoryID = t.ParentID.Value;
                    using (frmUPC upc = new frmUPC() {Item = item}) //, CurrentNodeInfo = m_NodeInfo } )
                    {
                        if (DialogResult.OK == upc.ShowDialog())
                        {
                            BLManagerFactory.Get().ManageUPCs().Save(item);
                            //                    var item = upc.Item as IUIUPCItem;
                            m_UPCItems.Add(item as IUIUPCItem);
                            RefreshTree(upc.Item);
                        }
                    }
                }
                return true;
            });
        }

        private void bbtnUpdateUPC_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                var UPC = BLManagerFactory.Get().ManageUPCs().Get(m_SelectedUPC.ObjectID);
                using (frmUPC upc = new frmUPC() { Item = UPC })//, CurrentNodeInfo = m_NodeInfo } )
                {
                    if (DialogResult.OK == upc.ShowDialog())
                    {
                        RefreshTree(upc.Item);
                    }
                }

                return true;
            });
        }

        private void bbtnAddSubCategory_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                using (frmSubCategoryAdd subCategoryAdd = new frmSubCategoryAdd() { CurrentNodeInfo = m_NodeInfo })
                {
                    if (DialogResult.OK == subCategoryAdd.ShowDialog())
                    {
                        RefreshTree(subCategoryAdd.Item);
                    }
                }

                return true;
            });
        }

        void bbtnAddCategory_ItemClick( object sender, DevExpress.XtraBars.ItemClickEventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                using (frmCategoryAdd categoryAdd = new frmCategoryAdd() { CurrentNodeInfo = null /*m_NodeInfo*/})
                {
                    if (DialogResult.OK == categoryAdd.ShowDialog())
                    {
                        RefreshTree(categoryAdd.Item);
                    }
                }

                return true;
            });
        }


        /// <summary>
        /// Use this event to paint the selected node as Highlighted (otherwise, it just has the dashed box around it).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void treeUPC_CustomDrawNodeCell( object sender, CustomDrawNodeCellEventArgs e )
        //{
        //    TreeList tree = sender as TreeList;
        //    if( e.Node == treeUPC.FocusedNode )
        //    {

        //        e.Graphics.FillRectangle( SystemBrushes.Window, e.Bounds );
        //        Rectangle rect = new Rectangle( e.EditViewInfo.ContentRect.Left,
        //                                        e.EditViewInfo.ContentRect.Top,
        //                                        Convert.ToInt32( e.Graphics.MeasureString( e.CellText, treeUPC.Font ).Width + 1 ),
        //                                        Convert.ToInt32( e.Graphics.MeasureString( e.CellText, treeUPC.Font ).Height ) );
        //        e.Graphics.FillRectangle( SystemBrushes.Highlight, rect);
        //        e.Graphics.DrawString( e.CellText, treeUPC.Font, SystemBrushes.HighlightText, rect);

        //        e.Handled = true;
        //    }
        //}

        void RefreshTree( IBLUPCItem item )
        {
            Log.MsgWrap(false, () =>
            {
                treeUPC.FocusedNodeChanged -= new FocusedNodeChangedEventHandler(treeUPC_FocusedNodeChanged);
                treeUPC.Nodes.Clear();
                UPCUI.LoadUPCTree(treeUPC);

                TreeListNode n = treeUPC.Nodes.OfType<TreeListNode>().SelectDeep(
                    p => p.Nodes.OfType<TreeListNode>()).ToList()
                    .FirstOrDefault(p => p.Tag is String &&
                                          ((IBLCategory)p.ParentNode.Tag).CategoryID == item.CategoryID
                                          && (String)p.Tag == item.Manufacturer);

                treeUPC.FocusedNodeChanged += new FocusedNodeChangedEventHandler(treeUPC_FocusedNodeChanged);
                if (null != n)
                {
                    treeUPC.FocusedNode = n;
                }
                item.ToType(m_SelectedUPC);
                gvUPCItems.RefreshData();

                return true;
            });
        }

        void RefreshTree( IBLCategory item )
        {
            Log.MsgWrap(false, () =>
            {
                treeUPC.FocusedNodeChanged -= new FocusedNodeChangedEventHandler(treeUPC_FocusedNodeChanged);
                treeUPC.Nodes.Clear();
                UPCUI.LoadUPCTree(treeUPC);

                bool isRoot = item.ParentID == null;

                TreeListNode n = treeUPC.Nodes.OfType<TreeListNode>().SelectDeep(
                    p => p.Nodes.OfType<TreeListNode>()).ToList()
                    .FirstOrDefault(p => p.Tag is IBLCategory
                                          && ((IBLCategory)p.Tag).CategoryID == item.CategoryID
                                          && ((IBLCategory)p.Tag).Name == item.Name);

                treeUPC.FocusedNodeChanged += new FocusedNodeChangedEventHandler(treeUPC_FocusedNodeChanged);
                if (null != n)
                {
                    treeUPC.FocusedNode = n;
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
            Log.MsgWrap(false, () =>
            {
                treeUPC.FocusedNodeChanged -=
                    new FocusedNodeChangedEventHandler(treeUPC_FocusedNodeChanged);
                treeUPC.Nodes.Clear();
                UPCUI.LoadUPCTree(treeUPC);
                treeUPC.FocusedNode = null;
                m_UPCItems = UPCUI.LoadUPCGrid(gvUPCItems);

                treeUPC.FocusedNodeChanged +=
                    new FocusedNodeChangedEventHandler(treeUPC_FocusedNodeChanged);

                return true;
            });
        }

        private void frmUPCManagement_Shown(object sender, EventArgs e)
        {
        //    ReloadData();
        }

        private void frmUPCManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            var items = gvUPCItems.DataSource as BindingList<IUIUPCItem>;
            if (null != items)
            {
                items.Clear();
            }
        }
    }

    public enum CategoryLevel
    {
        Category = 0,
        SubCategory = 1,
        Manufacturer = 2,
    }
}