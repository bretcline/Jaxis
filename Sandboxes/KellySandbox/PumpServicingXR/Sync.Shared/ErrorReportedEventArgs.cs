using System;

namespace LFI.Sync.Shared
{
    public class ErrorReportedEventArgs : EventArgs
    {
        /// <summary>
        /// Occurs when an error is reported.
        /// </summary>
        public ErrorReportedEventArgs( string errorMessage )
        {
            Error = errorMessage;
        }

        public string Error { get; private set; }
    }
}
