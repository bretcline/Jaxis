using System.Windows.Forms;
using LFI.RFID.Format;

namespace MobileInterrogator
{
    public partial class ValueEditorText : UserControl, IValueEditor
    {
        public ValueEditorText()
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
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public bool Validate()
        {
            if (dataElementDef.Required && string.IsNullOrEmpty(textBox1.Text))
            {
                string msg = "Please enter a value.";
                MessageBox.Show(msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                return false;
            }
            else if (!string.IsNullOrEmpty(dataElementDef.Constraints))
            {
                try
                {
                    int maxLen = int.Parse(dataElementDef.Constraints);
                    int textLen = textBox1.Text.Length;
                    if (textLen > maxLen)
                    {
                        string msg = string.Format("The value must be less than {0} characters.", maxLen);
                        MessageBox.Show(msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                        return false;
                    }
                }
                catch { }
            }

            return true;
        }

        public Control Control
        {
            get { return this; }
        }

        public void Activate()
        {
            this.textBox1.Focus();
            this.Parent.Text = dataElementDef.Name;
        }

        #endregion
    }
}