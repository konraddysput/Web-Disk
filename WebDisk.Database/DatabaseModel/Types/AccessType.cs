using System.ComponentModel.DataAnnotations;

namespace WebDisk.Database.DatabaseModel.Types
{
    public enum AccessType
    {
        [Display(Name ="Odczyt")]
        Read,
        [Display(Name ="Edycja")]
        Edit,
        [Display(Name ="Pełny dostęp")]
        Full
    }
}
