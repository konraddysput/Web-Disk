using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDisk.Database.DatabaseModel.Types
{
    [Flags]
    public enum ShareType
    {
        OnlyCatalog,
        OnlyFile,
        AllFilesInCatalog,
        FullAccess
    }
}
