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
        private Repository<SpaceShare> _spaceShareRepository;

        //Fields
        private Repository<Directory> _directoryRepository;
        private Repository<File> _fileRepository;

        public DirectoryService()
        {
            _directoryRepository = new Repository<Directory>(_context);
            _fileRepository = new Repository<File>(_context);
            _spaceRepository = new Repository<Space>(_context);
            _spaceShareRepository = new Repository<SpaceShare>(_context);
        }

        public IEnumerable<SpaceDataBase> GetAvailableFields(Guid userId)
        {
            //var spaces = GetUserSpaces(userId);
            throw new NotImplementedException();
        }

        private IEnumerable<SpaceBusinessLogicViewModel> GetUserSpaces(Guid userId)
        {
            return _spaceRepository.Get(n => n.OwnerId == userId)
                        .ConvertSpace()
                        .Concat(_spaceShareRepository
                                        .Get(n => n.SharedUserId == userId)
                                        .ConvertSpace());
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
