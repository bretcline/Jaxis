using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebWatcher
{
    [Serializable]
    public class WatcherSettings
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string LoginURL { get; set; }

        public int RefreshInterval { get; set; }

        public int NumberOfWatchers { get; set; }
    }
}