using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LFI.Mobile.Controls
{
    public class ScreenManager : IDisposable
    {
        private IScreen currentScreen;
        private ScreenResult lastScreenResult;

        private readonly Dictionary<string, MenuItem> leftItems;
        private readonly Dictionary<string, MenuItem> rightItems;

        private readonly Stack<IScreen> prevScreens;
        private readonly Dictionary<string, IScreen> screens;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenManager"/> class.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        public ScreenManager()
        {
            screens = new Dictionary<string, IScreen>();
            prevScreens = new Stack<IScreen>();
            leftItems = new Dictionary<string, MenuItem>();
            rightItems = new Dictionary<string, MenuItem>();
        }

        //----------------------------------------------------------------------

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            foreach (IScreen screen in screens.Values)
                screen.Dispose();
        }

        #endregion

        public event EventHandler<ScreenChangeEventArgs> HeaderTextChanged;
        public event EventHandler<ScreenChangeEventArgs> BeforeScreenChange;
        public event EventHandler<ScreenChangeEventArgs> AfterScreenChange;

        //----------------------------------------------------------------------
        /// <summary>
        /// Registers the screen.
        /// </summary>
        /// <param name="screen">The screen.</param>
        public void RegisterScreen(IScreen screen)
        {
            if (screens.ContainsKey(screen.RefTag))
                return;

            screen.ScreenMgr = this;

            leftItems.Add(screen.RefTag, screen.BuildLeftMenu());
            rightItems.Add(screen.RefTag, screen.BuildRightMenu());

            screens.Add(screen.RefTag, screen);
            screen.Control.Hide();
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Reloads all menus.
        /// </summary>
        public void ReloadAllMenus()
        {
            Cursor.Current = Cursors.WaitCursor;

            foreach (KeyValuePair<string, IScreen> screen in screens)
            {
                //screen.Value.Activate();
                //screen.Value.Deactivate();
                ResetLeftMenu(screen.Value);
                ResetRightMenu(screen.Value);
            }

            Cursor.Current = Cursors.Default;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        public bool NavigateTo(string tag)
        {
            Cursor.Current = Cursors.WaitCursor;
            IScreen screen;

            try
            {
                screen = TryGetScreen(tag);
            }
            catch
            {
                return false;
            }

            CleanUpCurrentScreen();
            ActivateScreen(screen);

            Cursor.Current = Cursors.Default;
            return true;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Navigates to the previous screen.
        /// </summary>
        /// <returns></returns>
        public bool GoBack()
        {
            if (prevScreens.Count == 0)
                return false;

            Cursor.Current = Cursors.WaitCursor;
            IScreen screen = prevScreens.Pop();

            // The previous screen on the stack may be the current screen if the screen navigated to a modal
            // and then the modal navigates (without using "GoBack()") to the original screen
            if (screen == currentScreen)
                screen = prevScreens.Pop();

            CleanUpCurrentScreen();
            ActivateScreen(screen);

            Cursor.Current = Cursors.Default;
            return true;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Refreshes the current screen.
        /// </summary>
        public void RefreshCurrentScreen()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (currentScreen != null)
                currentScreen.Reactivate();

            Cursor.Current = Cursors.Default;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Gets the current screen.
        /// </summary>
        /// <returns></returns>
        public IScreen GetCurrentScreen()
        {
            if (currentScreen != null)
                return currentScreen;

            return null;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Gets the screen.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        public IScreen GetScreen(string tag)
        {
            try
            {
                return TryGetScreen(tag);
            }
            catch
            {
                return null;
            }
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Clears the previous screens.
        /// </summary>
        public void ClearPreviousScreens()
        {
            prevScreens.Clear();
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Resets the left menu.
        /// </summary>
        public void ResetLeftMenu()
        {
            if (leftItems.ContainsKey(currentScreen.RefTag))
            {
                MenuItem item = leftItems[currentScreen.RefTag];
                leftItems.Remove(currentScreen.RefTag);
                item.Dispose();
            }

            leftItems.Add(currentScreen.RefTag, currentScreen.BuildLeftMenu());
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Resets the left menu.
        /// </summary>
        /// <param name="screen">The screen.</param>
        public void ResetLeftMenu(IScreen screen)
        {
            if (leftItems.ContainsKey(screen.RefTag))
            {
                MenuItem item = leftItems[screen.RefTag];
                leftItems.Remove(screen.RefTag);
                item.Dispose();
            }

            leftItems.Add(screen.RefTag, screen.BuildLeftMenu());
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Resets the right menu.
        /// </summary>
        public void ResetRightMenu()
        {
            if (rightItems.ContainsKey(currentScreen.RefTag))
            {
                MenuItem item = rightItems[currentScreen.RefTag];
                rightItems.Remove(currentScreen.RefTag);
                if (item != null)
                    item.Dispose();
            }

            rightItems.Add(currentScreen.RefTag, currentScreen.BuildRightMenu());
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Resets the right menu.
        /// </summary>
        /// <param name="screen">The screen.</param>
        public void ResetRightMenu(IScreen screen)
        {
            if (rightItems.ContainsKey(screen.RefTag))
            {
                MenuItem item = rightItems[screen.RefTag];
                rightItems.Remove(screen.RefTag);
                if (item != null)
                    item.Dispose();
            }

            rightItems.Add(screen.RefTag, screen.BuildRightMenu());
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Invalidates the header text.
        /// </summary>
        public void InvalidateHeaderText()
        {
            if (HeaderTextChanged != null)
                HeaderTextChanged(this, new ScreenChangeEventArgs(currentScreen, null, null));
        }


        //----------------------------------------------------------------------
        /// <summary>
        /// Tries the get screen.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        private IScreen TryGetScreen(string tag)
        {
            if (!screens.ContainsKey(tag))
                throw new Exception(String.Format("Unable to find the screen: {0}", tag));

            return screens[tag];
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Cleans up current screen.
        /// </summary>
        private void CleanUpCurrentScreen()
        {
            if (currentScreen != null)
            {
                lastScreenResult = currentScreen.ScreenResult;
                lastScreenResult.ScreenTag = currentScreen.RefTag;

                currentScreen.Deactivate();
                currentScreen.Control.Hide();

                if (!currentScreen.IsModal)
                    UpdateStack();
            }
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Activates the screen.
        /// </summary>
        /// <param name="screen">The screen.</param>
        private void ActivateScreen(IScreen screen)
        {
            // clears the screen results for the screen being navigated to
            screen.ScreenResult.Clear();

            currentScreen = screen;

            if (lastScreenResult != null && lastScreenResult.HasValue())
                screen.HandleNavigationResults(lastScreenResult);

            ScreenChangeEventArgs screenArgs = new ScreenChangeEventArgs(screen, leftItems[screen.RefTag], rightItems[screen.RefTag]);

            if (BeforeScreenChange != null)
                BeforeScreenChange(this, screenArgs);

            screen.Activate();
            screen.Control.Show();

            if (AfterScreenChange != null)
                AfterScreenChange(this, screenArgs);
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Updates the stack.
        /// </summary>
        private void UpdateStack()
        {
            if (!prevScreens.Contains(currentScreen))
                prevScreens.Push(currentScreen);
        }
    }
}