using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Jaxis.Util.Log4Net;

namespace AccountValidator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( )
        {
            Log.Wrap<int>("Program::Main", LogType.Debug, true, () =>
            {
                Application.EnableVisualStyles( );
                Application.SetCompatibleTextRenderingDefault( false );
                frmAccountEntry Entry = new frmAccountEntry( );
                Entry.Show( );
                Application.Run( new frmAccountValidator( ) );
                return 1;
            });
        }
    }
}
