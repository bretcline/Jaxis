using System;
using System.Windows.Forms;

namespace BeverageManagement.Forms.Reconcile
{
    static class Extensions
    {
        public static void Fire(this Form _instance, EventHandler _handler, EventArgs _e)
        {
            if (_handler != null)
            {
                _handler(_instance, _e);
            }
        }

        public static void Fire(this Form _instance, FormClosingEventHandler _handler, FormClosingEventArgs _e)
        {
            if (_handler != null)
            {
                _handler(_instance, _e);
            }
        }
    }
}
