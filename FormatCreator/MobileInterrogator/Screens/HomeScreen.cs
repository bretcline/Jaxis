using System;
using System.Windows.Forms;
using LFI.Mobile.Controls;


namespace MobileInterrogator
{
    public partial class HomeScreen : BaseScreen
    {
        public HomeScreen(AppContext appContext)
            : base(ScreenTag.Home)
        {
            InitializeComponent();

            HeaderText = "Home";

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
            // Returning to the home screen clears out the current state
            appContext.ClearState();
            ScreenMgr.ClearPreviousScreens();

            return true;
        }

        public override MenuItem BuildLeftMenu()
        {
            if (menuItemExit == null)
            {
                menuItemExit = new MenuItem { Enabled = true, Text = "Exit" };
                menuItemExit.Click += OnExitClicked;
            }

            return menuItemExit;
        }

        #endregion

        #region Other Event Handlers

        private void OnExitClicked(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Exit Application?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
                Cursor.Current = Cursors.Default;
                Application.Exit();
            }
        }

        private void OnScanTagClick(object sender, EventArgs e)
        {
            bool success = appContext.InitiateTagRead();
            if (success == true)
            {
                if (ScreenMgr != null) 
                    ScreenMgr.NavigateTo(ScreenTag.HeaderRow);
            }            
        }

        private void OnCreateTagClick(object sender, EventArgs e)
        {
            if (ScreenMgr != null) ScreenMgr.NavigateTo(ScreenTag.FormatSelect);
        }

        private void OnUploadDataClick(object sender, EventArgs e)
        {
            CursorState.BeginWait();

            // TODO: Trigger an updload of cached data files
        }

        #endregion

        #region Member Variables

        private AppContext appContext;
        private MenuItem menuItemExit;

        #endregion
    }
}