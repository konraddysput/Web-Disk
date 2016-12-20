using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Guid FieldId { get; set; }

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

        [ForeignKey("FieldId")]
        public virtual Field Field { get; set; }
    }
}
