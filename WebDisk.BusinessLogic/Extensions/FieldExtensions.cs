using System;
using System.IO;
using System.Linq;
using WebDisk.BusinessLogic.Common;
using WebDisk.BusinessLogic.Services;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Database.DatabaseModel;
using WebDisk.Database.DatabaseModel.Types;

namespace WebDisk.BusinessLogic.Extensions
{
    public static class FieldExtensions
    {
        public static void CutField(this Field field, Field destination)
        {
            field.ParentDirectory = destination;
        }
        public static void DeleteField(this Field field, Repository<Field> fieldRepository)
        {
            FieldAction(field, fieldRepository, DeleteFile, fieldRepository.Delete);
        }

        public static void CopyField(this Field field, Field destination, Guid userId, Repository<Field> fieldRepository)
        {
            var copy = field.Copy(destination, userId);
            FieldAction(copy, userId, fieldRepository, fieldRepository.CreateField, fieldRepository.CreateDirectory);
        }

        private static void FieldAction(this Field field, Guid userId, Repository<Field> fieldRepository, Action<Guid, Guid, Field> fileAction, Action<Field, Guid> directoryAction)
        {
            if (field.Type == FieldType.File)
            {
                fileAction(userId, field.ParentDirectoryId.Value, field);
            }
            else
            {
                DirectoryAction(field, userId, fieldRepository, fileAction, directoryAction);
            }
        }


        public static void FieldAction(this Field field, Repository<Field> fieldRepository, Action<Field, Repository<Field>> fileAction, Action<Field> directoryAction)
        {
            if (field.Type == FieldType.File)
            {
                fileAction(field, fieldRepository);
            }
            else
            {
                DirectoryAction(field, fieldRepository, fileAction, directoryAction);
            }
        }
        private static void DirectoryAction(Field field, Repository<Field> fieldRepository, Action<Field, Repository<Field>> fileAction, Action<Field> directoryAction)
        {
            foreach (var child in field.Fields)
            {
                FieldAction(child, fieldRepository, fileAction, directoryAction);
            }
            directoryAction(field);
        }

        private static void DirectoryAction(Field field,
            Guid userId,
            Repository<Field> fieldRepository,
            Action<Guid, Guid, Field> fileAction,
            Action<Field, Guid> directoryAction)
        {
            //var childs = fieldRepository.Get(n => n.ParentDirectoryId == field.FieldId);
            foreach (var child in field.Fields)
            {
                FieldAction(child, userId, fieldRepository, fileAction, directoryAction);
            }
            directoryAction(field, userId);
        }

        private static void DeleteFile(this Field field, Repository<Field> fieldRepository)
        {
            if (field.FieldInformation != null) { AzureManager.DeleteFile(field.FieldInformation.Localisation); }
            fieldRepository.Delete(field);
        }

        public static void CreateField(this Repository<Field> fieldRepository, Guid userId, Guid parentDirectoryId, Field field, Stream inputStream)
        {
            string pathToAzureFile = new AzureManager()
                                          .UploadFile(inputStream);

            field.FieldInformation.Localisation = pathToAzureFile;
            field.ParentDirectoryId = parentDirectoryId;
            field.LastModifiedById = userId;

            fieldRepository.Insert(field);
        }

        public static void CreateField(this Repository<Field> fieldRepository, Guid userId, Guid parentDirectoryId, Field field)
        {
            if (field.FieldInformation == null || string.IsNullOrWhiteSpace(field.FieldInformation.Localisation))
            {
                throw new ArgumentException("There is no copy path in field information");
            }

            field.FieldId = Guid.NewGuid();
            field.FieldInformation = new FieldInformation()
            {
                FieldInformationId = field.FieldId,
                LastBackupDate = field.FieldInformation.LastBackupDate,
                Localisation = new AzureManager()
                                                        .Copy(field.FieldInformation.Localisation),
                Size = field.FieldInformation.Size
            };

            field.ParentDirectoryId = parentDirectoryId;
            field.LastModifiedById = userId;

            fieldRepository.Insert(field);
        }


        public static void CreateDirectory(this Repository<Field> fieldRepository, Guid userId, Guid parentDirectoryId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is required");
            }
            fieldRepository.Insert(new Field()
            {
                ParentDirectoryId = parentDirectoryId,
                LastModifiedDate = DateTime.Now,
                Extension = string.Empty,
                Type = FieldType.Directory,
                FieldInformation = null,
                Name = name
            });
        }

        public static void CreateDirectory(this Repository<Field> fieldRepository, Field field, Guid userId)
        {
            field.LastModifiedById = userId;
            fieldRepository.Insert(field);
        }


        private static Field Copy(this Field source, Field destination, Guid userId)
        {
            var copy = new Field()
            {
                Attributes = source.Attributes,
                Extension = source.Extension,
                FieldId = Guid.NewGuid(),
                ParentDirectoryId = destination.ParentDirectoryId,
                LastModifiedById = userId,
                Name = source.Name,
                ParentDirectory = destination,
                Type = source.Type,
            };

            if (source.Type == FieldType.File)
            {
                copy.FieldInformation = new FieldInformation()
                {
                    FieldInformationId = copy.FieldId,
                    Localisation = source.FieldInformation.Localisation,
                    Size = source.FieldInformation.Size
                };
            }
            copy.Fields = source.Fields.Select(n => n.Copy(copy, userId)).ToList();
            return copy;

        }
    }
}
