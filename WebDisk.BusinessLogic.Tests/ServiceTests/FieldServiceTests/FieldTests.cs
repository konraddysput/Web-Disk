using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;
using Xunit;

namespace WebDisk.BusinessLogic.Tests.ServiceTests.DirectoryServiceTests
{
    public class FieldTests
    {
        private Mock<Repository<Field>> _mockedFieldRepository = new Mock<Repository<Field>>();
        [Theory]
        public void InsertInvalidTypeOfDirectory()
        {

            Assert.Throws<ArgumentException>(() => new Field()
            {
                Type = Database.DatabaseModel.Types.FieldType.File,
                FieldInformation = new FieldInformation()
                {
                    Size = 125
                }
            }
            );
        }

        [Fact]
        public void UpdatingFieldWithInvalidProperty()
        {
            var correctField = new Field()
            {
                Type = Database.DatabaseModel.Types.FieldType.File,
                FieldInformation = new FieldInformation()
                {
                    Size = 123
                }
            };
            Assert.Throws<ArgumentException>(() =>
            {
                correctField.Type = Database.DatabaseModel.Types.FieldType.Directory;
                return correctField.FieldInformation.FieldId;
            });
        }

        [Fact]
        public void TestCorrectField()
        {
            var correctField = new Field()
            {
                Type = Database.DatabaseModel.Types.FieldType.File,
                FieldInformation = new FieldInformation()
                {
                    Size = 1
                }
            };


        }
    }
}
