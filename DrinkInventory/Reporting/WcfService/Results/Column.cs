using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class Column
    {
        public Column() { }

        public Column(IColumn _column)
        {
            DisplayName = _column.DisplayName;
        }

        [DataMember]
        public string DisplayName { get; set; }
    }
}
