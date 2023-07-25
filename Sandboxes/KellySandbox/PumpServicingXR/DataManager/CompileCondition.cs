using System;

namespace LFI.Sync.DataManager
{
    [Flags]
    public enum CompileCondition
    {
        None = 1,
        NoOrderBy = 2,
        Defaults = 4 | None,
        CountOnly = 8 | PrimaryKeyOnly | NoOrderBy,
        PrimaryKeyOnly = 16,
        NoJoins = 32,
        NoWhere = 64
    }
}