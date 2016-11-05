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
        private Repository<Field> _fieldRepository;

        public DirectoryService()
        {
            _fieldRepository = new Repository<Field>(_context);
            _spaceRepository = new Repository<Space>(_context);
        }

        public DirectoryService(Repository<Space> spaceRepository,Repository<Field> fileRepository)
        {
            
            _fieldRepository = fileRepository;
            _spaceRepository = spaceRepository;
        }


        public IEnumerable<Field> GetAvailableFields(Guid userId)
        {
            //var spaces = GetUserSpaces(userId);
            throw new NotImplementedException();
        }

        public IEnumerable<Field> GetAvailableFields(Guid userId, Guid directoryId)
        {
            //var spaces = GetUserSpaces(userId);
            throw new NotImplementedException();
        }


        public IEnumerable<Field> GetSharedFields(Guid userID)
        {
            throw new NotImplementedException();
        }

        public void CreateDirectory(string name)
        {
            throw new NotImplementedException();
        }

        public void CreateField()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid directoryId)
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
