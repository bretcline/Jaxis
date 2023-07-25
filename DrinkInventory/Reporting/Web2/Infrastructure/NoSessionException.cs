using System;

namespace Jaxis.DrinkInventory.Reporting.Web2.Infrastructure
{
    public class NoSessionException : Exception
    {
        internal NoSessionException() : base("No user session")
        {
        }
    }
}
