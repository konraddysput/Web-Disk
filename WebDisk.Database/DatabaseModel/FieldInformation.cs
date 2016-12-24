using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.Database.DatabaseModel
{
    public class FieldInformation
    {
        [Key, ForeignKey("Field")]
        public Guid FieldId { get; set; }
        public Field Field { get; set; }
        public double Size { get; set; }
        //{
        //    get
        //    {
        //        if (Field.Type == FieldType.File)
        //        {
        //            return Size;
        //        }
        //        else
        //        {
        //            throw new NotSupportedException("Field that isnt file cannot have size");
        //        }
        //    }
        //    set
        //    {
        //        Size = value;
        //    }
        //}


        [ForeignKey("Blob")]
        public Guid BlobId { get; set; }
        public virtual Blob Blob { get; set; }
    }
}
