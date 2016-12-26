using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.Database.DatabaseModel
{
    public class Field
    {
        public Field()
        {
            FieldId = Guid.NewGuid();
            Attributes = Attributes.Empty;
            CreationDate = DateTime.Now;

        }

        [Key]
        public Guid FieldId { get; set; }

        [Required]
        public string Name { get; set; }
        public FieldType Type { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; private set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? LastBackupDate { get; set; }

        [Required]
        public Attributes Attributes { get; set; }

        public string DirectLink
        {
            get
            {
                if (Attributes.HasFlag(Types.Attributes.Shared))
                {
                    return DirectLink;
                }
                return string.Empty;
            }

            set
            {
                if (Attributes.HasFlag(Types.Attributes.Shared))
                {
                    DirectLink = value;
                }
            }
        }
        public string Extension { get; set; }

        [ForeignKey("ParentDirectory")]
        public Guid? ParentDirectoryId { get; set; }
        public Field ParentDirectory { get; set; }

        [ForeignKey("ModyfiedBy")]
        public Guid? LastModifiedById { get; set; }
        public virtual ApplicationUser ModyfiedBy { get; set; }

        public virtual FieldInformation FieldInformation { get; set; }

        public virtual ICollection<FieldShareInformation> SharedInformations { get; set; }


    }
}
