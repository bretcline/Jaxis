using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LFI.RFID.Format;

namespace Jaxis.RFID.Readers
{
    public class DL990Reader : IRFIDReader
    {
        public void ConfigureDevice( IRFIDConfig _Config )
        {

        }

        public void WriteTag( TagData _Data )
        {

        }

        public TagData ReadTag( )
        {
            List<byte> Data = new List<byte>( );

            TagData rc = new TagData( );
            return rc;
        }


        public List<TagData> ReadTags( )
        {
            List<TagData> rc = new List<TagData>( );


            return rc;
        }
        

    }
}