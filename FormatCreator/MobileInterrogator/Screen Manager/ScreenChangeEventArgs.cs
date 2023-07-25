using System;
using System.Windows.Forms;

namespace LFI.Mobile.Controls
{
    public class ScreenChangeEventArgs : EventArgs
    {
        public ScreenChangeEventArgs(IScreen screen, MenuItem leftMenu, MenuItem rightMenu)
        {
            this.Screen = screen;
            this.LeftMenu = leftMenu;
            this.RightMenu = rightMenu;
        }

        public IScreen Screen;
        public MenuItem LeftMenu;
        public MenuItem RightMenu;
    }
}