namespace IDENTEC
{
    using System;

    internal sealed class Registry
    {
        public static readonly RegistryKey ClassesRoot = new RegistryKey(0x80000000, "HKEY_CLASSES_ROOT", true, true);
        public static readonly RegistryKey CurrentUser = new RegistryKey(0x80000001, "HKEY_CURRENT_USER", true, true);
        public static readonly RegistryKey LocalMachine = new RegistryKey(0x80000002, "HKEY_LOCAL_MACHINE", true, true);
        public static readonly RegistryKey Users = new RegistryKey(0x80000003, "HKEY_USERS", true, true);

        private Registry()
        {
        }
    }
}

