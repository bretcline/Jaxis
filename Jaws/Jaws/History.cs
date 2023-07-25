using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ZStarDevice;

namespace Jaws
{
    class History
    {
        public BindingList<HistoryItem> GetHistory()
        {
            var rc = new BindingList<HistoryItem>();

            DirectoryInfo di = new DirectoryInfo(".");
            var directories = di.GetFiles("*.", SearchOption.AllDirectories);

            foreach (FileInfo d in directories)
            {
                var Item = new HistoryItem() { Name = d.Name, Date = d.LastWriteTime };
                rc.Add(Item);
            }

            return rc;
        }

        public void Save(BindingList<DataRead> List, string FileName)
        {
            //create a backup
            string backupName = Path.ChangeExtension(FileName, ".old");
            if (File.Exists(FileName))
            {
                if (File.Exists(backupName))
                    File.Delete(backupName);
                File.Move(FileName, backupName);
            }

            using (FileStream fs = new FileStream(FileName, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(BindingList<DataRead>));
                ser.Serialize(fs, List);
                fs.Flush();
                fs.Close();
            }
        }
    
        public BindingList<DataRead> Load(string FileName)
        {
            if (!File.Exists(FileName))
                throw new FileNotFoundException("The inventory file could not be found", FileName);

            BindingList<DataRead> result;

            using (FileStream fs = new FileStream(FileName, FileMode.Open))
            {
                XmlSerializer ser = new XmlSerializer(typeof(BindingList<DataRead>));
                result = (BindingList<DataRead>)ser.Deserialize(fs);
            }
            return result;
        }
    }

    class HistoryItem
    {
        public String Name { get; set; }
        public DateTime Date { get; set; }
    }
}
