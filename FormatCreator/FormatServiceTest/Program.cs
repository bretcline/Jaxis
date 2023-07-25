using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LFI.RFID.Format;

namespace FormatServiceTest
{
    class Program
    {
        static void Main( string[] args )
        {
            FormatService.FormatService fs = new FormatService.FormatService();            
            FormatDef Def = fs.GetFormat( Guid.Empty );
        }
    }
}
