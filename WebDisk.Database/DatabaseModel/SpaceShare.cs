using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDisk.Database.DatabaseModel
{
    public class SpaceShare
    {
        [Key]
        public Guid ShareId { get; set; }

        public DateTime SharedDate { get; set; }
        public ShareType Type { get; set; }

        [ForeignKey("Space")]
        public int SpaceId { get; set; }
        [ForeignKey("SharedUser")]
        public int SharedUserId { get; set; }


        public virtual Space Space { get; set; }
        public virtual ApplicationUser SharedUser { get; set; }
    }
}
