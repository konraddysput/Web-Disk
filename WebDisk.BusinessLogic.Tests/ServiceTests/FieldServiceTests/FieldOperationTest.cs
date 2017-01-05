using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Extensions;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;
using Xunit;

namespace WebDisk.BusinessLogic.Tests.ServiceTests.FieldServiceTests
{
    class FieldOperationTest
    {
        [Theory]
        [ExpectedException(typeof(Exception))]
        public void CopyFile()
        {
            Mock<Repository<Field>> fieldRepository = new Mock<Repository<Field>>();
            var fieldId = Guid.NewGuid();
            var field = new Field()
            {
                FieldId = fieldId,
                Extension = "png",
                FieldInformation = new FieldInformation(),
                Name = "temp",
                Type = Database.DatabaseModel.Types.FieldType.File,
                ParentDirectoryId = Guid.NewGuid()
            };
            var directoryId = Guid.NewGuid();
            var destinationDirectory = new Field()
            {
                FieldId = directoryId,
                Name = "temp",
                Type = Database.DatabaseModel.Types.FieldType.Directory
            };
            var userId = Guid.NewGuid();
            
            field.CopyField(destinationDirectory, userId, fieldRepository.Object);
        }

    }
}
