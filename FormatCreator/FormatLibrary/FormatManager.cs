using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LFI.RFID.Format;
using System.Xml;
using System.Text;

namespace LFI.RFID.Editor
{
    public class FormatManager
    {
        public FormatManager( string _FormatPath )
        {
            this.formatFolder = _FormatPath;
        }

        #region Public Methods and Properties

        public void Refresh()
        {
            formats = null;
        }

        public IEnumerable<FormatDef> GetAvailableFormats()
        {
            return Formats.Values;
        }

        public bool IsKnownFormat(Guid formatID)
        {
            return formats.ContainsKey(formatID);
        }

        public FormatDef GetFormatByID(Guid formatID)
        {
            if (Formats.ContainsKey(formatID))
                return Formats[formatID];
            else
                return null;
        }

        public bool SaveFormat(FormatDef formatDef)
        {
            try
            {
                if (!Formats.ContainsKey(formatDef.ID))
                    Formats.Add(formatDef.ID, formatDef);

                string path = GetFilePath(formatDef.ID);
                FileStream fileStream = new FileStream(path, FileMode.Create);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(fileStream, Encoding.UTF8);
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlTextWriter.Indentation = 2;
                XmlSerializer xs = new XmlSerializer(typeof(FormatDef));
                xs.Serialize(xmlTextWriter, formatDef);
                xmlTextWriter.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetFormatXML(FormatDef formatDef)
        {
            try
            {
                StringBuilder xml = new StringBuilder();
                StringWriter writer = new StringWriter(xml);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(writer);
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlTextWriter.Indentation = 2;
                XmlSerializer xs = new XmlSerializer(typeof(FormatDef));
                xs.Serialize(xmlTextWriter, formatDef);
                xmlTextWriter.Close();

                return xml.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool DeleteFormat(Guid formatID)
        {
            try
            {
                if (Formats.ContainsKey(formatID))
                    Formats.Remove(formatID);

                string path = GetFilePath(formatID);
                File.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Private Members

        private Dictionary<Guid, FormatDef> Formats
        {
            get
            {
                if (formats == null)
                {
                    string[] fileNames = Directory.GetFiles(formatFolder, "*.xml");
                    if (fileNames.Length == 0)
                        return null;

                    formats = new Dictionary<Guid, FormatDef>(fileNames.Length);
                    foreach (string fileName in fileNames)
                    {
                        try
                        {
                            FormatDef formatDef = LoadFormatFromFile(fileName);
                            if (!formats.ContainsKey(formatDef.ID))
                                formats.Add(formatDef.ID, formatDef);
                            else
                                System.Windows.Forms.MessageBox.Show(string.Format("Unable to load the {0} format because another format exists with an identical ID: {1}.", formatDef.Name, formatDef.ID.ToString("B")),
                                    "Format File Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None, 
                                    System.Windows.Forms.MessageBoxDefaultButton.Button1);
                        }
                        catch { }
                    }
                }

                return formats;
            }
        }

        private FormatDef CreateTestFormat()
        {
            FormatDef format = new FormatDef();
            format.ID = new Guid();
            format.Name = "Test";
            format.Description = "XXX";
            format.HeaderRowDef.ElementDefs.Add(new DataElementDef("HA-Text", DataType.Text, false, string.Empty));
            format.HeaderRowDef.ElementDefs.Add(new DataElementDef("HB-Number", DataType.Double, true, string.Empty));
            format.HeaderRowDef.ElementDefs.Add(new DataElementDef("HC-Date", DataType.DateOnly, false, string.Empty));
            format.HeaderRowDef.ElementDefs.Add(new DataElementDef("HD-PickList", DataType.PickList, false, "A|B|C|D"));
            format.MaxDataRows = 4;
            format.DataRowDef.ElementDefs.Add(new DataElementDef("DA-Text", DataType.Text, true, string.Empty));
            format.DataRowDef.ElementDefs.Add(new DataElementDef("DB-Number", DataType.Double, false, string.Empty));
            format.DataRowDef.ElementDefs.Add(new DataElementDef("DC-Date", DataType.DateOnly, false, string.Empty));
            format.DataRowDef.ElementDefs.Add(new DataElementDef("DD-PickList", DataType.PickList, false, "E|F|G|H"));
            return format;
        }

        private string GetFilePath(Guid formatID)
        {
            string fileName = formatID.ToString() + ".xml";
            return Path.Combine(formatFolder, fileName);
        }
        
        private FormatDef LoadFormatFromFile(string fileName)
        {
            try
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Open);
                XmlSerializer xs = new XmlSerializer(typeof(FormatDef));
                FormatDef formatDef = (FormatDef)xs.Deserialize(fileStream);
                formatDef.AcceptChanges();                
                fileStream.Close();
        
                return formatDef;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        private string formatFolder;
        private Dictionary<Guid, FormatDef> formats = null;
    }
}
