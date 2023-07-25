using System;
using System.Windows.Forms;
using LFI.Mobile.Controls;
using LFI.Mobile.Controls.Picklist;
using LFI.Mobile.Controls.Picklist.Grid;
using LFI.RFID.Format;
using System.Drawing;

namespace MobileInterrogator
{
    public partial class HeaderScreen : BaseScreen
    {
        public HeaderScreen(AppContext appContext)
            : base(ScreenTag.HeaderRow)
        {
            InitializeComponent();

            HeaderText = "Header";

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
            RebuildPickList();
            return true;
        }

        public override void Deactivate()
        {
            DisposePicklist();
            base.Deactivate();
        }

        public override void HandleNavigationResults(ScreenResult result)
        {
            // If coming from the editor and committed refresh the picklist
            if ((result.ScreenTag == ScreenTag.ValueEditor) &&
                (result.Result == ScreenResults.OK))
            {
                // Commit the change back to the data row
                string dataName = appContext.ValueEditState.DataElementDef.Name;                
                appContext.CurrentTagData.HeaderRow.Values[dataName] = appContext.ValueEditState.FinalValue;
            }
            else
            {
                base.HandleNavigationResults(result);
            }
        }

        public override MenuItem BuildLeftMenu()
        {
            if (menuItemDone == null)
            {
                menuItemDone = new MenuItem { Enabled = true, Text = "Done" };
                menuItemDone.Click += new EventHandler(menuItemDone_Click);
            }

            return menuItemDone;
        }

        public override MenuItem BuildRightMenu()
        {
            // TODO: Verify all required fields are filled out
            if (menuItemDetails == null)
            {
                menuItemDetails = new MenuItem { Enabled = true, Text = "Details" };
                menuItemDetails.Click += delegate { ScreenMgr.NavigateTo(ScreenTag.DataRow); };
            }

            return menuItemDetails;
        }

        #endregion

        #region Pick List

        private void DisposePicklist()
        {
            if (picklist != null)
            {
                if (pickListPanel.Controls.Contains(picklist))
                    pickListPanel.Controls.Remove(picklist);

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
            pickListPanel.Controls.Add(picklist);
            picklist.DoubleClick += delegate { EditSelectedValue(); };

            btnEdit.Enabled = ((appContext.CurrentTagData != null) && (!appContext.CurrentTagData.HeaderRow.IsLocked));

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
            foreach (DataElementDef elemDef in appContext.CurrentTagFormat.HeaderRowDef.ElementDefs)
            {
                string value = string.Empty;
                if (appContext.CurrentTagData.HeaderRow.Values.ContainsKey(elemDef.Name))
                    value = appContext.CurrentTagData.HeaderRow.Values[elemDef.Name];

                picklist.AddItem(PicklistItemFactory.CreateOneColumnTwoRows(properties, elemDef.Name, elemDef.Name, value));
            }        
        }

        private void OnPickListSelectedIndexChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = (picklist.SelectedIndex != -1);
        }

        #endregion

        #region Other Event Handlers

        private void OnEditValueClick(Object sender, EventArgs e)
        {
            EditSelectedValue();
        }

        private void EditSelectedValue()
        {
            if ((appContext.CurrentTagData == null) || (appContext.CurrentTagData.HeaderRow.IsLocked))
                return;

            int index = picklist.SelectedIndex;
            DataElementDef elemDef = appContext.CurrentTagFormat.HeaderRowDef.ElementDefs[index];
            string currentValue = string.Empty;
            if (appContext.CurrentTagData.HeaderRow.Values.ContainsKey(elemDef.Name))
                currentValue = appContext.CurrentTagData.HeaderRow.Values[elemDef.Name];
            
            appContext.ValueEditState.Init(elemDef, currentValue);
            ScreenMgr.NavigateTo(ScreenTag.ValueEditor);
        }

        private void menuItemDone_Click(object sender, EventArgs e)
        {
            appContext.InitiateTagWrite( );
            ScreenMgr.NavigateTo(ScreenTag.Home);
        }

        #endregion

        #region Member Variables

        private AppContext appContext;
        private MenuItem menuItemDone;
        private MenuItem menuItemDetails;
        private Picklist picklist;
        private GridPicklistProperties properties;

        #endregion
    }
}