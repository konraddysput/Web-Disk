using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebDisk.Database.BaseModels;

namespace WebDisk.Database.DatabaseModel
{
    public class File : SpaceDataBase
    {
        public File()
        {
            FileId= new Guid();
        }
        [Key]
        public Guid FileId { get; set; }
        [Required]
        public string Extension { get; set; }

        [ForeignKey("Blob")]
        public Guid BlobStartId { get; set; }
        public Blob Blob { get; set; }

        public double Size { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public Guid OwnerId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
