using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Aspects;
using WebDisk.BusinessLogic.Common;
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

        //Repositories
        private Repository<Space> _spaceRepository;
        private Repository<Field> _fieldRepository;
        private Repository<ApplicationUser> _userRepository;
        //Authentication Manager
        private AuthenticationManager _authManager;

        public DirectoryService()
        {
            _fieldRepository = new Repository<Field>(_context);
            _spaceRepository = new Repository<Space>(_context);
            _userRepository = new Repository<ApplicationUser>(_context);
            _authManager = new AuthenticationManager(_context);
        }

        public DirectoryService(Repository<Space> spaceRepository, Repository<Field> fileRepository)
        {

            _fieldRepository = fileRepository;
            _spaceRepository = spaceRepository;
        }

        [FieldAccessAspect]
        public IEnumerable<Field> GetAvailableFields(Guid userId)
        {

            var defaultDirectoryId = _spaceRepository
                                        .GetDefaultSpaceDirectory(userId)
                                        .FieldId;

            return _fieldRepository.GetFields(defaultDirectoryId);
        }

        [FieldAccessAspect]
        public IEnumerable<Field> GetAvailableFields(Guid userId, Guid directoryId)
        {
            if (!_authManager.IsUserHasRights(userId, directoryId))
            {
                throw new UnauthorizedAccessException($"user with id {userId} does not have access to field");
            }

            return _fieldRepository.GetFields(directoryId);
        }


        public IEnumerable<Field> GetSharedFields(Guid userId)
        {
            return _userRepository
                     .GetByID(userId)
                     .SharedFields
                     .Select(n=>n.Field);
        }
        public void CreateDirectory(string name)
        {
            throw new NotImplementedException();
        }

        //public void CreateDirectory(string name, Guid parentId, Guid userId)
        //{
        //    if (!_authManager.IsUserHasRights(userId, parentId))
        //    {
        //        throw
        //    }
        //}

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
