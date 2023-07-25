using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Interfaces
{
    public interface IReconcileAlert : IAlertMessage
    {
        string ObjectID { get; set; }
    }
}
