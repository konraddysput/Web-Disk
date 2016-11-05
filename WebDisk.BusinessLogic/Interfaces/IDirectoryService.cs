using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.Database.BaseModels;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Interfaces
{
    public interface IDirectoryService : IDisposable
    {
        IEnumerable<Field> GetAvailableFields(Guid userID);
        IEnumerable<Field> GetSharedFields(Guid userID);
        IEnumerable<Field> GetAvailableFields(Guid userId, Guid directoryId);
        void CreateDirectory(string name);
        void CreateField();

        void Delete(Guid directoryId);

    }
}
