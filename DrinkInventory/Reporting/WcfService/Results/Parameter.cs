using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class Parameter
    {
        public Parameter()
        {
        }

        public Parameter(IParameter _parameter)
        {
            Type = _parameter.Type;
            Name = _parameter.Name;
            Order = _parameter.Order;
            Label = _parameter.Label;
        }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public string Label { get; set; }
    }
}
