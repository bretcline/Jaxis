using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    public class GetViewResult : SuccessResult
    {
        public GetViewResult() { }

        public GetViewResult(IView _view)
        {
            View = new View(_view);
        }

        [DataMember]
        public View View { get; set; }

    }
}
