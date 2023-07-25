using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LFI.RFID.Editor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //LFI.RFID.Format.ByteConverterTest test = new LFI.RFID.Format.ByteConverterTest();
            //LFI.RFID.Format.TagDataConverterTest test = new LFI.RFID.Format.TagDataConverterTest();
            //bool result = test.Test1();
            
            Application.Run(new EditorForm());
        }
    }
}
