using System;

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
