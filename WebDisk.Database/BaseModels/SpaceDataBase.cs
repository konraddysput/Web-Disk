using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDisk.Database.BaseModels
{
    public class SpaceDataBase
    {
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime LastBackupDate { get; set; }
        public bool Locked { get; set; }
        public bool Disabled { get; set; }
    }
}
