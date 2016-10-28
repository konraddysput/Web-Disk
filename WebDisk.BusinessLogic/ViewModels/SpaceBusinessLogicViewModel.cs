using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.ViewModels.Types;

namespace WebDisk.BusinessLogic.ViewModels
{
    public class SpaceBusinessLogicViewModel
    {
        public Guid Id { get; set; }
        public SpaceType Type { get; set; }
        public string Name { get; set; }
    }
}
