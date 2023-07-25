using System;
using System.Windows.Forms;
using LFI.Mobile.Controls;
using LFI.Mobile.Controls.Picklist;
using LFI.Mobile.Controls.Picklist.Grid;
using LFI.RFID.Format;
using System.Drawing;
using System.Collections.Generic;

namespace MobileInterrogator
{
    public partial class ValueEditScreen : BaseScreen
    {
        public ValueEditScreen(AppContext appContext)
            : base(ScreenTag.ValueEditor, true)
        {
            InitializeComponent();

            HeaderText = "Edit Value";

            this.appContext = appContext;
        }

        #region IScreen Members

        public override void Reactivate()
        {
            base.Reactivate();
            Activate();
        }

        public override bool Activate()
        {
            HeaderText = appContext.ValueEditState.DataElementDef.Name;
            InitEditor();

            return true;
        }

        public override void Deactivate()
        {
            // Hide the SIP when leaving if it is up
            inputPanel1.Enabled = false;
            RemoveEditor();
            base.Deactivate();
        }

        public override MenuItem BuildLeftMenu()
        {
            if (menuItemCancel == null)
            {
                menuItemCancel = new MenuItem { Enabled = true, Text = "Cancel" };
                menuItemCancel.Click += new EventHandler(menuItemCancel_Click);
            }

            return menuItemCancel;
        }

        public override MenuItem BuildRightMenu()
        {
            if (menuItemOK == null)
            {
                menuItemOK = new MenuItem { Enabled = true, Text = "OK" };
                menuItemOK.Click += menuItemOK_Click;
            }

            return menuItemOK;
        }

        #endregion

        #region Editor Control

        private void RemoveEditor()
        {
            if (valueEditor != null)
            {
                if (editorPanel.Controls.Contains(valueEditor.Control))
                    editorPanel.Controls.Remove(valueEditor.Control);
            }
        }

        private void InitEditor()
        {
            // Kill the old editor
            if (valueEditor != null)
                RemoveEditor();

            // Create the new one
            valueEditor = GetValueEditorNeeded();

            // Add it to the panel
            valueEditor.Control.Dock = DockStyle.Fill;
            editorPanel.Controls.Add(valueEditor.Control);

            valueEditor.DataElementDef = appContext.ValueEditState.DataElementDef;
            valueEditor.Value = appContext.ValueEditState.InitialValue;
            valueEditor.Activate();

            Update();
        }

        private IValueEditor GetValueEditorNeeded()
        {
            // Create the editor the first time it is requested and then hold on to it for reuse
            DataType dataType = appContext.ValueEditState.DataElementDef.DataType;
            if (!valueEditors.ContainsKey(dataType))
            {
                IValueEditor editor = ValueEditorFactory.CreateValueEditor(dataType);
                valueEditors.Add(dataType, editor);
            }

            return valueEditors[dataType];
        }

        #endregion

        #region Other Event Handlers

        private void menuItemCancel_Click(object sender, EventArgs e)
        {
            this.ScreenResult.Result = ScreenResults.Cancel;
            appContext.ValueEditState.FinalValue = null;

            ScreenMgr.GoBack();
        }

        private void menuItemOK_Click(object sender, EventArgs e)
        {
            bool isValid = valueEditor.Validate();
            if (isValid)
            {
                this.ScreenResult.Result = ScreenResults.OK;
                appContext.ValueEditState.FinalValue = valueEditor.Value;
                ScreenMgr.GoBack();
            }
        }
        
        #endregion

        #region Member Variables

        private AppContext appContext;
        private MenuItem menuItemCancel;
        private MenuItem menuItemOK;
        private IValueEditor valueEditor;
        private Dictionary<DataType, IValueEditor> valueEditors = new Dictionary<DataType, IValueEditor>();

        #endregion
    }
}