using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Interfaces.Tags
{
    [Flags]
    public enum ButtonTypes
    {
        Primary,
        Secondary,
        Panic,
    }

    public interface IButtonPress : ITagRead
    {
        ButtonTypes ButtonType { get; }
    }
}