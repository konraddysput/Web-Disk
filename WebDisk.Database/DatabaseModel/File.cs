using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDisk.Database.DatabaseModel
{
    public class File : SpaceDataBase
    {
        [Key]
        public Guid FileId { get; set; }
        public string FileName { get; set; }

        [ForeignKey("Blob")]
        public int BlobStartId { get; set; }
        public Blob Blob { get; set; }



        public double Size { get; set; }


        [ForeignKey("ApplicationUser")]
        public int OwnerId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
