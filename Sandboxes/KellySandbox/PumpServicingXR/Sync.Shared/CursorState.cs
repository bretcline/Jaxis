#region

using System.Threading;
using System.Windows.Forms;

#endregion

namespace LFI.Sync.Shared
{
    /// <summary>
    /// Class that provides as consistent mechanism for storing a cursor state and then setting it back
    /// to the default state after a waiting period has passed.
    /// </summary>
    public static class CursorState
    {
        #region Fields

        private static readonly object _syncLock = new object();
        private static Cursor _prevCursor;
        private static int _refCount;

        #endregion

        /// <summary>
        /// Begins the wait.
        /// </summary>
        public static void BeginWait()
        {
            lock (_syncLock)
            {
                // only store the 'state' of the cursor the very first time
                if (_refCount == 0)
                {
                    _prevCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                }

                // Increment the semaphore
                Interlocked.Increment(ref _refCount);
            }
        }

        /// <summary>
        /// Ends the wait.
        /// </summary>
        public static void EndWait()
        {
            lock (_syncLock)
            {
                Interlocked.Decrement(ref _refCount);

                if (_refCount > 0)
                {
                    return;
                }

                if (Cursor.Current == Cursors.WaitCursor)
                {
                    Cursor.Current = _prevCursor ?? Cursors.Default;
                }
            }
        }
    }
}