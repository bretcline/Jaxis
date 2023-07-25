using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Interfaces
{
    public interface IProducer : IDevice
    {
        string ProduceMessage( IMessage _Message );

        Func<IMessage, string> Produce { set; }
    }
}