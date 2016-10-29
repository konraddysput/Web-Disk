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
        private Repository<SpaceShare> _sharedSpaceRepository;

        public SpaceService()
        {
            _spaceRepository = new Repository<Space>(_context);
            _sharedSpaceRepository = new Repository<SpaceShare>(_context);
        }

        public SpaceService(Repository<Space> spaceRepository, Repository<SpaceShare> shareSpaceRepository)
        {
            _spaceRepository = spaceRepository;
            _sharedSpaceRepository = shareSpaceRepository;
        }

        public IEnumerable<SpaceBusinessLogicViewModel> GetSpaces(Guid userId)
        {
            var currentUserSpaces = _spaceRepository
                                        .Get(n => n.OwnerId == userId)
                                        .ConvertSpace();
            if (currentUserSpaces == null)
            {
                throw new ArgumentException($"user with id={userId} does not exists");
            }
            return currentUserSpaces
                    .Concat(_sharedSpaceRepository
                        .Get(n => n.SharedUserId == userId)
                        .ConvertSpace());
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


        //public IEnumerable<SpaceDataBase> GetFields(Guid? directoryId)
        //{

        //}

        //public Repository<Directory> DirectoryRepository
        //{
        //    get
        //    {

        //        if (_directoryRepository == null)
        //        {
        //            _directoryRepository = new Repository<Directory>(_context);
        //        }
        //        return _directoryRepository;
        //    }
        //}

        //public Repository<File> CourseRepository
        //{
        //    get
        //    {

        //        if (_fileRepository == null)
        //        {
        //            _fileRepository = new Repository<File>(_context);
        //        }
        //        return _fileRepository;
        //    }
        //}

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
