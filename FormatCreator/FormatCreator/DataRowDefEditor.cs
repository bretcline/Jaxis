using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LFI.RFID.Format;

namespace LFI.RFID.Editor
{
    // TODO
    // - HasChanges
    // - Allow Row reordering

    public partial class DataRowDefEditor : UserControl
    {        
        public DataRowDefEditor()
        {
            InitializeComponent();
            InitializeDataTypeCombo();         
        }

        private void InitializeDataTypeCombo()
        {
            repositoryItemComboBox1.Items.Add(DataType.Text);
            repositoryItemComboBox1.Items.Add(DataType.TextUnicode);
            
            repositoryItemComboBox1.Items.Add(DataType.DateOnly);
            repositoryItemComboBox1.Items.Add(DataType.TimeOnly);
            repositoryItemComboBox1.Items.Add(DataType.DateTime);

            repositoryItemComboBox1.Items.Add(DataType.PickList);
            repositoryItemComboBox1.Items.Add(DataType.PickListUnicode);
            repositoryItemComboBox1.Items.Add(DataType.PickListKeyValue);

            repositoryItemComboBox1.Items.Add(DataType.Bool);
        
            repositoryItemComboBox1.Items.Add(DataType.Double);
            repositoryItemComboBox1.Items.Add(DataType.Float);
            repositoryItemComboBox1.Items.Add(DataType.Int16);
            repositoryItemComboBox1.Items.Add(DataType.Int32);

            repositoryItemComboBox1.Items.Add(DataType.Guid);
        }

        public void SetDataRowDef(DataRowDef rowDef)
        {            
            this.rowDef = rowDef;            
            gridControl1.DataSource = rowDef.ElementDefs;
        }

        public bool HasChanges()
        {                            
            return rowDef.HasChanges();
        }
        private DataRowDef rowDef;        

        public void AcceptChanges()
        {            
            rowDef.AcceptChanges();
        }

        public void RevertChanges()
        {         
            rowDef.RevertChanges();
        }        

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                gridView1.DeleteRow( gridView1.FocusedRowHandle );
            }
        }

        public void Post()
        {
            gridView1.PostEditor();
        }
    }
}
