using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Model = WebDisk.Database.DatabaseModel.Types;
namespace WebDisk.Web.Models.Field
{
    public class FieldDescriptionViewModel
    {
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Rozszerzenie")]
        public string Extension { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Data ostatniej modyfikacji")]
        public DateTime? LastModifiedDate { get; set; }

        public string Attribute { get; set; }

        public double Size { get; set; }

        public IEnumerable<FieldShareViewModel> SharedInformations { get; set; }

    }
}