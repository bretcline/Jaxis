using System;
using System.Collections.Generic;
using System.Text;

namespace IDENTEC.UdbElements
{
    public abstract class UDBElement
    {
        public abstract int getLength();
        public abstract byte[] getData();

        public  const String ELEMENT_ASSET_ID = "Asset ID";
        public  const String ELEMENT_ROUTING_CODE = "Routing Code";
        public  const String ELEMENT_USER_ID = "User ID";
        public  const String ELEMENT_OPTIONAL_COMMAND_LIST = "Optional Command List";
        public  const String ELEMENT_MEMORY_SIZE = "Memory Size";
        public  const String ELEMENT_TABLE_QUERY_SIZE = "Table Query Size";
        public  const String ELEMENT_TABLE_QUERY_RESULTS = "Table Query Results";
        public  const String ELEMENT_HARDWARE_FAULT_STATUS = "Hardware Fault Status";
        public  const String ELEMENT_APPLICATION_EXTENSIONS = "Application Extensions";
        public  const String ELEMENT_APPLICATION_EXTENSION_BLOCK = "Application Extension Block";
    }
}
