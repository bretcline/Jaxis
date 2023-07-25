namespace LFI.Sync.Shared
{
    /// <summary>
    /// Items that can be logically OR'ed together to sync type
    /// </summary>
    public class SyncType
    {
        public static int None = 1;
        public static int Download = 2;
        public static int Upload = 4;

        // Process which occurs immediately before any Uploads are processed.
        // Allowing for deleted items to be removed, in the instance they are
        // no longer present on the server.
        public static int Resolve = 8;
        public static int RefData = 16;
    }
}