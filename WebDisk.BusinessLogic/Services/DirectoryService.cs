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
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.BusinessLogic.Services
{
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


        /// <summary>
        /// Create a new file in existing directory
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <param name="fieldId">directory where file should exists</param>
        /// <param name="fileViewModel">information about file - bytes, name, extensions</param>
        [AfterDataChange]
        [FieldAccess]
        public void CreateField(Guid userId, Guid fieldId, FileViewModel fileViewModel)
        {
            //TODO 
            //UPLOAD FILE TO AZURE
            string pathToAzureFile = AzureManager.UploadFile(fileViewModel.Content);
            _fieldRepository
                .Insert(new Field()
                {
                    ParentDirectoryId = fieldId,
                    Name = fileViewModel.Name,
                    Extension = fileViewModel.Extensions,
                    Type = FieldType.File,
                    LastModifiedById = userId,
                    FieldInformation = new FieldInformation()
                    {
                        Size = fileViewModel.Content.Length,
                        Blob = new Blob()
                        {
                            Localisation = pathToAzureFile
                        }
                    }
                });
        }
        /// <summary>
        /// Deleting existing file or directory
        /// </summary>
        /// <param name="userId">current logged user id</param>
        /// <param name="fieldId">field Id </param>
        [FieldAccess]
        [AfterDataChange]
        public void Delete(Guid userId, Guid fieldId)
        {
            var field = _fieldRepository
                            .GetByID(fieldId);
            if (field == null)
            {
                throw new ArgumentException($"Field with id {fieldId} does not exists");
            }
            if (field.Type == FieldType.File)
            {
                DeleteFile(field.FieldId, field.FieldInformation.Blob.Localisation);
            }
            else
            {
                DeleteDirectory(fieldId);
            }

        }

        private void DeleteFile(Guid fieldId, string path)
        {
            AzureManager.DeleteFile(path);
            _sharedInformationRepository.Delete(fieldId);
        }

        private void DeleteDirectory(Guid fieldId)
        {
            _fieldRepository
                .Get(n => n.ParentDirectoryId == fieldId)
                .Select(n =>
                {
                    n.ParentDirectoryId = null;
                    return n;
                });
        }

    }
}
