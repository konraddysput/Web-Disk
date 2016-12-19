using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDisk.Web.Models.Login
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Nazwa użytkownika jest wymagana")]
        [Display(Name ="Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Hasło jest wymagane")]
        [StringLength(100, ErrorMessage = "Hasło musi składać się z conajmniej 6 znaków oraz maksymalnie 100.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie zgadzają się")]
        public string ConfirmPassword { get; set; }
    }
}