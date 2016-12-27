using System;
using System.Linq;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Extensions
{
    public static class UserExtensions
    {
        public static bool IsSharedOrUserRootDirectory(this Repository<ApplicationUser> source, Guid userId, Guid fieldId, Guid sharedFieldId)
        {
            return source
                    .Get(n => n.Id == userId && 
                            (n.SharedFields.Any(g => g.FieldId == sharedFieldId) || n.Space.DefaultDirectoryId == fieldId))
                    .Any();
        }
    }
}
