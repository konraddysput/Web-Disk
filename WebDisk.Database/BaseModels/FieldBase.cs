using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebDisk.Database.DatabaseModel;
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.Database.BaseModels
{
    public class FieldBase
    {
        [Key]
        public Guid FieldId { get; set; }

        [Required]
        public string Name { get; set; }
        public FieldType Type { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? LastBackupDate { get; set; }

        [Required]
        public bool Locked { get; set; }
        [Required]
        public bool Disabled { get; set; }
        [Required]
        public bool Hidden { get; set; }

        [ForeignKey("ParentDirectory")]
        public Guid? ParentDirectoryId { get; set; }
        public Directory ParentDirectory { get; set; }

        [ForeignKey("ModyfiedBy")]
        public Guid? LastModifiedById { get; set; }
        public virtual ApplicationUser ModyfiedBy { get; set; }
    }
}
