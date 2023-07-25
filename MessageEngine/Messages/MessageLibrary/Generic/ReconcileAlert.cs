using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;

namespace Jaxis.MessageLibrary
{
    public class ReconcileAlert : AlertNotification, IReconcileAlert
    {
        public string ObjectID { get; set; }
    }
}
