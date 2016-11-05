using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.Database.DatabaseModel
{
    public class FieldInformation
    {
        [Key,ForeignKey("Field")]
        public Guid FieldId { get; set; }
        public double Size
        {
            get
            {
                if (Field.Type == FieldType.File)
                {
                    return Size;
                }
                else
                {
                    throw new NotSupportedException("Field that isnt file cannot have size");
                }
            }
            set
            {
                if (Field.Type == FieldType.File)
                {
                    Size = value;
                }
                else
                {
                    throw new NotSupportedException("Field that isnt file cannot have size");
                }
            }
        }


        [ForeignKey("Blob")]
        public Guid BlobId
        {
            get
            {
                return BlobId;
            }
            set
            {
                if (Field.Type != FieldType.File)
                {
                    throw new NotSupportedException("Field that isnt file cannot have data");
                }
            }
        }
        public Blob Blob
        {
            get
            {
                return Blob;
            }
            set
            {
                if (Field.Type != FieldType.File)
                {
                    throw new NotSupportedException("Field that isnt file cannot have data");
                }
            }
        }
        public Field Field { get; set; }

    }
}
