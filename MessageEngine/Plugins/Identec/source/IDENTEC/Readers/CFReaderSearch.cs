namespace IDENTEC.Readers
{
    using IDENTEC;
    using System;

    public sealed class CFReaderSearch
    {
        private CFReaderSearch()
        {
        }

        private static bool FindName(string[] rgstNames, string stName)
        {
            if (rgstNames != null)
            {
                for (int i = 0; i < rgstNames.Length; i++)
                {
                    if (rgstNames[i] == stName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static int FindReaderComPort()
        {
            string[] subKeyNames;
            int length;
            string str = "Identec_Solutions_AG-i-CARD_CF";
            RegistryKey key = null;
            RegistryKey key2 = null;
            string name = null;
            string stName = null;
            if (NativeMethods.FullFramework)
            {
                name = @"SYSTEM\CurrentControlSet\Enum\PCMCIA";
                stName = @"\1\Control";
                string str4 = @"\1\Device Parameters";
                try
                {
                    key = Registry.LocalMachine.OpenSubKey(name);
                    if (key == null)
                    {
                        throw new NotSupportedException("The IDENTEC SOLUTIONS CF driver has not been installed");
                    }
                    subKeyNames = key.GetSubKeyNames();
                    key.Close();
                    length = 0;
                    while (length < subKeyNames.Length)
                    {
                        if (subKeyNames[length].StartsWith(str))
                        {
                            if (key2 != null)
                            {
                                key2.Close();
                            }
                            key2 = Registry.LocalMachine.OpenSubKey(name + @"\" + subKeyNames[length] + stName);
                            if ((key2 != null) && FindName(key2.GetValueNames(), "AllocConfig"))
                            {
                                break;
                            }
                        }
                        length++;
                    }
                    key2.Close();
                    key2 = Registry.LocalMachine.OpenSubKey(name + @"\" + subKeyNames[length] + str4);
                    string str5 = key2.GetValue("PortName") as string;
                    key2.Close();
                    return int.Parse(str5.Replace("COM", "").Replace(":", ""));
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            name = @"Drivers\Active";
            stName = "PnpId";
            try
            {
                key = Registry.LocalMachine.OpenSubKey(name);
                if (key == null)
                {
                    throw new NotSupportedException("The IDENTEC SOLUTIONS CF driver has not been installed");
                }
                subKeyNames = key.GetSubKeyNames();
                key.Close();
                length = subKeyNames.Length;
                do
                {
                    if (key2 != null)
                    {
                        key2.Close();
                    }
                    key2 = Registry.LocalMachine.OpenSubKey(name + @"\" + subKeyNames[--length]);
                }
                while ((length > 0) && (FindName(key2.GetValueNames(), stName) ? !key2.GetValue(stName).ToString().StartsWith(str) : true));
                if (length <= 0)
                {
                    throw new NotSupportedException("The IDENTEC SOLUTIONS CF card is not inserted");
                }
                string str6 = key2.GetValue("Name").ToString();
                key2.Close();
                return int.Parse(str6.Replace("COM", "").Replace(":", ""));
            }
            catch (NullReferenceException)
            {
                return -1;
            }
        }
    }
}

