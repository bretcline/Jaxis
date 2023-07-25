using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LFI.RFID.Format;

namespace Jaxis.RFID.Readers
{
    public interface IRFIDReader
    {
        void ConfigureDevice( IRFIDConfig _Config );
        void WriteTag( TagData _Data );
        TagData ReadTag( );
        // List<TagData> ReadTags( );
    }
}
