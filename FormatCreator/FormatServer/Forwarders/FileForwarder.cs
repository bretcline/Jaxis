using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LFI.RFID.Format;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace LFI.RFID.FormatServer
{
    public class FileForwarder : IDataForwarder
    {
        public FileForwarder()
        {
            outputFolder = @"App_Data\Received TagData";
        }

        public void Forward(TagData data)
        {
            string dataText = TagDataConverter.TagDataToString(data);
            if (string.IsNullOrEmpty(dataText))
                return;

            Guid fileGuid = Guid.NewGuid();
            string fileName = fileGuid.ToString() + ".xml";
            string path = Path.Combine(outputFolder, fileName);

            FileStream fileStream = new FileStream(path, FileMode.Create);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.Write(dataText);
            writer.Flush();
            writer.Close();
        }

        private string outputFolder;
    }    
}
