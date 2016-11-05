using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.Database.BaseModels;
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.Database.DatabaseModel
{
    public class FieldShareInformation
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid FileId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime SharedDate { get; set; }

        public TimeSpan SharedTime { get; set; } = new TimeSpan(0);// 0 - infinity

        [Required]
        public ShareType ShareType { get; set; }

        [Required]
        public AccessType AccessType { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("FileId")]
        public virtual Field File { get; set; }
    }
}
