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
        IEnumerable<FieldBase> GetAvailableFields(Guid userID);
        IEnumerable<FieldBase> GetSharedFields(Guid userID);
        IEnumerable<FieldBase> GetAvailableFields(Guid userId, Guid directoryId);
        void Cretea(Directory directory);

        void Delete(Guid directoryId);

        void Update(Directory directory);

    }
}
