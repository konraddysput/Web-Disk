using System;

namespace WebDisk.Database.DatabaseModel.Types
{
    [Flags]
    public enum Attributes
    {
        Empty = 0,
        ReadOnly = 1,
        Hidden = 2,
        System = 4,
        Disabled = 8,
        Locked = 16,
        Shared = 32


    }
}
