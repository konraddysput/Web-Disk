using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.Database.BaseModels
{
    public class SpaceDataBase
    {
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime LastBackupDate { get; set; }
        public bool Locked { get; set; }
        public bool Disabled { get; set; }
    }
}
