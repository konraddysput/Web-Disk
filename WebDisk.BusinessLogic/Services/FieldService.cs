using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Aspects;
using WebDisk.BusinessLogic.Common;
using WebDisk.BusinessLogic.Interfaces;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Database.DatabaseModel;
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.BusinessLogic.Services
{
    public class FieldService : ServiceBase, IFieldService
    {
        //Repositories
        private Repository<Space> _spaceRepository;
        private Repository<Field> _fieldRepository;
        private Repository<FieldShareInformation> _sharedInformationRepository;

        public Repository<Space> SpaceRepository
        {
            get
            {
                if(_spaceRepository == null)
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

        public FieldService(WebDiskDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Create new files in existing directory
        /// </summary>
        /// <param name="userId">current user Id<</param>
        /// <param name="fieldId">>directory where file should exists<</param>
        /// <param name="fileViewModels">information about file - bytes, name, extensions</param>
        public void CreateField(Guid userId, Guid fieldId, IEnumerable<FileViewModel> fileViewModels)
        {
            foreach (var fileViewModel in fileViewModels)
            {
                CreateField(userId, fieldId, fileViewModel);
            }
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

            string pathToAzureFile = new AzureManager()
                                            .UploadFile(fileViewModel.InputStream);
            string fileName = Path.GetFileNameWithoutExtension(fileViewModel.FileName);
            string extension = Path.GetExtension(fileViewModel.FileName);

            FieldRepository
                .Insert(new Field()
                {
                    ParentDirectoryId = fieldId,
                    Name = fileName,
                    Extension = extension,
                    Type = FieldType.File,
                    LastModifiedById = userId,
                    FieldInformation = new FieldInformation()
                    {
                        Size = fileViewModel.ContentLength,
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
            var field = FieldRepository
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
            SharedInformationRepository.Delete(fieldId);
        }

        private void DeleteDirectory(Guid fieldId)
        {
            FieldRepository
                .Get(n => n.ParentDirectoryId == fieldId)
                .Select(n =>
                {
                    n.ParentDirectoryId = null;
                    return n;
                });
        }
    }
}
