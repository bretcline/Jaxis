using System;
using System.Windows.Forms;

namespace LFI.Mobile.Controls
{
    public interface IScreen : IDisposable
    {
		string RefTag { get; }
		string HeaderText { get; }
		bool IsModal { get; }
		ScreenManager ScreenMgr { get; set; }
        Control Control { get; }
        ScreenResult ScreenResult { get; }
		bool Activate();
		void Deactivate();
		void Reactivate();
        void HandleNavigationResults(ScreenResult result);

		MenuItem BuildLeftMenu();
		MenuItem BuildRightMenu();
	}
}
