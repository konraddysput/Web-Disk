using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
