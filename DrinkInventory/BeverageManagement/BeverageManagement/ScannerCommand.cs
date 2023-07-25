using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraNavBar;

namespace BeverageManagement
{
    public class ScannerCommand
    {
        public ScannerCommand()
        {
            Commands = new Dictionary<char, Action>();
        }

        public bool PlusEntered { get; set; }
        public bool DollarEntered { get; set; }
        public char LastChar { get; set; }

        protected Stack<char> m_Stack = new Stack<char>();

        public Dictionary<char, Action> Commands { get; protected set; }
        public static Dictionary<char, Action> Navigation = new Dictionary<char, Action>();

        public Func<KeyPressEventArgs, bool> CustomProcessor { get; set; }

        public bool ProcessKeys(KeyPressEventArgs _keyChar)
        {
            if (null != CustomProcessor)
            {
                _keyChar.Handled = CustomProcessor(_keyChar);
            }
            else
            {
                char key = _keyChar.KeyChar;
                m_Stack.Push(key);
                if ('+' == key)
                {
                    PlusEntered = true;
                    _keyChar.Handled = true;
                }
                else if (PlusEntered && '$' == key)
                {
                    DollarEntered = true;
                    _keyChar.Handled = true;
                }
                else if (PlusEntered && DollarEntered)
                {
                    if (Navigation.ContainsKey(key))
                    {
                        Navigation[key]();
                        _keyChar.Handled = true;
                    }
                    if (Commands.ContainsKey(key))
                    {
                        Commands[key]();
                        _keyChar.Handled = true;
                    }
                    else if (key == 0x0d)
                    {
                        PlusEntered = DollarEntered = false;
                        _keyChar.Handled = true;
                    }
                }
                else if ('+' != m_Stack.Peek() && '$' != m_Stack.Peek())
                {
                    PlusEntered = DollarEntered = false;
                }
            }
            return _keyChar.Handled;
        }
    }
}