using System.ComponentModel.DataAnnotations;

namespace WebDisk.Database.DatabaseModel
{
    public class Blob
    {
        [Key]
        public int BlobId { get; set; }
        public int NextId { get; set; }
        public byte[] Data { get; set; }
    }
}