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
using System.IO;

namespace LFI.RFID.Editor
{
    public partial class EditorForm : Form
    {
        private IList<FormatDef> formatList = new List<FormatDef>();
        private bool canceled = false;
        private string formatFolderPath = string.Empty;

        public EditorForm()
        {
            InitializeComponent();
            InitializeRibbon();

            string exeLocation = System.Reflection.Assembly.GetExecutingAssembly( ).Location;
            string exeFolder = System.IO.Path.GetDirectoryName( exeLocation );
            formatFolderPath = System.IO.Path.Combine(exeFolder, "Formats");

            formatManager = new FormatManager(formatFolderPath);
            LoadFormatList();

            InitializeTagReader(formatFolderPath);
        }

        private void InitializeTagReader(string formatFolderPath)
        {
            tagReader = RFIDReaderManager.GetReader(RFIDReaderTypes.MockReader, formatFolderPath);
        }

        private void InitializeRibbon()
        {
            buttonNew.LargeGlyph = new Icon(Icons.New, new Size(32, 32)).ToBitmap();
            buttonCopy.LargeGlyph = new Icon(Icons.Copy, new Size(32, 32)).ToBitmap();
            buttonDelete.LargeGlyph = new Icon(Icons.Delete, new Size(32, 32)).ToBitmap();
            buttonSave.LargeGlyph = new Icon(Icons.Save, new Size(32, 32)).ToBitmap();
            buttonRefresh.LargeGlyph = new Icon(Icons.Refresh, new Size(32, 32)).ToBitmap();
            buttonTagCreate.LargeGlyph = new Icon(Icons.Tag_Write, new Size(32, 32)).ToBitmap();
            buttonTagRead.LargeGlyph = new Icon(Icons.Tag_Read, new Size(32, 32)).ToBitmap();             
        }

        private void LoadFormatList()
        {
            listFormats.Items.Clear();
            listFormats.DisplayMember = "Name";
            listFormats.ValueMember = "ID";           
            IEnumerable<FormatDef> formats = formatManager.GetAvailableFormats();
            foreach (FormatDef formatDef in formats)
            {
                formatDef.AcceptChanges();                
            }
            formatList = new List<FormatDef>(formats);
            listFormats.DataSource = formatList; 
            
            if (formatList.Count > 0)
            {
                listFormats.SelectedIndex = 0;
                groupControl1.Visible = true;
            }
            else
                groupControl1.Visible = false;
        }

        private void listFormats_SelectedValueChanged(object sender, EventArgs e)
        {
            if (canceled)
            {
                canceled = false;
                return;
            }
            if (HasChanges())
            {
                DialogResult result = MessageBox.Show(this, "Save current changes?", "Save", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    UpdateData();
                    formatManager.SaveFormat(formatDef);
                    dataRowDefEditorHeader.AcceptChanges();
                    dataRowDefEditorData.AcceptChanges();
                    InitializeTagReader(formatFolderPath);
                }
                else if (result == DialogResult.Cancel)
                {
                    canceled = true;
                    listFormats.SelectedItem = formatDef;
                    return;
                }
                else
                {
                    dataRowDefEditorHeader.RevertChanges();
                    dataRowDefEditorData.RevertChanges();
                }
            }
           
            groupControl1.Visible = true;
            formatDef = listFormats.SelectedItem as FormatDef;
            UpdateUI();
        }

        private bool HasChanges()
        {
            if (formatDef == null)
                return false;
            else if ((string.Compare(formatDef.Name, textEditName.Text) != 0) ||
                (string.Compare(formatDef.Description, memoEditDescription.Text) != 0) ||
                (formatDef.MaxDataRows != (int)spinEditHistory.Value) ||
                dataRowDefEditorHeader.HasChanges() ||
                dataRowDefEditorData.HasChanges())
                return true;
            else 
                return false;
        }

        private void UpdateUI()
        {
            if (formatDef == null)
                return;

            textEditName.Text = formatDef.Name;
            memoEditDescription.Text = formatDef.Description;
            spinEditHistory.Value = formatDef.MaxDataRows;
            dataRowDefEditorHeader.SetDataRowDef(formatDef.HeaderRowDef);
            dataRowDefEditorData.SetDataRowDef(formatDef.DataRowDef);
        }

        private void UpdateData()
        {
            if (formatDef == null)
                return;

            dataRowDefEditorData.Post();
            dataRowDefEditorHeader.Post();

            formatDef.Name = textEditName.Text;
            formatDef.Description = memoEditDescription.Text;
            formatDef.MaxDataRows = (int)spinEditHistory.Value;
        }

        private void spinEditHistory_EditValueChanged(object sender, EventArgs e)
        {
            // Max the value an integer between 0 and 100
            int value = (int)spinEditHistory.Value;
            value = Math.Max(value, 0);
            value = Math.Min(value, 100);
            spinEditHistory.Value = value;
        }

        #region Ribbon Events

        private void buttonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateData();
            if (formatDef != null)
            {
                formatManager.SaveFormat(formatDef);
                dataRowDefEditorHeader.AcceptChanges();
                dataRowDefEditorData.AcceptChanges();
                InitializeTagReader(formatFolderPath);
            }
        }

        private void buttonRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            formatManager.Refresh();
            LoadFormatList();
        }

        private void buttonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formatDef != null)
            {
                formatManager.DeleteFormat(formatDef.ID);
                formatList.Remove(formatDef);                
                if (formatList.Count > 0)
                    listFormats.SelectedIndex = 0;
                else
                {
                    groupControl1.Visible = false;
                }
            }
        }

        private void buttonNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormatDef format = new FormatDef();
            formatList.Add(format);
            listFormats.SelectedItem = format;
        }
        
        private void buttonCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formatDef != null)
            {
                FormatDef format = new FormatDef(formatDef);
                format.AcceptChanges();               
                formatList.Add(format);
                listFormats.SelectedItem = format;
            }
        }

        private void buttonTagCreate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (formatDef == null)
                return;

            TagData tagData = tagReader.ReadTag();

            TagDataForm dataForm = new TagDataForm(formatManager, formatDef, tagData, tagReader);
            dataForm.ShowDialog(this);
        }

        private void buttonTagRead_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //TagData debug_tagData = _DEBUG_CreateTagData();
            TagData tagData = tagReader.ReadTag();            
            FormatDef debug_formatDef = formatManager.GetFormatByID(tagData.FormatID);

            TagDataForm dataForm = new TagDataForm(formatManager, debug_formatDef, tagData, tagReader);
            dataForm.ShowDialog(this);
        }

        private TagData _DEBUG_CreateTagData()
        {
            TagData data = new TagData();
            data.FormatID = new Guid("00000000-0000-0000-0000-000000000002");
            data.HeaderRow = new TagDataRow();
            data.HeaderRow.Values.Add("Lease", "South Cowden");
            data.HeaderRow.Values.Add("Well Name", "Well-A14");
            data.HeaderRow.Values.Add("Operator", "OXY");

            TagDataRow dataRow1 = new TagDataRow();
            dataRow1.Values.Add("Tech", "Bob");
            dataRow1.Values.Add("Treatment Date", "10/1/2009 14:00");
            dataRow1.Values.Add("Chemical", "Foamer");
            dataRow1.Values.Add("Unit of Measure", "Gallon");
            dataRow1.Values.Add("As Found", "20.5");
            dataRow1.Values.Add("As Left", "46.0");
            data.DataRows.Add(dataRow1);

            TagDataRow dataRow2 = new TagDataRow();
            dataRow2.Values.Add("Tech", "Fred");
            dataRow2.Values.Add("Treatment Date", "11/1/2009 14:00");
            dataRow2.Values.Add("Chemical", "Corrosion Inhibitor");
            dataRow2.Values.Add("Unit of Measure", "Gallon");
            dataRow2.Values.Add("As Found", "10.5");
            dataRow2.Values.Add("As Left", "86.0");
            data.DataRows.Add(dataRow2);

            return data;
        }
        #endregion

        #region Member Variables

        private FormatDef formatDef = null;
        private FormatManager formatManager = null;
        private IRFIDReader tagReader = null;

        #endregion        
    }
}
