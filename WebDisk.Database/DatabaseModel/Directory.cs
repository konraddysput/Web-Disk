using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDisk.Database.DatabaseModel
{
    public class Directory : SpaceDataBase
    {
        [Key]
        public int DirectoryId { get; set; }
        public string DirectoryName { get; set; }

        [ForeignKey("Space")]
        public int SpaceId { get; set; }
        public virtual Space Space { get; set; }     

        [ForeignKey("ModyfiedBy")]
        public int LastModifiedById { get; set; }
        public virtual ApplicationUser ModifiedBy { get; set; }

      


    }
}
