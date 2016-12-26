using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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

        public Repository<Field> FieldRepository
        {
            get
            {
                if (_fieldRepository == null)
                {
                    _fieldRepository = new Repository<Field>(_context);
                }
                return _fieldRepository;
            }
        }

        public Repository<FieldShareInformation> SharedInformationRepository
        {
            get
            {
                if (_sharedInformationRepository == null)
                {
                    _sharedInformationRepository = new Repository<FieldShareInformation>(_context);
                }
                return _sharedInformationRepository;
            }
        }


        public DirectoryService(WebDiskDbContext context) : base(context)
        {
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
            return FieldRepository.GetByID(fieldId);
        }

        /// <summary>
        /// Get fields for default user directory
        /// </summary>
        /// <param name="userId">current logged user Id</param>
        /// <returns></returns>

        public IEnumerable<Field> GetAvailableFields(Guid userId)
        {

            var defaultDirectoryId = SpaceRepository
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
            return FieldRepository.GetFields(fieldId);
        }
        /// <summary>
        /// Returns the root directory of current logged user
        /// </summary>
        /// <param name="userId">current logged user Id</param>
        /// <returns>Root directory folder - Field</returns>
        public Field GetRootField(Guid userId)
        {
            return SpaceRepository.Get(n => n.Owner.Id == userId)
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
            FieldRepository
                   .Insert(new Field()
                   {
                       ParentDirectoryId = fieldId,
                       LastModifiedDate = DateTime.Now,
                       Extension = string.Empty,
                       Type = FieldType.Directory,
                       FieldInformation = null,
                       Name = name
                   });
        }


    }
}
