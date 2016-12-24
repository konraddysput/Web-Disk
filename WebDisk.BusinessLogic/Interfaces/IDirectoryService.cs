using System;
using System.Collections.Generic;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Interfaces
{
    public interface IDirectoryService
    {
        IEnumerable<Field> GetAvailableFields(Guid userID);
        IEnumerable<Field> GetSharedFields(Guid userID);
        IEnumerable<Field> GetAvailableFields(Guid userId, Guid directoryId);
        void CreateDirectory(Guid userId, Guid directoryId, string name);

    }
}
