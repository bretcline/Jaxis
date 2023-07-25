using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using EngineConfigUtil;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace BeverageManagement.Forms.Administration
{
    public partial class frmAdmin : DevExpress.XtraEditors.XtraForm
    {
        private IBLStandardNozzle m_LiquorNozzle = null;
        private IBLStandardNozzle m_WineNozzle = null;
        LayoutControl lcStandardPours = null;
        LayoutControlItem previousItem = null;
        private LayoutControlGroup group = null;
        private static Size MinCtrlSize = new Size(215, 69);
        private static Size MinGroupSize = new Size(220, 74);

        private List<StandardPourItem> m_Pours = new List<StandardPourItem>();

        public frmAdmin( )
        {
            InitializeComponent( );
            lcStandardPours = new LayoutControl();
            lcStandardPours.Dock = DockStyle.Fill;
            pnlStandardPours.Controls.Add( lcStandardPours);

            lcStandardPours.Root.GroupBordersVisible = false;
            lcStandardPours.BestFit();
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                CommonUI.LoadNozzleTypes(cmbLiquorNozzleShape);
                CommonUI.LoadNozzleTypes(cmbWineNozzleShape);

                (new List<LookUpEdit> { lueSizeType }).SetupNamedObjectEdits("Name", "SizeTypeID");

                var data = BLManagerFactory.Get().ManageSizeTypes().GetAll().ToList();
                lueSizeType.Properties.DataSource = data;

                lueSizeType.EditValue = new Guid( BLManagerFactory.Get().GetAdminValue("Pour Conversion") );

                txtUserName.Text =
                    BLManagerFactory.Get().ManageUsers().Get(BLManagerFactory.Get().UserSession.UserID).ProperName;

                m_LiquorNozzle = BLManagerFactory.Get().GetDefaultNozzle();
                cmbLiquorNozzleShape.SelectedItem = (NozzleShapes)m_LiquorNozzle.Shape;
                txtLiquorWidth.Text = m_LiquorNozzle.Width.ToString();
                txtLiquorLength.Text = m_LiquorNozzle.Length.ToString();

                m_WineNozzle = BLManagerFactory.Get().GetDefaultNozzle(DefaultNozzleType.Wine);
                cmbWineNozzleShape.SelectedItem = (NozzleShapes)m_WineNozzle.Shape;
                txtWineWidth.Text = m_WineNozzle.Width.ToString();
                txtWineLength.Text = m_WineNozzle.Length.ToString();

                LoadPours();

                return true;
            });
        }

        private void LoadPours()
        {
            var standard = BLManagerFactory.Get().ManageStandardPours().GetAll().Where( p => p.SystemStandard == true);
            lcStandardPours.BeginUpdate();
            lcStandardPours.Clear();
            previousItem = null;
            group = new LayoutControlGroup();
            group.Name = "StandardPourInfo";
            group.Text = "Standard Pour";
            group.TextVisible = false;

            lcStandardPours.Root.Add(group);
            try
            {
                foreach (var blStandardPour in standard)
                {
                    var pourItem = new StandardPourItem( blStandardPour );
                    AddItem(pourItem);
                }
            }
            finally
            {
                lcStandardPours.EndUpdate();
            }
        }


        private void btnAddPour_Click(object sender, EventArgs e)
        {
            lcStandardPours.BeginUpdate();
            try
            {
                AddItem();
            }
            finally
            {
                lcStandardPours.EndUpdate();
            }
        }

        private void AddItem( StandardPourItem _item = null)
        {
            LayoutControlItem item = null;
            if( null == _item )
            {
                _item = new StandardPourItem();
            }
            m_Pours.Add(_item);

            _item.Size = MinCtrlSize;
            if (null == previousItem)
            {
                previousItem = group.AddItem();
                previousItem.TextVisible = false;
            }
            item = new LayoutControlItem(lcStandardPours, _item);
            item.Move(previousItem, InsertType.Top);
            item.SizeConstraintsType = SizeConstraintsType.Custom;
            item.MinSize = MinGroupSize;
            item.ControlMinSize = MinCtrlSize;
            item.TextVisible = false;
            previousItem = item;
        }

        private void btnFiles_Click( object sender, EventArgs e )
        {
            Log.MsgWrap(false, () =>
            {
                if( DialogResult.OK == ofdUPCImport.ShowDialog( ) )
                {
                    txtUPCPath.Text = ofdUPCImport.FileName;
                }
                return true;
            });

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                using( frmUsers frm = new frmUsers())
                {
                    frm.ShowDialog( this );
                }
                return true;
            });

        }

        private void btnSetUnits_Click(object sender, EventArgs e)
        {
            BLManagerFactory.Get().SetAdminValue("Pour Conversion", (lueSizeType.EditValue).ToString() );
        }

        private void btnEngineConfig_Click(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                Process.Start("EngineConfigUtil.exe");
                return true;
            });

        }

        private void btnSaveSpout_Click(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                m_LiquorNozzle.Shape = (int)cmbLiquorNozzleShape.SelectedItem;
                m_LiquorNozzle.Length = Convert.ToDouble(txtLiquorLength.Text);
                m_LiquorNozzle.Width = Convert.ToDouble(txtLiquorWidth.Text);

                BLManagerFactory.Get().ManageStandardNozzles().Save(m_LiquorNozzle);

                m_WineNozzle.Shape = (int)cmbWineNozzleShape.SelectedItem;
                m_WineNozzle.Length = Convert.ToDouble(txtWineLength.Text);
                m_WineNozzle.Width = Convert.ToDouble(txtWineWidth.Text);

                BLManagerFactory.Get().ManageStandardNozzles().Save(m_WineNozzle);

                return true;
            });

        }

        private void btnSetStandardPours_Click(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                foreach (var blStandardPour in m_Pours)
                {
                    var item = blStandardPour.PourItem;
                    item.SystemStandard = true;
                    BLManagerFactory.Get().ManageStandardPours().Save(item);
                }
                return true;
            });

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            BLManagerFactory.Get().ManageUPCs().ImportUPCList(txtUPCPath.Text);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPours();
        }

        private void btnUPCManagement_Click(object sender, EventArgs e)
        {
            using( var frmUPC = new frmUPCManagement( ) )
            {
                frmUPC.ShowDialog();
            }
        }
    }
}