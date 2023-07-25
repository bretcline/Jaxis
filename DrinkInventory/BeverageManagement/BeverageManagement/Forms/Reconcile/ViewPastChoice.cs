using System;

namespace BeverageManagement.Forms.Reconcile
{
    public class ViewPastChoice
    {
        public TimeSpan TimeSpan { get; set; }
        public string Display { get; set; }

        public override string ToString()
        {
            return Display;
        }
    }
}
