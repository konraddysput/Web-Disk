using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDisk.Database.DatabaseModel
{
    public class Space
    {
        public Space()
        {
            SpaceId= new Guid();
        }
        [Key]
        public Guid SpaceId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public string LastLoginIp { get; set; }
        public string Name { get; set; }

        [Required]
        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }        
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<SpaceShare> Shares { get; set; }
    }
}
