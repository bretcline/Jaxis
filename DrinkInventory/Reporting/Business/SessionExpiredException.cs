using System;

namespace Jaxis.DrinkInventory.Reporting.Business
{
    public class SessionExpiredException : Exception
    {
        public SessionExpiredException() : base("The session has expired.")
        {
            
        }
    }
}
