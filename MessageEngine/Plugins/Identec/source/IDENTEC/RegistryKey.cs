namespace IDENTEC
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Text;

    internal sealed class RegistryKey : MarshalByRefObject, IDisposable
    {
        private const int ERROR_NO_MORE_ITEMS = 0x103;
        private const int KEY_ALL_ACCESS = 0x2003f;
        private const int KEY_CREATE_LINK = 0x20;
        private const int KEY_CREATE_SUB_KEY = 4;
        private const int KEY_ENUMERATE_SUB_KEYS = 8;
        private const int KEY_EXECUTE = 0x20019;
        private const int KEY_NOTIFY = 0x10;
        private const int KEY_QUERY_VALUE = 1;
        private const int KEY_READ = 0x20019;
        private const int KEY_SET_VALUE = 2;
        private const int KEY_WRITE = 0x20006;
        private uint m_handle;
        private bool m_isroot;
        private string m_name;
        private bool m_writable;
        private const int READ_CONTROL = 0x20000;

        internal RegistryKey(uint handle, string name, bool writable, bool isroot)
        {
            this.m_handle = handle;
            this.m_name = name;
            this.m_writable = writable;
            this.m_isroot = isroot;
        }

        private bool CheckHKey()
        {
            if (this.m_handle == 0)
            {
                return false;
            }
            return true;
        }

        public void Close()
        {
            if (!this.m_isroot && this.CheckHKey())
            {
                int num = 0;
                if (IDENTEC.NativeMethods.FullFramework)
                {
                    num = RegCloseKeyPC(this.m_handle);
                }
                else
                {
                    num = RegCloseKey(this.m_handle);
                }
                if (num != 0)
                {
                    throw new ExternalException("Error closing RegistryKey");
                }
                this.m_handle = 0;
            }
        }

        public RegistryKey CreateSubKey(string subkey)
        {
            return this.CreateSubKey(subkey, false);
        }

        public RegistryKey CreateSubKey(string subkey, bool createVolatile)
        {
            if (!this.CheckHKey())
            {
                throw new ObjectDisposedException("The RegistryKey on which this method is being invoked is closed (closed keys cannot be accessed).");
            }
            if (subkey == null)
            {
                throw new ArgumentNullException("The specified subkey is null.");
            }
            if (subkey.Length >= 0x100)
            {
                throw new ArgumentException("The length of the specified subkey is longer than the maximum length allowed (255 characters).");
            }
            uint phkResult = 0;
            KeyDisposition lpdwDisposition = (KeyDisposition) 0;
            RegOptions nonVolatile = RegOptions.NonVolatile;
            if (createVolatile)
            {
                nonVolatile = RegOptions.Volatile;
            }
            if (RegCreateKeyEx(this.m_handle, subkey, 0, null, nonVolatile, 0, IntPtr.Zero, ref phkResult, ref lpdwDisposition) != 0)
            {
                throw new ExternalException("An error occured creating the registry key.");
            }
            return new RegistryKey(phkResult, this.m_name + @"\" + subkey, true, false);
        }

        public void DeleteSubKey(string subkey)
        {
            this.DeleteSubKey(subkey, true);
        }

        public void DeleteSubKey(string subkey, bool throwOnMissingSubKey)
        {
            if (string.IsNullOrEmpty(subkey))
            {
                throw new ArgumentNullException("subkey");
            }
            if (!this.CheckHKey())
            {
                throw new ObjectDisposedException("The RegistryKey on which this method is being invoked is closed (closed keys cannot be accessed).");
            }
            if ((RegDeleteKey(this.m_handle, subkey) != 0) && throwOnMissingSubKey)
            {
                throw new ArgumentException("The specified subkey is not a valid reference to a registry key");
            }
        }

        public void DeleteSubKeyTree(string subkey)
        {
            this.DeleteSubKey(subkey, true);
        }

        public void DeleteValue(string name)
        {
            this.DeleteValue(name, true);
        }

        public void DeleteValue(string name, bool throwOnMissingValue)
        {
            if (!this.m_writable)
            {
                throw new UnauthorizedAccessException("Cannot delete a value from a RegistryKey opened as ReadOnly.");
            }
            if (!this.CheckHKey())
            {
                throw new ObjectDisposedException("The RegistryKey being manipulated is closed (closed keys cannot be accessed).");
            }
            if (name == null)
            {
                throw new ArgumentException("name is null");
            }
            if ((RegDeleteValue(this.m_handle, name) == 0x57) && throwOnMissingValue)
            {
                throw new ArgumentException("name is not a valid reference to a value (and throwOnMissingValue is true) or name is null");
            }
        }

        public void Dispose()
        {
            this.Close();
        }

        public void Flush()
        {
            RegFlushKey(this.m_handle);
        }

        public string[] GetSubKeyNames()
        {
            if (!this.CheckHKey())
            {
                throw new ObjectDisposedException("The RegistryKey being manipulated is closed (closed keys cannot be accessed).");
            }
            int num = 0;
            ArrayList list = new ArrayList();
            int iIndex = 0;
            StringBuilder sKeyName = new StringBuilder(0x100);
            int iKeyNameLen = 0x100;
            if (IDENTEC.NativeMethods.FullFramework)
            {
                num = RegEnumKeyExPC(this.m_handle, iIndex, sKeyName, ref iKeyNameLen, 0, null, 0, 0);
            }
            else
            {
                num = RegEnumKeyEx(this.m_handle, iIndex, sKeyName, ref iKeyNameLen, 0, null, 0, 0);
            }
            while (num != 0x103)
            {
                list.Add(sKeyName.ToString());
                iIndex++;
                iKeyNameLen = 0x100;
                if (IDENTEC.NativeMethods.FullFramework)
                {
                    num = RegEnumKeyExPC(this.m_handle, iIndex, sKeyName, ref iKeyNameLen, 0, null, 0, 0);
                }
                else
                {
                    num = RegEnumKeyEx(this.m_handle, iIndex, sKeyName, ref iKeyNameLen, 0, null, 0, 0);
                }
                if (iIndex > this.SubKeyCount)
                {
                    break;
                }
            }
            list.Sort();
            return (string[]) list.ToArray(typeof(string));
        }

        public object GetValue(string name)
        {
            return this.GetValue(name, null);
        }

        public object GetValue(string name, object defaultValue)
        {
            if (!this.CheckHKey())
            {
                throw new ObjectDisposedException("The RegistryKey being manipulated is closed (closed keys cannot be accessed).");
            }
            RegistryValueKind unknown = RegistryValueKind.Unknown;
            int lpcbData = 0;
            int num2 = 0;
            if (IDENTEC.NativeMethods.FullFramework)
            {
                num2 = RegQueryValueExPC(this.m_handle, name, 0, ref unknown, null, ref lpcbData);
            }
            else
            {
                num2 = RegQueryValueEx(this.m_handle, name, 0, ref unknown, null, ref lpcbData);
            }
            if (num2 != 0x57)
            {
                byte[] lpData = new byte[lpcbData];
                if (IDENTEC.NativeMethods.FullFramework)
                {
                    num2 = RegQueryValueExPC(this.m_handle, name, 0, ref unknown, lpData, ref lpcbData);
                }
                else
                {
                    num2 = RegQueryValueEx(this.m_handle, name, 0, ref unknown, lpData, ref lpcbData);
                }
                switch (unknown)
                {
                    case RegistryValueKind.String:
                    case RegistryValueKind.ExpandString:
                    {
                        if (!IDENTEC.NativeMethods.FullFramework)
                        {
                            string str2 = Encoding.Unicode.GetString(lpData, 0, lpcbData);
                            return str2.Substring(0, str2.IndexOf('\0'));
                        }
                        string str = Encoding.ASCII.GetString(lpData, 0, lpcbData);
                        return str.Substring(0, str.IndexOf('\0'));
                    }
                    case RegistryValueKind.Binary:
                        return lpData;

                    case RegistryValueKind.DWord:
                        return BitConverter.ToInt32(lpData, 0);

                    case (RegistryValueKind.DWord | RegistryValueKind.String):
                    case (RegistryValueKind.DWord | RegistryValueKind.ExpandString):
                        return defaultValue;

                    case RegistryValueKind.MultiString:
                    {
                        string str3 = Encoding.Unicode.GetString(lpData, 0, lpcbData);
                        return str3.Substring(0, str3.IndexOf("\0\0")).Split(new char[1]);
                    }
                }
            }
            return defaultValue;
        }

        public RegistryValueKind GetValueKind(string name)
        {
            if (!this.CheckHKey())
            {
                throw new ObjectDisposedException("The RegistryKey being manipulated is closed (closed keys cannot be accessed).");
            }
            RegistryValueKind unknown = RegistryValueKind.Unknown;
            int lpcbData = 0;
            if (RegQueryValueEx(this.m_handle, name, 0, ref unknown, null, ref lpcbData) != 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Error retrieving value type");
            }
            return unknown;
        }

        public string[] GetValueNames()
        {
            if (!this.CheckHKey())
            {
                throw new ObjectDisposedException("The RegistryKey being manipulated is closed (closed keys cannot be accessed).");
            }
            int num = 0;
            ArrayList list = new ArrayList();
            int iIndex = 0;
            StringBuilder sValueName = new StringBuilder(0x100);
            int iValueNameLen = 0x100;
            if (IDENTEC.NativeMethods.FullFramework)
            {
                num = RegEnumValuePC(this.m_handle, iIndex, sValueName, ref iValueNameLen, 0, 0, null, 0);
            }
            else
            {
                num = RegEnumValue(this.m_handle, iIndex, sValueName, ref iValueNameLen, 0, 0, null, 0);
            }
            while (num != 0x103)
            {
                list.Add(sValueName.ToString());
                iIndex++;
                iValueNameLen = 0x100;
                if (IDENTEC.NativeMethods.FullFramework)
                {
                    num = RegEnumValuePC(this.m_handle, iIndex, sValueName, ref iValueNameLen, 0, 0, null, 0);
                }
                else
                {
                    num = RegEnumValue(this.m_handle, iIndex, sValueName, ref iValueNameLen, 0, 0, null, 0);
                }
            }
            list.Sort();
            return (string[]) list.ToArray(typeof(string));
        }

        public RegistryKey OpenSubKey(string name)
        {
            return this.OpenSubKey(name, false);
        }

        public RegistryKey OpenSubKey(string name, bool writable)
        {
            if (!this.CheckHKey())
            {
                throw new ObjectDisposedException("The RegistryKey being manipulated is closed (closed keys cannot be accessed).");
            }
            if (name == null)
            {
                throw new ArgumentNullException("name is null.");
            }
            if (name.Length >= 0x100)
            {
                throw new ArgumentException("The length of the specified subkey is longer than the maximum length allowed (255 characters).");
            }
            uint phkResult = 0;
            int num2 = 0;
            if (IDENTEC.NativeMethods.FullFramework)
            {
                num2 = RegOpenKeyExPC(this.m_handle, name, 0, 0x20019, ref phkResult);
            }
            else
            {
                num2 = RegOpenKeyEx(this.m_handle, name, 0, 0, ref phkResult);
            }
            if (num2 == 0)
            {
                return new RegistryKey(phkResult, this.m_name + @"\" + name, writable, false);
            }
            return null;
        }

        [DllImport("coredll.dll", SetLastError=true)]
        private static extern int RegCloseKey(uint hKey);
        [DllImport("advapi32.dll", EntryPoint="RegCloseKey", SetLastError=true)]
        private static extern int RegCloseKeyPC(uint hKey);
        [DllImport("coredll.dll", SetLastError=true)]
        private static extern int RegCreateKeyEx(uint hKey, string lpSubKey, int lpReserved, string lpClass, RegOptions dwOptions, int samDesired, IntPtr lpSecurityAttributes, ref uint phkResult, ref KeyDisposition lpdwDisposition);
        [DllImport("coredll.dll", SetLastError=true)]
        private static extern int RegDeleteKey(uint hKey, string keyName);
        [DllImport("coredll.dll", SetLastError=true)]
        private static extern int RegDeleteValue(uint hKey, string valueName);
        [DllImport("coredll.dll", SetLastError=true)]
        private static extern int RegEnumKeyEx(uint hKey, int iIndex, StringBuilder sKeyName, ref int iKeyNameLen, int iReservedZero, byte[] sClassName, int iClassNameLenZero, int iFiletimeZero);
        [DllImport("advapi32.dll", EntryPoint="RegEnumKeyEx", SetLastError=true)]
        private static extern int RegEnumKeyExPC(uint hKey, int iIndex, StringBuilder sKeyName, ref int iKeyNameLen, int iReservedZero, byte[] sClassName, int iClassNameLenZero, int iFiletimeZero);
        [DllImport("coredll.dll", SetLastError=true)]
        private static extern int RegEnumValue(uint hKey, int iIndex, StringBuilder sValueName, ref int iValueNameLen, int iReservedZero, int iTypeZero, byte[] byData, int iDataLenZero);
        [DllImport("advapi32.dll", EntryPoint="RegEnumValue", SetLastError=true)]
        private static extern int RegEnumValuePC(uint hKey, int iIndex, StringBuilder sValueName, ref int iValueNameLen, int iReservedZero, int iTypeZero, byte[] byData, int iDataLenZero);
        [DllImport("coredll.dll", SetLastError=true)]
        private static extern int RegFlushKey(uint hKey);
        [DllImport("coredll.dll", SetLastError=true)]
        private static extern int RegOpenKeyEx(uint hKey, string lpSubKey, int ulOptions, int samDesired, ref uint phkResult);
        [DllImport("advapi32.dll", EntryPoint="RegOpenKeyEx", SetLastError=true)]
        private static extern int RegOpenKeyExPC(uint hKey, string lpSubKey, int ulOptions, int samDesired, ref uint phkResult);
        [DllImport("coredll.dll", SetLastError=true)]
        private static extern int RegQueryInfoKey(uint hKey, char[] lpClass, ref int lpcbClass, int reservedZero, out int cSubkey, out int iMaxSubkeyLen, out int lpcbMaxSubkeyClassLen, out int cValueNames, out int iMaxValueNameLen, out int iMaxValueLen, int securityDescriptorZero, int lastWriteTimeZero);
        [DllImport("advapi32.dll", EntryPoint="RegQueryInfoKey", SetLastError=true)]
        private static extern int RegQueryInfoKeyPC(uint hKey, char[] lpClass, ref int lpcbClass, int reservedZero, out int cSubkey, out int iMaxSubkeyLen, out int lpcbMaxSubkeyClassLen, out int cValueNames, out int iMaxValueNameLen, out int iMaxValueLen, int securityDescriptorZero, int lastWriteTimeZero);
        [DllImport("coredll.dll", SetLastError=true)]
        private static extern int RegQueryValueEx(uint hKey, string lpValueName, int lpReserved, ref RegistryValueKind lpType, byte[] lpData, ref int lpcbData);
        [DllImport("advapi32.dll", EntryPoint="RegQueryValueEx", SetLastError=true)]
        private static extern int RegQueryValueExPC(uint hKey, string lpValueName, int lpReserved, ref RegistryValueKind lpType, byte[] lpData, ref int lpcbData);
        [DllImport("coredll.dll", EntryPoint="RegSetValueExW", SetLastError=true)]
        private static extern int RegSetValueEx(uint hKey, string lpValueName, int lpReserved, RegistryValueKind lpType, byte[] lpData, int lpcbData);
        public void SetValue(string name, object value)
        {
            byte[] bytes;
            int num;
            if (!this.m_writable)
            {
                throw new UnauthorizedAccessException("Cannot set value on RegistryKey which was opened as ReadOnly");
            }
            if (!this.CheckHKey())
            {
                throw new ObjectDisposedException("The RegistryKey being manipulated is closed (closed keys cannot be accessed).");
            }
            RegistryValueKind unknown = RegistryValueKind.Unknown;
            string str2 = value.GetType().ToString();
            if (str2 != null)
            {
                if (!(str2 == "System.String"))
                {
                    if (str2 == "System.String[]")
                    {
                        StringBuilder builder = new StringBuilder();
                        foreach (string str in (string[]) value)
                        {
                            builder.Append(str + '\0');
                        }
                        builder.Append('\0');
                        unknown = RegistryValueKind.MultiString;
                        bytes = Encoding.Unicode.GetBytes(builder.ToString());
                        goto Label_012F;
                    }
                    if (str2 == "System.Byte[]")
                    {
                        unknown = RegistryValueKind.Binary;
                        bytes = (byte[]) value;
                        goto Label_012F;
                    }
                    if (str2 == "System.Int32")
                    {
                        unknown = RegistryValueKind.DWord;
                        bytes = BitConverter.GetBytes((int) value);
                        goto Label_012F;
                    }
                    if (str2 == "System.UInt32")
                    {
                        unknown = RegistryValueKind.DWord;
                        bytes = BitConverter.GetBytes((uint) value);
                        goto Label_012F;
                    }
                }
                else
                {
                    unknown = RegistryValueKind.String;
                    bytes = Encoding.Unicode.GetBytes(((string) value) + '\0');
                    goto Label_012F;
                }
            }
            throw new ArgumentException("value is not a supported type");
        Label_012F:
            num = bytes.Length;
            if (RegSetValueEx(this.m_handle, name, 0, unknown, bytes, num) != 0)
            {
                throw new ExternalException("Error writing to the RegistryKey");
            }
        }

        public void SetValue(string name, object value, RegistryValueKind valueKind)
        {
            byte[] bytes;
            if (!this.m_writable)
            {
                throw new UnauthorizedAccessException("Cannot set value on RegistryKey which was opened as ReadOnly");
            }
            if (!this.CheckHKey())
            {
                throw new ObjectDisposedException("The RegistryKey being manipulated is closed (closed keys cannot be accessed).");
            }
            switch (valueKind)
            {
                case RegistryValueKind.String:
                    bytes = Encoding.Unicode.GetBytes(((string) value) + '\0');
                    break;

                case RegistryValueKind.Binary:
                    bytes = (byte[]) value;
                    break;

                case RegistryValueKind.DWord:
                    if (value is uint)
                    {
                        bytes = BitConverter.GetBytes((uint) value);
                    }
                    else
                    {
                        bytes = BitConverter.GetBytes(Convert.ToInt32(value));
                    }
                    break;

                case RegistryValueKind.MultiString:
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (string str in (string[]) value)
                    {
                        builder.Append(str + '\0');
                    }
                    builder.Append('\0');
                    bytes = Encoding.Unicode.GetBytes(builder.ToString());
                    break;
                }
                default:
                    this.SetValue(name, value);
                    return;
            }
            int length = bytes.Length;
            if (RegSetValueEx(this.m_handle, name, 0, valueKind, bytes, length) != 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Error writing to the RegistryKey");
            }
        }

        public override string ToString()
        {
            if (!this.CheckHKey())
            {
                throw new ObjectDisposedException("The RegistryKey being manipulated is closed (closed keys cannot be accessed).");
            }
            return (this.m_name + " [0x" + this.m_handle.ToString("X") + "]");
        }

        public string Name
        {
            get
            {
                return this.m_name;
            }
        }

        public int SubKeyCount
        {
            get
            {
                int num;
                int num2;
                int num3;
                int num4;
                int num5;
                int num6;
                if (!this.CheckHKey())
                {
                    throw new ObjectDisposedException("The RegistryKey being manipulated is closed (closed keys cannot be accessed).");
                }
                char[] lpClass = new char[0x100];
                int length = lpClass.Length;
                if (IDENTEC.NativeMethods.FullFramework)
                {
                    if (RegQueryInfoKeyPC(this.m_handle, lpClass, ref length, 0, out num, out num3, out num4, out num2, out num5, out num6, 0, 0) != 0)
                    {
                        throw new ExternalException("Error retrieving registry properties");
                    }
                    return num;
                }
                if (RegQueryInfoKey(this.m_handle, lpClass, ref length, 0, out num, out num3, out num4, out num2, out num5, out num6, 0, 0) != 0)
                {
                    throw new ExternalException("Error retrieving registry properties");
                }
                return num;
            }
        }

        public int ValueCount
        {
            get
            {
                int num;
                int num2;
                int num3;
                int num4;
                int num5;
                int num6;
                if (!this.CheckHKey())
                {
                    throw new ObjectDisposedException("The RegistryKey being manipulated is closed (closed keys cannot be accessed).");
                }
                char[] lpClass = new char[0x100];
                int length = lpClass.Length;
                if (IDENTEC.NativeMethods.FullFramework)
                {
                    if (RegQueryInfoKeyPC(this.m_handle, lpClass, ref length, 0, out num, out num3, out num4, out num2, out num5, out num6, 0, 0) != 0)
                    {
                        throw new ExternalException("Error retrieving registry properties");
                    }
                    return num2;
                }
                if (RegQueryInfoKey(this.m_handle, lpClass, ref length, 0, out num, out num3, out num4, out num2, out num5, out num6, 0, 0) != 0)
                {
                    throw new ExternalException("Error retrieving registry properties");
                }
                return num2;
            }
        }

        private enum KeyDisposition
        {
            CreatedNewKey = 1,
            OpenedExistingKey = 2
        }

        [Flags]
        private enum RegOptions
        {
            NonVolatile,
            Volatile
        }
    }
}

