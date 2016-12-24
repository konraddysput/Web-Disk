using System;
using System.Collections.Generic;
using System.Linq;
using WebDisk.BusinessLogic.Aspects;
using WebDisk.BusinessLogic.Common;
using WebDisk.BusinessLogic.Extensions;
using WebDisk.BusinessLogic.Interfaces;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Database.DatabaseModel;
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.BusinessLogic.Services
{
    /// <summary>
    /// base directory operations
    /// </summary>
    public class DirectoryService : ServiceBase, IDirectoryService
    {
        //Repositories
        private Repository<Space> _spaceRepository;
        private Repository<Field> _fieldRepository;
        private Repository<FieldShareInformation> _sharedInformationRepository;
        public DirectoryService() : base()
        {
            _fieldRepository = new Repository<Field>(_context);
            _spaceRepository = new Repository<Space>(_context);
            _sharedInformationRepository = new Repository<FieldShareInformation>(_context);
        }

        public DirectoryService(Repository<Space> spaceRepository, Repository<Field> fileRepository) : base()
        {
            _fieldRepository = fileRepository;
            _spaceRepository = spaceRepository;
            _sharedInformationRepository = new Repository<FieldShareInformation>(_context);
        }

        /// <summary>
        /// Get specific informations about fields 
        /// </summary>
        /// <param name="userId">current logged user</param>
        /// <param name="fieldId">expected field ID</param>
        /// <returns>Field object</returns>
        [FieldAccess]
        public Field GetFieldDetails(Guid userId, Guid fieldId)
        {
            return _fieldRepository.GetByID(fieldId);
        }

        /// <summary>
        /// Get fields for default user directory
        /// </summary>
        /// <param name="userId">current logged user Id</param>
        /// <returns></returns>

        public IEnumerable<Field> GetAvailableFields(Guid userId)
        {

            var defaultDirectoryId = _spaceRepository
                                        .GetDefaultSpaceDirectory(userId)
                                        .FieldId;

            return GetAvailableFields(userId, defaultDirectoryId);
        }
        /// <summary>
        /// Get fields for specific directory for current logged user 
        /// </summary>
        /// <param name="userId">current logged user Id</param>
        /// <param name="directoryId">root directory of fields that we want to retrive from method</param>
        /// <returns>Fields from root directory</returns>
        [FieldAccess]
        public IEnumerable<Field> GetAvailableFields(Guid userId, Guid fieldId)
        {
            return _fieldRepository.GetFields(fieldId);
        }
        /// <summary>
        /// Returns the root directory of current logged user
        /// </summary>
        /// <param name="userId">current logged user Id</param>
        /// <returns>Root directory folder - Field</returns>
        public Field GetRootField(Guid userId)
        {
            return _spaceRepository.Get(n => n.Owner.Id == userId)
                                    .FirstOrDefault()
                                    .Directory;
        }

        /// <summary>
        /// Get fields that are shared with user
        /// </summary>
        /// <param name="userId">current logged user Id</param>
        /// <returns></returns>
        public IEnumerable<Field> GetSharedFields(Guid userId)
        {
            return new Repository<ApplicationUser>(_context)
                     .GetByID(userId)
                     .SharedFields
                     .Select(n => n.Field);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fieldId"></param>
        /// <param name="name"></param>
        [FieldAccess]
        [AfterDataChange]
        public void CreateDirectory(Guid userId, Guid fieldId, string name)
        {
            _fieldRepository
                    .Insert(new Field()
                    {
                        ParentDirectoryId = fieldId,
                        LastModifiedById = userId,
                        Type = FieldType.Directory,
                        FieldInformation = null,
                        Name = name
                    });
        }


    }
}
