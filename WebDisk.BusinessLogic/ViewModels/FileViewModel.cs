using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDisk.BusinessLogic.ViewModels
{
    public class FileViewModel
    {
        [Required]
        public byte[] Content { get; set; }

        [Required]
        [MinLength(1,ErrorMessage = "File should have name")]
        public string Name { get; set; }
        public string Extensions { get; set; }

    }
}
