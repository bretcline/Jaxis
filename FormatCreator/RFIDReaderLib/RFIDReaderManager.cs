using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LFI.RFID.Format;

namespace Jaxis.RFID.Readers
{
    public enum RFIDReaderTypes
    {
        DL990,
        SDReader,
        MockReader,
    }


    public class RFIDReaderManager
    {
        public static IRFIDReader GetReader(RFIDReaderTypes _Type, string _formatDefinitionPath)
        {
            IRFIDReader rc = null;
            switch(_Type)
            {
                case RFIDReaderTypes.DL990:
                {
                    break;
                }
                case RFIDReaderTypes.SDReader:
                {
                    break;
                }
                case RFIDReaderTypes.MockReader:
                {
                    rc = new MockReader();
                    MockConfig config = new MockConfig();
                    config.FormatDefinitionPath = _formatDefinitionPath;
                    rc.ConfigureDevice(config);
                    break;
                }
                default:
                {
                    break;
                }
            }
            return rc;
        }
    }
}
