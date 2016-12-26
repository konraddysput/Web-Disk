using System;
using System.Collections.Generic;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Extensions
{
    public static class DirectoryExtensions
    {
        public static IEnumerable<Field> GetFields(this Repository<Field> source, Guid directoryId)
        {
            return source.Get(n => n.ParentDirectoryId == directoryId);
        }

        public static Field GetFieldRoot(this Repository<Field> source, Guid fieldId)
        {
            if(source.GetByID(fieldId) == null)
            {
                throw new ArgumentException($"There is no root folder for expected field Id:{fieldId}");
            }
            return source.GetByID(fieldId).ParentDirectoryId.HasValue
                                ? GetFieldRoot(source, source.GetByID(fieldId).ParentDirectoryId.Value)
                                : source.GetByID(fieldId);
        }
    }
}
