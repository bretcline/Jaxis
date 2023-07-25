using System;
using System.Collections.Generic;
using Jaxis.Inventory.Data;

namespace BeverageManagement.Forms.Activity.Widgets
{
    public interface IActivityReceiver
    {
        bool AddActivityItem( object _item );
    }

    public interface IActivityControl : IActivityReceiver
    {
        string DisplayName { get; }

        Guid DisplayID { get; }

        int Width { get; set; }

        int Height { get; set; }

        object ControlTag { get; set; }

        List<Type> MessageType { get; }
    }

}