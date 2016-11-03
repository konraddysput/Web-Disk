using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.Database.BaseModels
{
    public class ShareInformationBase
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime SharedDate { get; set; }

        public TimeSpan SharedTime { get; set; } = new TimeSpan(0);// 0 - infinity

        [Required]
        public ShareType ShareType { get; set; }

        [Required]
        public AccessType AccessType { get; set; }

    }
}
