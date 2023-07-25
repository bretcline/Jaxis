using System;
using System.Collections.Generic;
using System.Text;

namespace LFI.Mobile.Controls
{
	public enum ScreenResults
	{
		None,
		OK,
		Cancel
	}

    public sealed class ScreenResult
    {
		internal ScreenResults Result { get; set; }
        internal string ScreenTag { get; set; }

        internal void Clear()
        {
			Result = ScreenResults.None;
            ScreenTag = String.Empty;
        }

        internal bool HasValue()
        {
        	return Result != ScreenResults.None;
        }
    }
}
