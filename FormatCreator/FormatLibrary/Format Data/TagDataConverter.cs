using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace LFI.RFID.Format
{
    public static class TagDataConverter
    {
        public static TagData TagDataFromString(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return null;

            try
            {
                StringReader reader = new StringReader(xml);
                XmlSerializer xs = new XmlSerializer(typeof(TagData));
                TagData data = (TagData)xs.Deserialize(reader);
                reader.Close();

                return data;
            }
            catch
            {
                return null;
            }
        }

        public static string TagDataToString(TagData data)
        {
            try
            {
                StringBuilder xml = new StringBuilder();
                StringWriter writer = new StringWriter(xml);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(writer);
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlTextWriter.Indentation = 2;
                XmlSerializer xs = new XmlSerializer(typeof(TagData));
                xs.Serialize(xmlTextWriter, data);
                xmlTextWriter.Close();

                return xml.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
                //return string.Empty;
            }
        }
    }
}
