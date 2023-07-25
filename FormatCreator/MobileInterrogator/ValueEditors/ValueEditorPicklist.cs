using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using LFI.RFID.Format;
using LFI.Mobile.Controls.Picklist;
using LFI.Mobile.Controls.Picklist.Grid;
using LFI.Mobile.Controls;


namespace MobileInterrogator
{
    public partial class ValueEditorPicklist : UserControl, IValueEditor
    {
        public ValueEditorPicklist()
        {
            InitializeComponent();
        }

        #region IValueEditor Members

        public DataElementDef DataElementDef
        {
            get { return dataElementDef; }
            set 
            { 
                dataElementDef = value;
                RebuildAvailableValues();
            }
        }
        private DataElementDef dataElementDef;

        public string Value
        {
            get { return currentValue; }
            set { currentValue = value; }
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
            RebuildPickList();
        }

        #endregion

        #region Picklist

        private void DisposePicklist()
        {
            if (picklist != null)
            {
                if (Controls.Contains(picklist))
                    Controls.Remove(picklist);

                picklist.SelectedIndexChanged -= OnPickListSelectedIndexChanged;
                picklist.Dispose();
            }
        }

        private void RebuildPickList()
        {
            // Kill the old pick list
            if (picklist != null)
                DisposePicklist();

            // Create the new one
            CreatePickList();
            LoadPickList();

            // Add it to the panel
            picklist.Dock = DockStyle.Fill;
            Controls.Add(picklist);

            // TODO: make double click perform OK on parent
            //picklist.DoubleClick += delegate { OnOK(); };
            picklist.SelectedIndexChanged += OnPickListSelectedIndexChanged;

            Update();
        }

        private void CreatePickList()
        {
            properties = new GridPicklistProperties();
            GridColumnProperties colProps = new GridColumnProperties { PercentWidth = 100 };
            properties.SetColumnPropertiesForColumnNumber(0, colProps);
            properties.AlternateBackgroundGradientDirection = Direction.Vertical;
            properties.AlternateBackgroundGradientEndColor = Color.Wheat;
            properties.AlternateBackgroundGradientStartColor = Color.FromArgb(220, 197, 154);
            properties.BackgroundGradientDirection = Direction.Vertical;
            properties.BackgroundGradientEndColor = Color.White;
            properties.BackgroundGradientStartColor = Color.FromArgb(230, 230, 230);

            picklist = new Picklist(properties);
        }

        private void LoadPickList()
        {
            foreach (KeyValuePair<string, string> kvp in availableValues)
                picklist.AddItem(PicklistItemFactory.CreateOneColumnTwoRows(properties, kvp.Key, kvp.Value, string.Empty));

            // Try to select the previous value or default to the first item
            int index = picklist.FindNearestMatch(currentValue);
            if (index == -1) index = 0;
            picklist.SelectedIndex = index;            
        }

        private void OnPickListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (picklist.SelectedIndex != -1)
                currentValue = availableValues[picklist.SelectedIndex].Key;
        }

        private void RebuildAvailableValues()
        {
            availableValues = new List<KeyValuePair<string, string>>();

            if (dataElementDef.DataType == DataType.PickListKeyValue)
            {
                string[] values = dataElementDef.Constraints.Split('|');
                for (int i = 0; i < values.Length; i += 2)
                    availableValues.Add(new KeyValuePair<string, string>(values[i], values[i + 1]));
            }
            else if (dataElementDef.DataType == DataType.Bool)
            {
                // If nothing was provided just show true false
                if (string.IsNullOrEmpty(dataElementDef.Constraints))
                {
                    availableValues.Add(new KeyValuePair<string, string>("true", "True"));
                    availableValues.Add(new KeyValuePair<string, string>("false", "False"));
                }
                // Otherwise parse out the values and use the first for true and the second for false
                else
                {
                    string[] values = dataElementDef.Constraints.Split('|');
                    availableValues.Add(new KeyValuePair<string, string>("true", values[0]));
                    availableValues.Add(new KeyValuePair<string, string>("false", values[1]));
                }               
            }
            else
            {
                string[] values = dataElementDef.Constraints.Split('|');
                foreach (string value in values)
                    availableValues.Add(new KeyValuePair<string, string>(value, value));
            }            
        }

        #endregion

        #region Member Variables

        private Picklist picklist;
        private GridPicklistProperties properties;
        private List<KeyValuePair<string, string>> availableValues;
        private string currentValue;

        #endregion
    }
}