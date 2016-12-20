using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDisk.Database.DatabaseModel
{
    public class Space
    {
      
        [Key, ForeignKey("Owner")]
        public Guid SpaceId { get; set; }

        [Required]
        [ForeignKey("Directory")]
        public Guid DefaultDirectoryId { get; set; }

        [Required]
        public bool IsEnabled { get; set; }
        public virtual Field Directory { get; set; }
      
        public virtual ApplicationUser Owner { get; set; }
    }
}
