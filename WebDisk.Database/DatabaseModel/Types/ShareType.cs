using System;
using System.ComponentModel.DataAnnotations;

namespace WebDisk.Database.DatabaseModel.Types
{
    public enum ShareType
    {
        [Display(Name ="Tylko katalog")]
        OnlyCatalog,
        [Display(Name ="Tylko plik")]
        OnlyFile,
        [Display(Name ="Dostęp do plików w katalogu")]
        AllFilesInCatalog,
        [Display(Name ="Pełen dostęp")]
        FullAccess
    }
}
