using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;
using JaxisExtensions;

namespace BeverageManagement.Forms
{
    public class EnumItem<T>
    {
        public T EnumValue { get; set; }
        public string EnumName { get; set; }
    }
    
    public static class CommonUI
    {
        public static void CustomDrawNodeCell( object sender, CustomDrawNodeCellEventArgs e )
        {
            // obtaining brushes for cells of focused and unfocused nodes
            Brush backBrush, foreBrush;
            if( e.Node != ( (TreeList) sender ).FocusedNode )
            {
                backBrush = new LinearGradientBrush( e.Bounds, Color.Empty, Color.Empty,
                  LinearGradientMode.Horizontal );
                foreBrush = new SolidBrush( Color.Black );
            }
            else
            {
                backBrush = new SolidBrush( Color.SteelBlue );
                foreBrush = new SolidBrush( Color.White );
            }
            // filling the background
            e.Graphics.FillRectangle( backBrush, e.Bounds );
            // painting node value
            e.Graphics.DrawString( e.CellText, e.Appearance.Font, foreBrush, e.Bounds,
              e.Appearance.GetStringFormat( ) );

            // prohibiting default painting
            e.Handled = true;
        }

        public static void NozzleDiameterValidation( object sender, ChangingEventArgs e )
        {
            var mask = new Regex( @"^[0-9]{1,2}([\.]+[0-9]{0,2})?$" );
            var te = sender as TextEdit;
            if ( null != te && !String.IsNullOrEmpty( e.NewValue.ToString( ) ) )
            {
                if ( !mask.Match( e.NewValue.ToString( ) ).Success )
                {
                    e.Cancel = true;
                }
            }
        }

        public static void LoadNozzleTypes( ComboBoxEdit _control )
        {
            Log.MsgWrap(false, () =>
            {
                _control.Properties.Items.Add(NozzleShapes.Round);
                _control.Properties.Items.Add(NozzleShapes.Oval);
                _control.Properties.Items.Add(NozzleShapes.Rectangle);
                _control.Properties.Items.Add(NozzleShapes.RollerBall);

                return true;
            });
        }

        public static void SetupNamedObjectEdits(this LookUpEdit _editor, string _displayName, string _valueName)
        {
            Log.MsgWrap(false, () =>
            {
                _editor.Properties.Columns.Clear();
                _editor.Properties.ValueMember = _valueName;
                _editor.Properties.ShowHeader = false;
                _editor.Properties.DisplayMember = _displayName;
                _editor.Properties.Columns.Add(new LookUpColumnInfo(_displayName));

                return true;
            });
        }
        
        public static void SetupNamedObjectEdits(this List<LookUpEdit> _editors, string _displayName, string _valueName)
        {
            Log.MsgWrap(false, () =>
            {
                _editors.ForEach(l => l.SetupNamedObjectEdits(_displayName, _valueName));

                return true;
            });
        }

        // Return true if the LookUpEdit's EditValue is 1) null, 2) is a null or empty string, 3) is a Guid and has Guid.Empty value.
        public static bool IsLUENullOrEmpty( this LookUpEdit _lue )
        {
            return Log.MsgWrap(false, () =>
            {
                Guid? tempGuid;
                return (null == _lue.EditValue || String.IsNullOrEmpty(_lue.EditValue.ToString())
                         || ((tempGuid = _lue.EditValue as Guid?).HasValue && tempGuid == Guid.Empty));
            });
        }

        public static IList<EnumItem<T>> GetEnumItems<T>()
        {
            var values = Enum.GetValues(typeof(T));
            return (from object value in values
                    let name = Enum.GetName(typeof(T), value)
                    select new EnumItem<T>() { EnumName = name, EnumValue = (T)value }).ToList();
        }
    }

    public static class UPCUI
    {
        public static void LoadUPCTree( TreeList _tree )
        {
            Log.Time("LoadUPC Tree", LogType.Debug, false, () =>
            {
                using (CursorManager.Create())
                {
                    _tree.CustomDrawNodeCell -= CommonUI.CustomDrawNodeCell;
                    _tree.CustomDrawNodeCell += CommonUI.CustomDrawNodeCell;

                    _tree.Nodes.Clear();

                    var allCategories = BLManagerFactory.Get().ManageCategories().GetAll();
                    var categories = allCategories.Where(c => c.ParentID == null).OrderBy(c => c.Name);
                    var subCats = allCategories.Where(c => c.ParentID != null).OrderBy(c => c.Name);
                    var manufacturer = BLManagerFactory.Get().ManageManufacturerViews().GetAll().OrderBy(m => m.Name).ToList();

                    var items = BLManagerFactory.Get().ManageUPCs().GetAll().ToList().OrderBy(u => u.Name); ;

                    foreach (var blCategory in categories)
                    {
                        IBLCategory category = blCategory;
                        TreeListNode categoryNode = _tree.AppendNode(null, null);
                        categoryNode.SetValue("Name", category.Name);
                        categoryNode.Tag = category;

                        var localSubs = subCats.Where(C => C.ParentID == category.CategoryID).OrderBy(C => C.Name);
                        foreach (var blSub in localSubs)
                        {
                            IBLCategory sub = blSub;
                            TreeListNode subNode = _tree.AppendNode(null, categoryNode);
                            subNode.SetValue("Name", sub.Name);
                            subNode.Tag = sub;

                            //var manufacturer =
                            //    (from I in items
                            //    where I.CategoryID == sub.CategoryID
                            //    group I by I.Manufacturer into elements
                            //    //orderby Name
                            //    select new { name = elements.Key } ).OrderBy( m=> m.name);

                            var subID = sub.CategoryID;
                            var manItems = manufacturer.Where(M => M.CategoryID == subID).OrderBy( m => m.Name).ToList();

                            foreach (var man in manItems)
                            {
                                TreeListNode t = _tree.AppendNode(null, subNode);
                                t.SetValue("Name", man.Name);
                                t.Tag = man.Name;
                            }
                        }
                    }
                }

                return true;
            });
        }

        public static BindingList<IUIUPCItem> LoadUPCGrid( GridView _view )
        {
            return Log.Time("LoadUPCGrid",LogType.Debug, false, () =>
            {
                var oldData = _view.GridControl.DataSource as BindingList<IUIUPCItem>;
                if (null != oldData)
                {
                    oldData.Clear();
                }
                var rc = new BindingList<IUIUPCItem>(BLManagerFactory.Get().ManageUPCs().GetAllView().OfType<IUIUPCItem>().ToList());
                _view.GridControl.DataSource = rc;
                _view.CustomizeView();
                return rc;
            });
        }

        public static frmUPCManagement.NodeInfo FilterUPCGrid( TreeListNode _node, CategoryLevel _level, GridView _view )
        {
            return Log.MsgWrap(false, () =>
            {
                IBLCategory category = null;

                var rc = new frmUPCManagement.NodeInfo();

                // Setup recursion to run up the tree to the base node and come back bringing the relevant Data object for each level.
                Action<TreeListNode, List<object>> rec = null;
                rec = (t1, t2) =>
                {
                    if (null != t1.ParentNode)
                    {
                        rec(t1.ParentNode, t2);
                    }
                    t2.Add(t1.Tag);
                    return;
                };

                // The recursion is triggered here.
                rec(_node, rc.NodeObjects);

                // The data to be displayed depends on the level in the tree.  
                // If a top level item is selected, data contains all the UPCs whose CategoryID matches a Category whose 
                // ParentID is the selected top level item.
                // If a second level item is selected, data contains all the UPC's whose CategoryID matches the selected item.
                // If a third level item is selected, data contains all the UPC's whose Manufacturer matches the selected item
                // but within the scope of the selected item's parent.);
                using (CursorManager.Create())
                {
                    string Filter = string.Empty;
                    switch (_level)
                    {
                        case CategoryLevel.Category:
                            {
                                category = _node.Tag as IBLCategory;
                                Filter = string.Format("[RootCategoryID] = '{0}'", category.CategoryID);
                                break;
                            }
                        case CategoryLevel.SubCategory:
                            {
                                category = _node.Tag as IBLCategory;
                                Filter = string.Format("[CategoryID] = '{0}'", category.CategoryID);
                                break;
                            }
                        case CategoryLevel.Manufacturer:
                            {
                                category = _node.ParentNode.Tag as IBLCategory;
                                var manufacturer = _node.Tag as string;
                                Filter = string.Format("[CategoryID] = '{0}' AND [Manufacturer] = '{1}'", category.CategoryID, manufacturer);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    try
                    {
                        _view.ActiveFilterString = Filter;
                    }
                    catch
                    {
                    }
                }
                return rc;
            });
        }
    }


    public static class InventoryUI
    {
        private static DateTime m_LastGridRefresh = DateTime.Now.AddSeconds(-1.0);  // changed it back cause 5 minutes makes the app unusable.
        public static void LoadGrid(GridControl _grid, bool _includeTagged)
        {
            Log.Time( "InventoryUI - LoadGrid", LogType.Debug, false, () =>
            {
                if (m_LastGridRefresh < DateTime.Now.AddSeconds(-0.5))
                {
                    _grid.BeginUpdate();
                    try
                    {
                        var oldData = _grid.DataSource as BindingList<IUIInventoryItemView>;
                        if (null != oldData)
                        {
                            oldData.Clear();
                        }

                        var items = BLManagerFactory.Get().ManageInventoryItemViews().GetAll().ToList();
                        if (0 < items.Count)
                        {
                            _grid.DataSource = new BindingList<IUIInventoryItemView>(items.Cast<IUIInventoryItemView>().ToList());
                            _grid.DefaultView.CustomizeView(false);
                            m_LastGridRefresh = DateTime.Now;
                        }
                        items.Clear();
                    }
                    finally
                    {
                        _grid.EndUpdate();
                    }
                }
                return true;
            });
        }

        public static void LoadInventoryTree( TreeList _tree )
        {
            Log.Time("LoadInventoryTree", LogType.Debug, false, () =>
            {
                _tree.CustomDrawNodeCell -= CommonUI.CustomDrawNodeCell;
                _tree.CustomDrawNodeCell += CommonUI.CustomDrawNodeCell;

                _tree.Nodes.Clear();
                var treeNodes = new Dictionary<Guid, TreeListNode>();
                var nodeList = new List<TreeListNode>();

                var allNodes = BLManagerFactory.Get().ManageLocations().GetAll().ToList();
                var rootNodes = allNodes.Where(L => L.ParentID == null).ToList();
                var otherNodes = allNodes.Where(L => L.ParentID != null).ToList();

                var org = BLManagerFactory.Get().ManageOrganizations().GetAll().FirstOrDefault();
                var orgNode = _tree.AppendNode(null, null);
                orgNode.SetValue("Name", org.Name);
                orgNode.Tag = org;

                foreach (var location in rootNodes)
                {
                    var node = _tree.AppendNode(null, orgNode);
                    node.SetValue("Name", location.Name);
                    node.Tag = location;
                    treeNodes.Add(location.LocationID, node);
                    AddNodes(_tree, node, nodeList, otherNodes);
                }
                _tree.ExpandAll();

                return true;
            });
        }

        public static void AddTreeNode( TreeListNode _current, IBLLocation _newLocation )
        {
            Log.MsgWrap(false, () =>
            {
                var node = _current.TreeList.AppendNode(null, _current);
                node.SetValue("Name", _newLocation.Name);
                node.Tag = _newLocation;

                return true;
            });
        }

        public static void AddNodes( TreeList _tree, TreeListNode _parent, List<TreeListNode> _nodeList, List<IBLLocation> _locations )
        {
            Log.MsgWrap("AddNodes", LogType.Debug, false, () =>
            {
                var parent = _parent.Tag as IBLLocation;
                if (null != parent)
                {
                    var otherNodes = _locations.Where(L => L.ParentID == parent.ObjectID).ToList();
                    foreach (var location in otherNodes)
                    {
                        var node = _tree.AppendNode(null, _parent);
                        node.SetValue("Name", location.Name);
                        node.Tag = location;
                        _nodeList.Add(node);

                        AddNodes(_tree, node, _nodeList, _locations);
                    }
                }

                return true;
            });
        }

        public static string BuildFilter( TreeListNode _node, StringBuilder _builder )
        {
            return Log.MsgWrap(false, () =>
            {
                string rc = string.Empty;
                if (_node.Tag is IBLLocation || _node.Tag is IBLOrganization)
                {
                    var loc = _node.Tag as IBLLocation;
                    if (null != loc)
                    {
                        rc = string.Format(" ( LocationName = '{0}' ) ", loc.Name);
                    }
                    foreach (TreeListNode child in _node.Nodes)
                    {
                        _builder.Append(String.Format(" OR ({0}) ", BuildFilter(child, _builder)));
                    }
                }
                return rc;
            });
        }
    }


    public static class TagUI
    {
        public static IUITag ValidateTagID( string _tagId, BindingList<IUITag> _tags )
        {
            IUITag rc = Log.MsgWrap(false, () =>
            {
                IUITag tag = null;
                if (!DoubleScanRemove(_tagId, _tags))
                {
                    var man = BLManagerFactory.Get().ManageTags();
                    tag = man.GetTagWithInventory(_tagId).FirstOrDefault() as IUITag;
                    if (null == tag )
                    {
                        MessageBox.Show(string.Format("Tag {0} is not currently associated with a bottle.", _tagId ) );
                    }
                    else
                    {
                        _tags.Add(tag);
                    }
                }
                return tag;
            });
            return rc;
        }

        public static bool DoubleScanRemove( string _tagId, BindingList<IUITag> _tags )
        {
            return Log.MsgWrap(false, () =>
            {
                var rc = false;
                var bottle = _tags.Where(B => B.TagNumber == _tagId).FirstOrDefault();
                if (null != bottle)
                {
                    rc = _tags.Remove(bottle);
                }
                return rc;
            });
        }
    }
}
