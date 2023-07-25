using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace BeverageManagement.Forms.Inventory
{
    public partial class frmLocationAdd : DevExpress.XtraEditors.XtraForm
    {
        private IBLOrganization m_Organization = null;
        private IBLLocation m_Location = null;
        private IBLLocation m_ParentLocation = null;
        private TreeListNode m_Node = null;
        private bool m_AddNew = false;

        public IBLLocation Location
        {
            get { return m_Location; }
            set 
            {
                Log.MsgWrap<bool>(false, () =>
                {
                    m_Location = value;
                    if (null != m_Location)
                    {
                        txtName.Text = m_Location.Name;
                        txtDescription.Text = m_Location.Description;
                        chkHalfPours.Checked = m_Location.AllowHalfPour;
                        if (Guid.Empty == m_Location.LocationID)
                        {
                            txtDescription.Enabled = txtName.Enabled = btnDelete.Enabled = false;
                        }
                        else
                        {
                            txtDescription.Enabled = txtName.Enabled = btnDelete.Enabled = true;
                        }
                        if (m_Location.DeviceID.HasValue)
                        {
                            lueDevices.EditValue = m_Location.DeviceID.Value;
                        }
                        txtPOSAlias.Text = m_Location.POSAlias;
                    }
                    else
                    {
                        txtDescription.Text = txtName.Text = string.Empty;
                        txtDescription.Enabled = txtName.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                    return true;
                });
            }
        }


        public frmLocationAdd( )
        {
            InitializeComponent( );
        }

        private void frmLocationAdd_Load(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                treeInventory.FocusedNodeChanged -= new FocusedNodeChangedEventHandler(treeInventory_FocusedNodeChanged);
                InventoryUI.LoadInventoryTree(treeInventory);
                treeInventory.FocusedNodeChanged += new FocusedNodeChangedEventHandler(treeInventory_FocusedNodeChanged);

                lueDevices.SetupNamedObjectEdits("Name", "DeviceID");
                lueDevices.Properties.DataSource = BLManagerFactory.Get().ManageDevices().GetAll().ToList();

                m_Organization = treeInventory.Nodes[0].Tag as IBLOrganization;
                m_Node = treeInventory.Nodes[0];

                return true;
            });
        }

        void treeInventory_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                if (m_AddNew)
                {
                    if (System.Windows.Forms.DialogResult.Yes ==
                        MessageBox.Show(this, "Do you want to save your changes?", "Save Location", MessageBoxButtons.YesNo))
                    {
                        btnSave_Click(null, null);
                    }
                    m_AddNew = false;
                }
                if (null != e.Node.Tag)
                {
                    using (CursorManager.Create())
                    {
                        m_ParentLocation = Location = e.Node.Tag as IBLLocation;
                        m_Organization = e.Node.RootNode.Tag as IBLOrganization;
                        m_Node = e.Node;
                    }
                }

                return true;
            });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                Location = BLManagerFactory.Get().ManageLocations().Create();
                m_AddNew = true;

                return true;
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                Location = Location;
                m_AddNew = false;

                return true;
            });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                m_Location.Name = txtName.Text;
                m_Location.Description = txtDescription.Text;
                m_Location.OrganizationID = m_Organization.OrganizationID;
                m_Location.AllowHalfPour = chkHalfPours.Checked;
                if( string.IsNullOrWhiteSpace( txtPOSAlias.Text ))
                {
                    m_Location.POSAlias = txtPOSAlias.Text;
                }
                if (null == m_ParentLocation)
                {
                    m_Location.ParentID = (Guid?)null;
                }
                else if (m_ParentLocation.LocationID == m_Location.LocationID)
                {
                    m_Location.ParentID = (null != m_ParentLocation) ? m_ParentLocation.ParentID : (Guid?)null;
                }
                else
                {
                    m_Location.ParentID = (null != m_ParentLocation) ? m_ParentLocation.LocationID : (Guid?)null;
                }
                m_Location.DeviceID = (Guid?)(lueDevices.EditValue ?? Guid.Empty);

                if (m_AddNew)
                {
                    InventoryUI.AddTreeNode(m_Node, m_Location);
                }
                else
                {
                    m_Node.SetValue("Name", m_Location.Name);
                }
                BLManagerFactory.Get().ManageLocations().Save(m_Location);

                m_AddNew = false;

                frmPopup.ShowPopup( string.Format("Saved {0}", m_Location.Name));

                return true;
            });
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                if (System.Windows.Forms.DialogResult.Yes ==
                MessageBox.Show(this, string.Format("Are you sure you want to delete {0}?", m_Location.Name),
                    "Delete Location", MessageBoxButtons.YesNo))
                {
                    if (m_AddNew)
                    {
                        m_Location = null;
                    }
                    else
                    {
                        BLManagerFactory.Get().ManageLocations().Delete(m_Location);
                        treeInventory.Nodes.Remove(m_Node);
                        frmPopup.ShowPopup(string.Format("Saved {0}", m_Location.Name));
                    }
                }
                return true;
            });
        }
    }
}