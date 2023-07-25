namespace Jaxis.DrinkInventory.Reporting.Common
{
    public static class Services
    {
        private static IClock m_clock;

        public static IClock Clock
        {
            get { return m_clock ?? (m_clock = new Clock()); }
            set { m_clock = value; }
        }
    }
}
