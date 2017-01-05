using System;
using System.Linq;
using WebDisk.BusinessLogic.Interfaces;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Services
{
    public class SpaceService : ServiceBase, ISpaceService
    {

        private Repository<Space> _spaceRepository;

        public Repository<Space> SpaceRepository
        {
            get
            {
                if (_spaceRepository == null)
                {
                    _spaceRepository = new Repository<Space>(_context);
                }
                return _spaceRepository;
            }
        }


        public SpaceService(WebDiskDbContext context) : base(context) { }

        public Space GetSpace(Guid userId)
        {
            var currentUserSpaces = SpaceRepository
                                        .Get(n => n.SpaceId == userId, null, string.Empty);

            if (currentUserSpaces == null || currentUserSpaces.Any())
            {
                throw new ArgumentException($"user with id={userId} does not exists");
            }
            return currentUserSpaces.FirstOrDefault();
        }

        public void Create(Guid usedId)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid spaceId)
        {
            throw new NotImplementedException();
        }

        public void ShareSpace(Guid spaceId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
