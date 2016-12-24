using System.ComponentModel.DataAnnotations;
using System.IO;

namespace WebDisk.BusinessLogic.ViewModels
{
    public class FileViewModel
    {
        [Required]
        public Stream InputStream { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "FileName is required")]
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public double ContentLength { get; set; }

    }
}
