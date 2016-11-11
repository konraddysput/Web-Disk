using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Extensions
{
    public static class UserExtensions
    {
        public static bool IsSharedOrUserRootDirectory(this Repository<ApplicationUser> source, Guid userId, Guid fieldId)
        {
            return source
                    .Get(n => n.Id == userId && 
                            (n.SharedFields.Any(g => g.FieldId == fieldId) || n.Space.DefaultDirectoryId == fieldId))
                    .Any();
        }
    }
}
