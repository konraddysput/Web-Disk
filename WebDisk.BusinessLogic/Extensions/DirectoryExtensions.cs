using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.BaseModels;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Extensions
{
    public static class DirectoryExtensions
    {
        public static IEnumerable<Field> GetFields(this Repository<Field> source, Guid directoryId)
        {
            //return source.Get(n=> { n.ParentDirectoryId == directoryId && n.id)
            throw new NotImplementedException();
        }
    }
}
