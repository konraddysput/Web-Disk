using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.Database.DatabaseModel
{
    public class FieldInformation
    {
        public FieldInformation()
        {
            FieldInformationId = Guid.NewGuid();
        }
        [Key,ForeignKey("Field")]
        public Guid FieldInformationId { get; set; }    
        public double Size { get; set; }

        [Required]
        public string Localisation { get; set; }

        public DateTime? LastBackupDate { get; set; }
        public Field Field { get; set; }
    }
}
