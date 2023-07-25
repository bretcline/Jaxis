using System;
using System.Web.Mvc;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public class ServerResult
    {
        public static object SuccessResult(object _data, string _successMessage)
        {
            return new ServerResult {Status = (int)ResultStatus.Success, Message = _successMessage, Data = _data};
        }

        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static ServerResult FailureResult(string _failureMessage)
        {
            return new ServerResult {Status = (int)ResultStatus.Failure, Message = _failureMessage};
        }

        public static ServerResult NoSessionResult(string _redirectUrl)
        {
            return new ServerResult {Status = (int) ResultStatus.NoSession, Message = _redirectUrl};
        }
    }
}
