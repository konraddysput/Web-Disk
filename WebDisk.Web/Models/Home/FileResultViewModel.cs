using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDisk.Web.Models.Home
{
    public class FileResultViewModel
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}