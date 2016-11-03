using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Extensions;
using WebDisk.BusinessLogic.Interfaces;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Services
{
    public class SpaceService : ISpaceService
    {
        private WebDiskDbContext _context = new WebDiskDbContext();
        private Repository<Space> _spaceRepository;

        public SpaceService()
        {
            _spaceRepository = new Repository<Space>(_context);
        }

        public SpaceService(Repository<Space> spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        public Space GetSpace(Guid userId)
        {
            var currentUserSpaces = _spaceRepository
                                        .Get(n => n.SpaceId == userId, null, string.Empty);

            if (currentUserSpaces == null || currentUserSpaces.Count() == 0)
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


        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
