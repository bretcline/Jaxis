using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.MessageLibrary.POS;

namespace Jaxis.Readers.POS.Parsers
{
    public interface IParser
    {
        ITicket ParseData( string _Data );
        void ClearCache();
    }
}
