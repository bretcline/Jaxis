using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LFI.Mobile.Controls;

namespace MobileInterrogator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            appContext = new AppContext();
            InitializeScreens();

            // Start out on the home screen
            screenMgr.NavigateTo(ScreenTag.Home);
        }

        #region Screen Management

        private void InitializeScreens()
        {
            screenMgr = new ScreenManager();
            screenMgr.BeforeScreenChange += OnBeforeScreenChanged;
            screenMgr.AfterScreenChange += OnAfterScreenChanged; 
            screenMgr.HeaderTextChanged += OnHeaderTextChanged;

            RegisterScreens();
        }

        private void RegisterScreens()
        {
            CursorState.BeginWait();

            try
            {
                screenMgr.RegisterScreen(new HomeScreen(appContext));
                screenMgr.RegisterScreen(new FormatScreen(appContext));
                screenMgr.RegisterScreen(new HeaderScreen(appContext));
                screenMgr.RegisterScreen(new DataRowScreen(appContext));
                screenMgr.RegisterScreen(new ValueEditScreen(appContext));
            }
            finally
            {
                CursorState.EndWait();
            }
        }


        void OnAfterScreenChanged(object sender, ScreenChangeEventArgs e)
        {
            Text = e.Screen.HeaderText;
        }

        private void OnBeforeScreenChanged(object sender, ScreenChangeEventArgs e)
        {
            // clear current menu
            mainMenu.MenuItems.Clear();

            // clear current screen content
            contentPanel.Controls.Clear();

            // add new page's menus
            if (e.LeftMenu != null)
                mainMenu.MenuItems.Add(e.LeftMenu);

            if (e.RightMenu != null)
                mainMenu.MenuItems.Add(e.RightMenu);

            e.Screen.Control.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(e.Screen.Control);
        }

        private void OnHeaderTextChanged(object sender, ScreenChangeEventArgs e)
        {
            Text = e.Screen.HeaderText;
        }

        #endregion

        #region Member Variables

        private AppContext appContext;
        private ScreenManager screenMgr;

        #endregion
    }
}