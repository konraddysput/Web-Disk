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
        [Key]
        public int SpaceId { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastLoginIp { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }        
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<SpaceShare> Shares { get; set; }
    }
}
