using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebDisk.Database.BaseModels;

namespace WebDisk.Database.DatabaseModel
{
    public class Directory : SpaceDataBase
    {
        public Directory()
        {
            DirectoryId = new Guid();
        }
        [Key]
        public Guid DirectoryId { get; set; }

        [NotMapped]
        public new FieldType Type { get; } = FieldType.Directory;

        [Required]
        [ForeignKey("Space")]
        public Guid SpaceId { get; set; }
        public virtual Space Space { get; set; }

        [ForeignKey("ModyfiedBy")]
        public Guid? LastModifiedById { get; set; }
        public virtual ApplicationUser ModyfiedBy { get; set; }




    }
}
