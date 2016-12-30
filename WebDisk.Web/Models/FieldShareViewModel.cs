using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDisk.Web.Models
{
    public class FieldShareViewModel
    {
        public string FileName { get; set; }
        public string SharedUserName { get; set; }
        public DateTime SharedDate { get; set; }
        public TimeSpan SharedTime { get; set; }

        public string ShareType { get; set; }
        public string AccessType { get; set; }


    }
}