using System.ComponentModel;

namespace Jaxis.Interfaces
{
    public enum AlertTypes
    {
        [Description("Missed Pour")]
        MissedPour = 100,
        [Description("Missed Message")]
        MissedMsg = 101,
        [Description("Bad Bottle Attach")]
        BadBottleAttach = 102,
        [Description("Tag Moved")]
        TagMoved = 103,
        [Description("Low Battery")]
        LowBattery = 104,
        [Description("Tag Not Reporting")]
        TagNotReporting = 105,

        [Description("Branded tag not attached")]
        BrandedTagNotAttached = 106,
        [Description("After Hours Activity")]
        AfterHoursActivity = 107,

        [Description("Tag removed from a non empty bottle")]
        NonEmptyBottle = 127,

        [Description("Reconnect")]
        Reconnect = 199,
        [Description("Cannot Connect")]
        CannotConnect = 200,
        [Description("Not Reading Tags")]
        NotReadingTags = 201,

        [Description("Unreconciled Pour")]
        UnreconciledPour = 1000,
        [Description("Unreconciled Ticket Item")]
        UnreconciledTicketItem = 1001,

        [Description("SQL Alert Low Value")]
        SqlLowValue = 2000,
        [Description("SQL Alert High Value")]
        SqlHighValue = 2001,


    }

    public enum AlertClasses
    {
        Devices,
        Tags,
        POSItems,
    }
}