using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace kson
{
    public static class JsonHelper
    {
        public static DataTable JsonToDataTable(string _json, string _nodeListPath)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(_json)))
            {
                var table = new DataTable();
                var reader = JsonReaderWriterFactory.CreateJsonReader(ms, XmlDictionaryReaderQuotas.Max);
                var doc = new XmlDocument();
                doc.Load(reader);
                var nodes = doc.SelectNodes(_nodeListPath);
                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        var children = node.SelectNodes("*");
                        if (children != null)
                        {
                            foreach (XmlNode child in children)
                            {
                                if (!table.Columns.Contains(child.Name))
                                {
                                    table.Columns.Add(child.Name, typeof(string));
                                }
                            }

                            var row = table.NewRow();
                            foreach (XmlNode child in children)
                            {
                                row[child.Name] = child.InnerText;
                            }

                            table.Rows.Add(row);
                        }
                    }
                }

                return table;
            }
        }

        public static string Serialize(object _obj)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(_obj.GetType());
                serializer.WriteObject(stream, _obj);
                var json = Encoding.UTF8.GetString(stream.ToArray(), 0, (int) stream.Length);
                return json;
            }
        }

    }
}
