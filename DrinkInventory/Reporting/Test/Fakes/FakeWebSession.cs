using System.Collections.Generic;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;

namespace Jaxis.DrinkInventory.Reporting.Test.Fakes
{
    class FakeWebSession : ISession
    {
        private readonly Dictionary<string, object> m_values;
 
        public FakeWebSession()
        {
            m_values = new Dictionary<string, object>();
        }

        public object this[string _key]
        {
            get { return m_values.ContainsKey(_key) ? m_values[_key] : null; }
            set { m_values[_key] = value; }
        }
    }
}
