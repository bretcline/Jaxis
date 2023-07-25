using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Interfaces.Tags
{
    public interface ITag : IMessage
    {
        string TagID { get; }
    }
}