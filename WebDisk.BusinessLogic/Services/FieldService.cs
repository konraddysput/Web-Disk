using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Aspects;
using WebDisk.BusinessLogic.Common;
using WebDisk.BusinessLogic.Extensions;
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

        private Repository<Field> FieldRepository
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
            var field = AutoMapper.Mapper.Map<Field>(fileViewModel);
            FieldRepository.CreateField(userId, fieldId, field, fileViewModel.InputStream);
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
            field.DeleteField(FieldRepository);
        }

        [FieldAccess]
        [AfterDataChange]
        public void Copy(Guid userId, Guid destinationId, Guid fieldId)
        {
            var destinationDirectory = FieldRepository.GetByID(destinationId);
            var currentField = FieldRepository.GetByID(fieldId);

            if (destinationDirectory == null || currentField == null)
            {
                throw new ArgumentException("directory or field does not exists");
            }
            if (currentField.ParentDirectoryId == destinationId)
            {
                return;
            }
            currentField.CopyField(destinationDirectory, userId, FieldRepository);
        }

        [FieldAccess]
        [AfterDataChange]
        public void Cut(Guid userId, Guid destinationId, Guid fieldId)
        {
            var destinationDirectory = FieldRepository.GetByID(destinationId);
            var currentField = FieldRepository.GetByID(fieldId);

            if (destinationDirectory == null || currentField == null)
            {
                throw new ArgumentException("directory or field does not exists");
            }
            if (currentField.ParentDirectoryId == destinationId)
            {
                return;
            }
            currentField.CutField(destinationDirectory);
        }

        [FieldAccess]
        public FileViewModel Get(Guid userId, Guid fieldId)
        {
            var currentField = FieldRepository.GetByID(fieldId);
            if (currentField == null)
            {
                throw new ArgumentException("You cannot download directory or field that does not exists");
            }

            return currentField.Download();

        }

        [FieldAccess]
        [AfterDataChange]
        public void Update(Guid userId, Guid fieldId, string newFieldName)
        {
            var field = FieldRepository.GetByID(fieldId);
            if (field == null)
            {
                throw new ArgumentException("Field does not exists");
            }
            field.Name = newFieldName;
        }
    }
}
