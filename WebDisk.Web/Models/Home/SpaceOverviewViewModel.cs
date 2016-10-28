using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDisk.BusinessLogic.ViewModels.Types;

namespace WebDisk.Web.Models.Home
{
    public class SpaceOverviewViewModel
    {
        public Guid Id { get; set; }
        public SpaceType Type { get; set; }
        public string Name { get; set; }
    }
}