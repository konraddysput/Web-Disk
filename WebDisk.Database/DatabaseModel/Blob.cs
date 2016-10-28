using System;
using System.ComponentModel.DataAnnotations;

namespace WebDisk.Database.DatabaseModel
{
    public class Blob
    {
        public Blob()
        {
            BlobId = new Guid();
        }
        [Key]
        public Guid BlobId { get; set; }
        public byte[] Data { get; set; }
    }
}