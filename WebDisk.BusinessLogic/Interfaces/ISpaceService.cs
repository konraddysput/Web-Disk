using System;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Interfaces
{
    public interface ISpaceService
    {
        Space GetSpace(Guid userId);
        void Create(Guid usedId);
        void ShareSpace(Guid spaceId, Guid userId);
        void Delete(Guid spaceId);
    }
}
