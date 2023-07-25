using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AuditImport
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( )
        {
            Application.EnableVisualStyles( );
            Application.SetCompatibleTextRenderingDefault( false );
            var form = new AuditImportForm( );
            var businessLogic = new BusinessLogic( );
            form.DirectorySelected += businessLogic.OnDirectorySelected;
            form.SetBinding( businessLogic.Messages );
            form.ShowDialog( );
        }
    }
}
