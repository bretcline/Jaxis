using System;
using System.Windows.Forms;
using LFI.Mobile.Controls;
using LFI.Mobile.Controls.Picklist;
using LFI.Mobile.Controls.Picklist.Grid;
using LFI.RFID.Format;
using System.Drawing;

namespace MobileInterrogator
{
    public partial class FormatScreen : BaseScreen
    {
        public FormatScreen(AppContext appContext)
            : base(ScreenTag.FormatSelect)
        {
            InitializeComponent();

            HeaderText = "Select Format";

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

        public override MenuItem BuildLeftMenu()
        {
            if (menuItemCancel == null)
            {
                menuItemCancel = new MenuItem { Enabled = true, Text = "Cancel" };
                menuItemCancel.Click += delegate { ScreenMgr.GoBack(); };
            }

            return menuItemCancel;
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
            picklist.DoubleClick += delegate { OnCreateTagClick(this, EventArgs.Empty); };


            btnCreateTag.Enabled = appContext.AvailableFormats.Count > 0;

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
            foreach (FormatDef formatDef in appContext.AvailableFormats)
                picklist.AddItem(PicklistItemFactory.CreateOneColumnTwoRows(properties, formatDef.ID.ToString(), formatDef.Name, formatDef.Description));
        }

        private void OnPickListSelectedIndexChanged(object sender, EventArgs e)
        {
            btnCreateTag.Enabled = (picklist.SelectedIndex != -1);
        }

        #endregion

        #region Other Event Handlers

        private void OnCreateTagClick(object sender, EventArgs e)
        {
            string formatIDText = picklist.SelectedItemID;
            Guid formatID = new Guid(formatIDText);

            bool success = appContext.CreateNewTagData(formatID);
            if (success == true)
            {
                if (ScreenMgr != null)
                    ScreenMgr.NavigateTo(ScreenTag.HeaderRow);
            }
        }

        #endregion

        #region Member Variables

        private AppContext appContext;
        private MenuItem menuItemCancel;
        private Picklist picklist;
        private GridPicklistProperties properties;

        #endregion
    }
}