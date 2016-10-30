using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.Database.BaseModels;

namespace WebDisk.BusinessLogic.Interfaces
{
    public interface IDirectoryService : IDisposable
    {
        IEnumerable<SpaceDataBase> GetAvailableFields(Guid userID);

    }
}
