using System;
using System.Collections.Generic;

namespace Jaxis.DrinkInventory.Reporting.Web2.Widgets
{
    public class TopFiveVolumes : IWidget
    {
        public Guid Id
        {
            get { return new Guid("0F18C569-7E50-44FA-B8B5-212998044FBB"); }
        }

        public string Name
        {
            get { return "Top 5 Volumes"; }
        }

        public string ViewName
        {
            get { return "_TopFiveVolumes"; }
        }

        public void UpdateData(Guid _sessionId)
        {
            Values = new List<AmountPerDescription>
            {
                new AmountPerDescription {Amount = 3423.12M, Description = "Chivas 18"},
                new AmountPerDescription {Amount = 2656.98M, Description = "Crown Royal"},
                new AmountPerDescription {Amount = 2398.38M, Description = "Bicardi"},
                new AmountPerDescription {Amount = 2948.01M, Description = "Jose Cuervo"},
                new AmountPerDescription {Amount = 2398.55M, Description = "Don Julio"}
            };
        }

        public List<AmountPerDescription> Values { get; set; }
    }
}
