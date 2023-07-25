using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BeverageManagement
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern
            bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern
            bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern
            bool IsIconic(IntPtr hWnd);

        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        public static Process PriorProcess()
        {
            var curr = Process.GetCurrentProcess();
            var procs = Process.GetProcessesByName(curr.ProcessName);
            return procs.FirstOrDefault(p => (p.Id != curr.Id) && (p.MainModule.FileName == curr.MainModule.FileName));
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( )
        {
            var process = PriorProcess();
            if ( null != process )
            {

                IntPtr hWnd = process.MainWindowHandle;
                // if iconic, we need to restore the window

                if (IsIconic(hWnd))
                {
                    ShowWindowAsync(hWnd, 9);
                }

                SetForegroundWindow(hWnd);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
        }
    }
}


    ///*** Create a License File ***/
    //public void CreateLicenseFile(string project_filename)
    //{
    //    LicenseGenerator licensegen = new LicenseGenerator(project_filename);

    //    licensegen.AdditonalLicenseInformation.Add("Name", "John Doe");
    //    licensegen.AdditonalLicenseInformation.Add("Company", "Acme");
    //    licensegen.HardwareLock_Enabled = true;
    //    licensegen.HardwareID = "1234-1234-1234-1234-1234";

    //    licensegen.CreateLicenseFile(@"C:\MyProject\newlicense.license");
    //}
