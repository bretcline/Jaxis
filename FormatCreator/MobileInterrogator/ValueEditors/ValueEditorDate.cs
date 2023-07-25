using System;
using System.Windows.Forms;
using LFI.RFID.Format;

namespace MobileInterrogator
{
    public partial class ValueEditorDate : UserControl, IValueEditor
    {
        public ValueEditorDate()
        {
            InitializeComponent();
        }

        #region IValueEditor Members

        public DataElementDef DataElementDef
        {
            get { return dataElementDef; }
            set { dataElementDef = value; }
        }
        private DataElementDef dataElementDef;

        public string Value
        {
            get 
            {
                DateTime currentValue = new DateTime(
                    dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day,
                    dateTimePicker2.Value.Hour, dateTimePicker2.Value.Minute, dateTimePicker1.Value.Second);

                return currentValue.ToString();
            }
            set 
            {
                DateTime currentValue;
                if (string.IsNullOrEmpty(value))
                    currentValue = DateTime.Now;
                else
                {
                    try
                    {
                        currentValue = DateTime.Parse(value);
                    }
                    catch
                    {
                        currentValue = DateTime.Now;
                    }
                }

                dateTimePicker1.Value = currentValue;
                dateTimePicker2.Value = currentValue;
            }
        }

        public bool Validate()
        {
            return true;
        }

        public Control Control
        {
            get { return this; }
        }

        public void Activate()
        {
            this.dateTimePicker1.Focus();
            this.Parent.Text = dataElementDef.Name;
        }

        #endregion
    }
}