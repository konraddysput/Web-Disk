using System;
using System.Linq;
using WebDisk.Database.DatabaseModel.Types;
using DbModel = WebDisk.Database.DatabaseModel.Types;
namespace WebDisk.Web.Models.Home
{
    public class FieldViewModel
    {
        public Guid FieldId { get; set; }
        public string Name { get; set; }
        public DbModel.Attributes Attributes { get; set; }
        public string Extension { get; set; }
        public FieldType Type { get; set; }
        public char AttributeShortcut
        {
            get
            {
                return Attributes.ToString().ToUpper().First();
            }
        }
    }
}