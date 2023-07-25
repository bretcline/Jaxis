using System;
using System.Windows.Forms;
using LFI.Mobile.Controls;
using LFI.Mobile.Controls.Picklist;
using LFI.Mobile.Controls.Picklist.Grid;
using LFI.RFID.Format;
using System.Drawing;

namespace MobileInterrogator
{
    public partial class DataRowScreen : BaseScreen
    {
        public DataRowScreen(AppContext appContext)
            : base(ScreenTag.DataRow)
        {
            InitializeComponent();

            HeaderText = "Data";

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
            int numDataRows = (null != appContext.CurrentTagData.DataRows) ? appContext.CurrentTagData.DataRows.Count : 0;
            if (currentRowIndex < 0)
            currentRowIndex = (numDataRows > 0) ? 0 : -1;
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
                TagDataRow dataRow = appContext.CurrentTagData.DataRows[currentRowIndex];
                string dataName = appContext.ValueEditState.DataElementDef.Name;
                dataRow.Values[dataName] = appContext.ValueEditState.FinalValue;
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
                menuItemDone.Click += delegate 
                {
                    appContext.InitiateTagWrite( );
                    ScreenMgr.NavigateTo(ScreenTag.Home); 
                };
            }

            return menuItemDone;
        }

        public override MenuItem BuildRightMenu()
        {
            if (menuItemHeader == null)
            {
                menuItemHeader = new MenuItem { Enabled = true, Text = "Header" };
                menuItemHeader.Click += delegate { ScreenMgr.GoBack(); };
            }

            return menuItemHeader;
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
            int numDataRows = ( null != appContext.CurrentTagData.DataRows ) ? appContext.CurrentTagData.DataRows.Count : 0;
            if (currentRowIndex < 0)
                currentRowIndex = (numDataRows > 0) ? 0 : -1;

            UpdateButtonStates();

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

            btnEdit.Enabled = ((currentRowIndex != -1) && (!appContext.CurrentTagData.DataRows[currentRowIndex].IsLocked));

            Update();

            // If their aren't any rows, create one
            if (numDataRows == 0)
                OnNewRowClick(this, EventArgs.Empty);
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
            foreach (DataElementDef elemDef in appContext.CurrentTagFormat.DataRowDef.ElementDefs)
            {
                string value = string.Empty;
                if (currentRowIndex != -1)
                {
                    TagDataRow dataRow = appContext.CurrentTagData.DataRows[currentRowIndex];
                    if (dataRow.Values.ContainsKey(elemDef.Name))
                        value = dataRow.Values[elemDef.Name];
                }

                picklist.AddItem(PicklistItemFactory.CreateOneColumnTwoRows(properties, elemDef.Name, elemDef.Name, value));
            }
        }

        private void OnPickListSelectedIndexChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = (picklist.SelectedIndex != -1);
        }

        #endregion

        #region Other Event Handlers

        private void OnPreviousRowClick(Object sender, EventArgs e)
        {
            if (currentRowIndex > 0)
            {
                currentRowIndex--;
                UpdateButtonStates();
                DisposePicklist();
                RebuildPickList();
                Refresh();
            }
        }

        private void OnNextRowClick(Object sender, EventArgs e)
        {
            if (currentRowIndex < appContext.CurrentTagData.DataRows.Count)
            {
                currentRowIndex++;
                UpdateButtonStates();
                DisposePicklist();
                RebuildPickList();
                Refresh();
            }
        }

        private void OnNewRowClick(Object sender, EventArgs e)
        {
            if (currentRowIndex < (appContext.CurrentTagFormat.MaxDataRows - 1))
            {
                appContext.CurrentTagData.DataRows.Add(new TagDataRow());
                // Note: Also works with empty data which starts out with index -1 (++ will set it to zero)
                currentRowIndex++;
                UpdateButtonStates();
                DisposePicklist();
                RebuildPickList();
                Refresh();
            }
        }
        private void OnEditValueClick(Object sender, EventArgs e)
        {
            EditSelectedValue();
        }

        private void EditSelectedValue()
        {
            if ((appContext.CurrentTagData == null))
                return;

            if (currentRowIndex >= appContext.CurrentTagData.DataRows.Count)
                return;

            TagDataRow dataRow = appContext.CurrentTagData.DataRows[currentRowIndex];
            if (dataRow.IsLocked)
                return;

            int index = picklist.SelectedIndex;
            DataElementDef elemDef = appContext.CurrentTagFormat.DataRowDef.ElementDefs[index];
            string currentValue = string.Empty;
            if (dataRow.Values.ContainsKey(elemDef.Name))
                currentValue = dataRow.Values[elemDef.Name];

            appContext.ValueEditState.Init(elemDef, currentValue);
            ScreenMgr.NavigateTo(ScreenTag.ValueEditor);
        }

        private void UpdateButtonStates()
        {
            int numDataRows = ( null != appContext.CurrentTagData.DataRows ) ? appContext.CurrentTagData.DataRows.Count : 0;
            bool hasDataRows = ( numDataRows > 0 );

            btnPrev.Enabled = (hasDataRows && (currentRowIndex > 0));
            btnNext.Enabled = (hasDataRows && (currentRowIndex < numDataRows - 1));
            btnNew.Enabled = (currentRowIndex < appContext.CurrentTagFormat.MaxDataRows - 1);
            btnEdit.Enabled = (hasDataRows && (!appContext.CurrentTagData.DataRows[currentRowIndex].IsLocked));
        }

        #endregion

        #region Member Variables

        private AppContext appContext;
        private MenuItem menuItemDone;
        private MenuItem menuItemHeader;
        private Picklist picklist;
        private GridPicklistProperties properties;
        private int currentRowIndex = -1;

        #endregion
    }
}