using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Database.BaseModels;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Interfaces
{
    public interface IDirectoryService
    {
        IEnumerable<Field> GetAvailableFields(Guid userID);
        IEnumerable<Field> GetSharedFields(Guid userID);
        IEnumerable<Field> GetAvailableFields(Guid userId, Guid directoryId);
        void CreateDirectory(Guid userId, Guid directoryId, string name);
        void CreateField(Guid userId, Guid directoryId, FileViewModel fileViewModel);

        void Delete(Guid userId, Guid fieldId);

    }
}
