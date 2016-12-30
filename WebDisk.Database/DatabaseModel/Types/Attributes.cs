using System;
using System.ComponentModel.DataAnnotations;

namespace WebDisk.Database.DatabaseModel.Types
{
    [Flags]
    public enum Attributes
    {
        [Display(Name ="Brak")]
        Empty = 0,
        [Display(Name ="Do odczytu")]
        ReadOnly = 1,
        [Display(Name ="Ukryty")]
        Hidden = 2,
        [Display(Name ="System")]
        System = 4,
        [Display(Name ="Wyłączony")]
        Disabled = 8,
        [Display(Name ="Zablokowany")]
        Locked = 16,
        [Display(Name ="Współdzielony")]
        Shared = 32


    }
}
