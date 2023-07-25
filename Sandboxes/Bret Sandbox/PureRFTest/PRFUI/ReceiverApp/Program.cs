namespace ReceiverApp
{
    using PureRF;
    using ReceiverApp.Properties;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    internal static class Program
    {
        public static ReceiverApp.MainWin MainWin;

        public static void ExportListViewToCSV(ListView list, SaveFileDialog dlg)
        {
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(dlg.OpenFile());
                    string[] strArray = new string[list.Columns.Count];
                    int index = 0;
                    while (index < strArray.Length)
                    {
                        strArray[index] = list.Columns[index].Text;
                        index++;
                    }
                    writer.WriteLine(string.Join(",", strArray));
                    for (index = 0; index < list.Items.Count; index++)
                    {
                        strArray = new string[list.Items[index].SubItems.Count];
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            strArray[i] = list.Items[index].SubItems[i].Text;
                        }
                        writer.WriteLine(string.Join(",", strArray));
                    }
                    writer.Close();
                }
                catch
                {
                    MessageBox.Show("Unable to write file", "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        public static void ExportListViewToXML(ListView list, SaveFileDialog dlg)
        {
            XmlDocument document = new XmlDocument();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                document.AppendChild(document.CreateNode(XmlNodeType.XmlDeclaration, "", ""));
                XmlNode newChild = document.CreateElement("PureRF");
                document.AppendChild(newChild);
                for (int i = 0; i < list.Items.Count; i++)
                {
                    XmlElement element = document.CreateElement("Item");
                    element.SetAttribute("ID", Convert.ToString((int) (i + 1)));
                    for (int j = 0; j < list.Items[i].SubItems.Count; j++)
                    {
                        string name = list.Columns[j].Text;
                        if (!(name == "#"))
                        {
                            name = name.Replace(" ", "_").Replace("'", "").Replace("\"", "").Replace("?", "");
                            XmlText text = document.CreateTextNode(list.Items[i].SubItems[j].Text);
                            XmlElement element2 = document.CreateElement(name);
                            element2.AppendChild(text);
                            element.AppendChild(element2);
                        }
                    }
                    newChild.AppendChild(element);
                }
                try
                {
                    document.Save(dlg.OpenFile());
                }
                catch
                {
                    MessageBox.Show("Unable to write file", "Export XML", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MainWin = new ReceiverApp.MainWin());
        }

        public static Color RetVal2Color(ReceiverRetVal RetVal)
        {
            ReceiverApp.Properties.Settings settings = ReceiverApp.Properties.Settings.Default;
            Dictionary<ReceiverRetVal, Color> dictionary = new Dictionary<ReceiverRetVal, Color>();
            dictionary.Add(ReceiverRetVal.ERROR, settings.Color_Error);
            dictionary.Add(ReceiverRetVal.SUCCESS, settings.Color_Success);
            dictionary.Add(ReceiverRetVal.LOOP_TIMEOUT, settings.Color_Error);
            dictionary.Add(ReceiverRetVal.LOOP_COMM_ERROR, settings.Color_Error);
            dictionary.Add(ReceiverRetVal.BAD_CRC, settings.Color_Error);
            dictionary.Add(ReceiverRetVal.BAD_SYNC, settings.Color_Error);
            dictionary.Add(ReceiverRetVal.PROTOCOL_ERROR, settings.Color_Error);
            dictionary.Add(ReceiverRetVal.PACKET_TOO_SMALL, settings.Color_Error);
            dictionary.Add(ReceiverRetVal.BAD_PARAMS, settings.Color_Error);
            dictionary.Add(ReceiverRetVal.NO_BROADCAST, settings.Color_Success);
            dictionary.Add(ReceiverRetVal.GOT_BROADCAST, settings.Color_Success);
            dictionary.Add(ReceiverRetVal.FLASH_OK, settings.Color_Success);
            dictionary.Add(ReceiverRetVal.FLASH_DAMAGED, settings.Color_Error);
            if (dictionary.ContainsKey(RetVal))
            {
                return dictionary[RetVal];
            }
            return settings.Color_Default;
        }

        public static Color TagMsg2Color(Receiver.TagMsg tagMsg)
        {
            ReceiverApp.Properties.Settings settings = ReceiverApp.Properties.Settings.Default;
            Dictionary<Receiver.TagMsg, Color> dictionary = new Dictionary<Receiver.TagMsg, Color>();
            dictionary.Add(Receiver.TagMsg.Tamper, settings.Color_TagMsg_Maintenance);
            dictionary.Add(Receiver.TagMsg.MotionSensor, settings.Color_TagMsg_Movement);
            dictionary.Add(Receiver.TagMsg.ACTIVATOR, settings.Color_TagMsg_Near_Activator);
            dictionary.Add(Receiver.TagMsg.LowBattery, settings.Color_TagMsg_Near_Activator);
            if (dictionary.ContainsKey(tagMsg))
            {
                return dictionary[tagMsg];
            }
            return settings.Color_Default;
        }
    }
}

