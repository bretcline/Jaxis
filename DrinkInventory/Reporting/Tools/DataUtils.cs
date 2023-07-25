using System.Data;
using System.Xml.Linq;

namespace Jaxis.DrinkInventory.Reporting.Tools
{
    public static class DataUtils
    {
        public static XElement GetXmlSchema(this DataTable _this)
        {
            var document = new XDocument();
            using (var writer = document.CreateWriter())
            {
                _this.WriteXmlSchema(writer);
                writer.Close();
                return document.Root;
            }
        }

        public static XElement GetXmlData(this DataTable _this)
        {
            var document = new XDocument();
            using (var writer = document.CreateWriter())
            {
                _this.WriteXml(writer);
                writer.Close();
                return document.Root;
            }
        }
    }
}
