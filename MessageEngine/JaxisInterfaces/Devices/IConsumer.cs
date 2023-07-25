using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Interfaces
{
    public interface IConsumer : IDevice
    {
        string Consume( IMessage _message );
    }
}