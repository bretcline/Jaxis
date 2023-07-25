using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LFI.RFID.Format;
using Jaxis.RFID.Readers;

// TODO: Handle FormatDef.MaxDataRows

namespace LFI.RFID.Editor
{
    public partial class TagDataForm : Form
    {
        public TagDataForm(FormatManager _formatManager, FormatDef _formatDef, TagData _tagData, IRFIDReader tagReader)
        {
            InitializeComponent();

            this.tagReader = tagReader;
            this.formatManager = _formatManager;
            this.formatDef = _formatDef;
            this.tagData = _tagData;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadFormatDefCombo();

            if (formatDef != null)
                cmbFormatDefs.SelectedValue = formatDef.ID;
            else
                cmbFormatDefs.SelectedIndex = 0;

            if (tagData == null)
            {
                tagData = new TagData();
                tagData.FormatID = (Guid)cmbFormatDefs.SelectedValue;
            }

            LoadToScreen();

            initialized = true;
        }

        private void LoadFormatDefCombo()
        {
            cmbFormatDefs.DisplayMember = "Name";
            cmbFormatDefs.ValueMember = "ID";
            IEnumerable<FormatDef> formats = formatManager.GetAvailableFormats();
            List<FormatDef> formatList = new List<FormatDef>(formats);
            cmbFormatDefs.DataSource = formatList;
        }

        private void LoadToScreen()
        {
            try
            {
                dataSet = TagDataHelper.CreateDataSet(formatDef, tagData);
                Dictionary<string, Dictionary<string, string>> headerPickLists = TagDataHelper.CreatePickLists(formatDef.HeaderRowDef);
                Dictionary<string, Dictionary<string, string>> dataRowPickLists = TagDataHelper.CreatePickLists(formatDef.DataRowDef);

                InitializeGrid(gridHeader, dataSet.Tables[TagDataHelper.HeaderTableName], formatDef.HeaderRowDef, headerPickLists);
                InitializeGrid(gridDataRows, dataSet.Tables[TagDataHelper.DataRowTableName], formatDef.DataRowDef, dataRowPickLists);
                buttonDelete.Enabled = gridViewDataRows.RowCount > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void InitializeGrid(DevExpress.XtraGrid.GridControl _grid, DataTable _table, DataRowDef _dataRowDef, Dictionary<string, Dictionary<string, string>> _pickLists)
        {
            _grid.DataSource = _table;
            _grid.RepositoryItems.Clear();
            _grid.MainView.PopulateColumns();
            DevExpress.XtraGrid.Views.Grid.GridView view = _grid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;

            foreach (KeyValuePair<string, Dictionary<string, string>> kvp in _pickLists)
            {
                string columnName = kvp.Key;
                Dictionary<string, string> pickList = kvp.Value;

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit editor = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                editor.AutoHeight = false;
                editor.NullText = string.Empty;
                editor.Name = columnName + "_picklist";
                editor.ValueMember = "Key";
                editor.DisplayMember = "Value";
                editor.DataSource = pickList.ToList<KeyValuePair<string, string>>();

                _grid.RepositoryItems.Add(editor);

                DevExpress.XtraGrid.Columns.GridColumn column = view.Columns[columnName];

                column.ColumnEdit = editor;
            }

            foreach (DataElementDef elementDef in _dataRowDef.ElementDefs)
            {
                DevExpress.XtraGrid.Columns.GridColumn column = view.Columns[elementDef.Name];
                DevExpress.XtraEditors.Repository.RepositoryItem editor = null;
                if (elementDef.DataType == DataType.TimeOnly)
                {
                    editor = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
                }
                else if (elementDef.DataType == DataType.DateOnly || elementDef.DataType == DataType.DateTime)
                {
                    editor = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
                    if (elementDef.DataType == DataType.DateTime)
                        (editor as DevExpress.XtraEditors.Repository.RepositoryItemDateEdit).VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
                }
                if (editor != null)
                {
                    column.ColumnEdit = editor;
                    _grid.RepositoryItems.Add(editor);
                }
            }
        }

        private bool ReadFromScreen()
        {
            tagData = TagDataHelper.ExtractFromDataSet(dataSet, formatDef);
            return (tagData != null);
        }             

        #region Form Events

        private void btnWrite_Click(object sender, EventArgs e)
        {            
            bool success = ReadFromScreen();
            if (success)
            {
                tagReader.WriteTag(tagData);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            // TODO: Read the tag data and update the formatDef and tagData objects 
            bool success = true;

            if (success)
            {
                cmbFormatDefs.SelectedValue = formatDef.ID;
                LoadToScreen();
                tagData = tagReader.ReadTag();
            }
        }

        private void cmbFormatDefs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!initialized) return;

            // If the format changes create a new TagData object with that format
            tagData = new TagData();
            tagData.FormatID = (Guid)cmbFormatDefs.SelectedValue;
            formatDef = formatManager.GetFormatByID(tagData.FormatID);            

            LoadToScreen();
        }

        #endregion

        #region Member Variables

        private IRFIDReader tagReader;
        private FormatManager formatManager;
        private FormatDef formatDef;
        private TagData tagData;
        private DataSet dataSet;
        private bool initialized = false;

        #endregion

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //gridViewDataRows.AddNewRow();
            TagDataRow tagRow = new TagDataRow();            
            TagDataHelper.AddDataRow(dataSet.Tables[TagDataHelper.DataRowTableName], formatDef.DataRowDef, tagRow, true);
            if (gridViewDataRows.RowCount > formatDef.MaxDataRows)
                gridViewDataRows.DeleteRow(gridViewDataRows.RowCount - 1);
            buttonDelete.Enabled = gridViewDataRows.RowCount > 0;
            gridViewDataRows.FocusedRowHandle = 0;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            gridViewDataRows.DeleteRow(gridViewDataRows.FocusedRowHandle);
            buttonDelete.Enabled = gridViewDataRows.RowCount > 0;
        }
    }
}
