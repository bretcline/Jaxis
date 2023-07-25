using System;
using System.Windows.Forms;
using LFI.RFID.Format;

namespace MobileInterrogator
{
    public interface IValueEditor
    {
        void Activate(); 
        DataElementDef DataElementDef { get; set; }
        string Value { get; set; }
        bool Validate();
        Control Control { get; }
    }
}
