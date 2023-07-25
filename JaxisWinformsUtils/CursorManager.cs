using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// Manages the Cursor.Current member by changing the current cursor to the one specified
    /// and then restoring it to the original state in the Dispose method. 
    /// 
    /// This class should be constructed/used within a using statement like this:
    /// 
    /// using (CursorManager.Create( )) // defaults to Cursors.WaitCursor
    ///     {
    ///         do some stuff
    ///     }
    ///     
    /// or
    /// 
    /// using (CursorManager.Create( Cursors.SOME_PARTICULAR_CURSOR))
    ///     {
    ///         do some stuff
    ///     }
    /// </summary>
    public class CursorManager : IDisposable
    {
        private Cursor _originalCursor;

        /// <summary>
        /// Creates a CursorManager instance to manage the Cursor.Current static property. The value of
        /// Cursor.Current is saved, then set to the changeToCursor, and then restored in the Dispose.
        /// </summary>
        /// <param name="changeToCursor"></param>
        public static CursorManager Create( Cursor changeToCursor )
        {
            return new CursorManager( changeToCursor );
        }

        public static CursorManager Create( ) 
        {
            return CursorManager.Create( Cursors.WaitCursor );
        }

        private CursorManager( Cursor changeToCursor )
        {
            _originalCursor = Cursor.Current;
            Cursor.Current = changeToCursor;
        }

        public void Dispose( )
        {
            Cursor.Current = _originalCursor;
        }
    }
}
