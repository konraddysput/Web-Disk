using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebDisk.Database.BaseModels;

namespace WebDisk.Database.DatabaseModel
{
    public class File : FieldBase
    {
        public File()
        {
            FieldId = new Guid();
        }
        
        public string Extension { get; set; }

        [ForeignKey("Blob")]
        public Guid BlobId { get; set; }
        public Blob Blob { get; set; }

        public double Size { get; set; }
        public virtual ICollection<FileShareInformation> SharedInformations { get; set; }
    }
}
