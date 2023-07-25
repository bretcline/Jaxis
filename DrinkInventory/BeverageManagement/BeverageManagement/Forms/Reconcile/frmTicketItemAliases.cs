using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BeverageManagement.Properties;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using Jaxis.Inventory.Data.IBLDataItems;

namespace BeverageManagement.Forms.Reconcile
{
// ReSharper disable InconsistentNaming
    public partial class frmTicketItemAliases : DevExpress.XtraEditors.XtraForm, ITicketItemAliasesView
// ReSharper restore InconsistentNaming
    {
        private List<string> m_deletedAliases = new List<string>();

        public frmTicketItemAliases()
        {
            new TicketItemAliasesPresenter(this);
            InitializeComponent();
        }

        public IEnumerable<TicketItemAliasDisplay> Aliases
        {
            get
            {
                var list = (BindingList<ITicketItemAliasDisplay>)gcAliases.DataSource;
                return list.Cast<TicketItemAliasDisplay>();
            }
            set
            {
                var aliases = new BindingList<ITicketItemAliasDisplay>(value.Cast<ITicketItemAliasDisplay>().ToList());
                aliases.AddingNew += AddingNewAlias;
                gcAliases.DataSource = aliases;
                gvAliases.Columns["AssignedDrinkRecipe"].ColumnEdit = gcAliases.RepositoryItems["Recipe"];
            }
        }

        public IEnumerable<string> UnknownTicketItems
        {
            set
            {
                lbcUnassigned.Items.Clear();
                foreach (var item in value)
                {
                    lbcUnassigned.Items.Add(item);
                }
                lbcUnassigned.SortOrder = SortOrder.Ascending;
            }
        }

        public TicketItemAliasDisplay SelectedAlias
        {
            get
            {
                var row = gvAliases.GetFocusedRow();
                return row as TicketItemAliasDisplay;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string SelectedUnknownItem
        {
            get
            {
                return (string)lbcUnassigned.SelectedItem;
            }
            set
            {
                lbcUnassigned.SelectedItem = value;
            }
        }

        private static void AddingNewAlias(object _sender, AddingNewEventArgs _e)
        {
            _e.NewObject = new TicketItemAliasDisplay();
        }

        public event EventHandler OkClick;

        public IEnumerable<IBLRecipe> RecipeList
        {
            set
            {
                var editor = new RepositoryItemLookUpEdit
                {
                    ValueMember = "RecipeID",
                    DisplayMember = "Description",
                    NullText = string.Empty,
                    DataSource = value.ToList()
                };

                editor.NullText = Resources.frmTicketItemAliases_RecipeList_NullItem;
                var column = new LookUpColumnInfo("Description", "Recipe");
                editor.Columns.Add(column);
                editor.Name = "Recipe";
                gcAliases.RepositoryItems.Add(editor);
            }
        }

        public new void ShowDialog()
        {
            base.ShowDialog();
        }

        public void AddAlias(string _description, decimal _price)
        {
            gvAliases.AddNewRow();
            //Get the handle of the new row 
            var newRowHandle = gvAliases.FocusedRowHandle;
            var newRow = (TicketItemAliasDisplay)gvAliases.GetRow(newRowHandle);
            newRow.DescriptionOnTicket = _description;
            newRow.Price = _price;
        }

        public void DeleteSelectedAlias()
        {
            var focusedRow = gvAliases.FocusedRowHandle;
            if (focusedRow != GridControl.InvalidRowHandle)
            {
                m_deletedAliases.Add( gvAliases.GetFocusedValue() as string );
                gvAliases.DeleteRow(focusedRow);
            }
        }

        public void ShowError(string _message)
        {
            MessageBox.Show(this, _message, Resources.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SbOkClick(object _sender, EventArgs _e)
        {
            this.Fire(OkClick, _e);
        }

        public new event FormClosingEventHandler Closing;
        private void FrmTicketItemAliasesFormClosing(object _sender, FormClosingEventArgs _e)
        {
            this.Fire(Closing, _e);
        }

        public event EventHandler DeleteClick;
        private void SbDeleteAssignmentClick(object _sender, EventArgs _e)
        {
            this.Fire(DeleteClick, _e);
        }

        public event EventHandler AssignClick;
        private void SbAssignClick(object _sender, EventArgs _e)
        {
            this.Fire(AssignClick, _e);
        }
        private void LbcUnassignedDoubleClick(object _sender, EventArgs _e)
        {
            this.Fire(AssignClick, _e);
        }

        public event EventHandler AliasModified;
        private void GvAliasesRowUpdated(object _sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs _e)
        {
            this.Fire(AliasModified, _e);
        }

        private void btnAlias_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAliasUPC())
            {
                frm.ShowDialog();
            }
        }


        public IEnumerable<string> DeletedAliases
        {
            get { return m_deletedAliases; }
            set
            {
                m_deletedAliases.AddRange( value );
            }
        }
    }
}
