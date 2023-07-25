using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using LFI.RFID.Format;

namespace MobileInterrogator
{
    public partial class ValueEditorNumber : UserControl, IValueEditor
    {
        [DllImport("coredll.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public ValueEditorNumber()
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
                return FormatReturnValue();
            }
            set { textBox1.Text = value; }
        }

        public bool Validate()
        {
            double value; 
            string valueAsText = textBox1.Text.Trim();
            
            if (!ValidateRequired(valueAsText) ||
                !ValidateNumeric(valueAsText, out value) ||
                !ValidateRange(value))
                return false;
            else
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
            UpdateKeyPad();
        }

        #endregion

        #region Numeric Key Pad

        private void UpdateKeyPad()
        {
            bool isInteger = ((dataElementDef.DataType == DataType.Int16) || (dataElementDef.DataType == DataType.Int32));
            btnDecimal.Visible = (isInteger == false);
        }

        private void AddToTextBox(byte key)
        {
            textBox1.Focus();

            keybd_event(key, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
        }

        private void OnClickNumber0(Object sender, EventArgs e) { AddToTextBox(KEY_0); }
        private void OnClickNumber1(Object sender, EventArgs e) { AddToTextBox(KEY_1); }
        private void OnClickNumber2(Object sender, EventArgs e) { AddToTextBox(KEY_2); }
        private void OnClickNumber3(Object sender, EventArgs e) { AddToTextBox(KEY_3); }
        private void OnClickNumber4(Object sender, EventArgs e) { AddToTextBox(KEY_4); }
        private void OnClickNumber5(Object sender, EventArgs e) { AddToTextBox(KEY_5); }
        private void OnClickNumber6(Object sender, EventArgs e) { AddToTextBox(KEY_6); }
        private void OnClickNumber7(Object sender, EventArgs e) { AddToTextBox(KEY_7); }
        private void OnClickNumber8(Object sender, EventArgs e) { AddToTextBox(KEY_8); }
        private void OnClickNumber9(Object sender, EventArgs e) { AddToTextBox(KEY_9); }
        private void OnClickDecimal(Object sender, EventArgs e) { AddToTextBox(KEY_DECIMAL); }
        private void OnClickBackspace(Object sender, EventArgs e) { AddToTextBox(KEY_BACKSPACE); }
        private void OnClickClear(Object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox1.Focus();
        }
        private void OnClickSign(Object sender, EventArgs e)
        {
            // Toggle the - at the beginning of the string
            string text = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(text))
                textBox1.Text = "-";
            else if (text[0] == '-')
                textBox1.Text = text.Remove(0, 1);
            else
                textBox1.Text = "-" + text;

            textBox1.Focus();
            if (textBox1.Text.Length > 0)
                textBox1.Select(textBox1.Text.Length - 1, 0);
        }

        #endregion

        #region Validations

        private bool ValidateRequired(string valueAsText)
        {
            if (!dataElementDef.Required || !string.IsNullOrEmpty(valueAsText))
                return true;
            else
            {
                string msg = "Please enter a value.";
                MessageBox.Show(msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        private bool ValidateNumeric(string valueAsText, out double value)
        {
            value = double.NaN;

            try
            {
                // Parse will throw an exception if it isn't a valid number
                value = double.Parse(valueAsText);
                return true;
            }
            catch
            {
                string msg = "Please enter a valid number.";
                MessageBox.Show(msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        private bool ValidateRange(double value)
        {
            if (dataElementDef.DataType == DataType.Double)
                return true;
            else if (dataElementDef.DataType == DataType.Float)
            {
                if ((value > float.MaxValue) || (value < float.MinValue))
                    return ShowRangeError();
            }
            else if (dataElementDef.DataType == DataType.Int16)
            {
                if ((value > short.MaxValue) || (value < short.MinValue))
                    return ShowRangeError();
            }
            else if (dataElementDef.DataType == DataType.Int32)
            {
                if ((value > int.MaxValue) || (value < int.MinValue))
                    return ShowRangeError();
            }
            
            return true;
        }

        private bool ShowRangeError()
        {
            string msg = "Value is out of range.";
            MessageBox.Show(msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            return false;
        }

        private string FormatReturnValue()
        {
            // By this point the value as already been validated for being required, type, and range
            // So the casting and assumptions here should be valid

            string valueAsText = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(valueAsText))
                return string.Empty;
            
            // If it is a float or double just return it
            if ((dataElementDef.DataType == DataType.Double) ||
                (dataElementDef.DataType == DataType.Float))
                return valueAsText;
            else
            {
                double valueAsDouble = double.Parse(valueAsText);
                int valueAsInt = (int)valueAsDouble;
                return valueAsInt.ToString();
            }
        }

        #endregion

        #region Member Variables

        private byte KEY_0 = 0x60;
        private byte KEY_1 = 0x61;
        private byte KEY_2 = 0x62;
        private byte KEY_3 = 0x63;
        private byte KEY_4 = 0x64;
        private byte KEY_5 = 0x65;
        private byte KEY_6 = 0x66;
        private byte KEY_7 = 0x67;
        private byte KEY_8 = 0x68;
        private byte KEY_9 = 0x69;
        private byte KEY_DECIMAL = 0x6E;
        private byte KEY_BACKSPACE = 0x08;
        private int KEYEVENTF_KEYUP = 0x2;
        private int KEYEVENTF_KEYDOWN = 0x0;

        #endregion
    }
}