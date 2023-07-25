using System;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public class ServiceException : Exception
    {
        public ServiceException(string _message) : base(_message)
        {
        }
    }
}
