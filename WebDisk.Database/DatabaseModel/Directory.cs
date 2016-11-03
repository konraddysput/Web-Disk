using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebDisk.Database.BaseModels;
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.Database.DatabaseModel
{
    public class Directory : FieldBase
    {
        public Directory()
        {
            FieldId = new Guid();
        }

        [NotMapped]
        public new FieldType Type { get; private set; } = FieldType.Directory;

        public bool IsDirectLinkEnable { get; set; }
        public string DirectLink { get; set; }

        public virtual ICollection<DirectoryShareInformation> SharedInformations { get; set; }
    }
}
