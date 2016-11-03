using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Extensions;
using WebDisk.BusinessLogic.Interfaces;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Database.BaseModels;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Services
{
    public class DirectoryService : IDirectoryService
    {
        private WebDiskDbContext _context = new WebDiskDbContext();

        //Spaces
        private Repository<Space> _spaceRepository;

        //Fields
        private Repository<Directory> _directoryRepository;
        private Repository<File> _fileRepository;

        public DirectoryService()
        {
            _directoryRepository = new Repository<Directory>(_context);
            _fileRepository = new Repository<File>(_context);
            _spaceRepository = new Repository<Space>(_context);
        }

        public DirectoryService(Repository<Space> spaceRepository,Repository<Directory> directoryRepository,
                                Repository<File> fileRepository)
        {
            _directoryRepository = directoryRepository;
            _fileRepository = fileRepository;
            _spaceRepository = spaceRepository;
        }


        public IEnumerable<FieldBase> GetAvailableFields(Guid userId)
        {
            //var spaces = GetUserSpaces(userId);
            throw new NotImplementedException();
        }

        public IEnumerable<FieldBase> GetAvailableFields(Guid userId, Guid directoryId)
        {
            //var spaces = GetUserSpaces(userId);
            throw new NotImplementedException();
        }


        public IEnumerable<FieldBase> GetSharedFields(Guid userID)
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

        public void Cretea(Directory directory)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid directoryId)
        {
            throw new NotImplementedException();
        }

        public void Update(Directory directory)
        {
            throw new NotImplementedException();
        }
    }
}
