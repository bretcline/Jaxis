using System;
using Jaxis.Interfaces.Tags;

namespace Jaxis.MessageLibrary.Generic
{
    public class TagButtonPress : TagRead, IButtonPress
    {
        #region IButtonPress Members

        public ButtonTypes ButtonType { get; set; }

        #endregion IButtonPress Members
    }
}