using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebDisk.BusinessLogic.Extensions;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;
using WebDisk.Database.DatabaseModel.Types;
using Xunit;

namespace WebDisk.BusinessLogic.Tests.ServiceTests.DirectoryServiceTests
{
    public class DirectorySelectTests
    {

        //db ids
        private Guid _userId = Guid.NewGuid();
        private Guid _spaceId = Guid.NewGuid();
        private Guid _directoryId = Guid.NewGuid();

        private Guid _expectedSharedFileId = Guid.NewGuid();


        //default repository data
        private IEnumerable<Space> _userSpace;
        private IEnumerable<Field> _expectedFields;


        //mocks setup
        private Mock<Repository<Space>> _mockedSpaceRepository = new Mock<Repository<Space>>();
        private Mock<Repository<Field>> _mockedFieldRepository = new Mock<Repository<Field>>();

        public DirectorySelectTests()
        {
            //setup results
            _userSpace = new List<Space>() { new Space()
            {
                SpaceId = _userId,
                DefaultDirectoryId = _directoryId
            } };           

            _expectedFields = new List<Field>()
            {
                new Field()
                {
                    FieldId = Guid.Parse("59812153-e8bf-46ed-9110-e2a4e055c0bd"),
                    Type = FieldType.File,
                    Name= "file"
                },
                new Field()
                {
                    FieldId = Guid.Parse("1547a712-2ac9-495b-87d2-9f8a1884c319"),
                    Type = FieldType.Directory,
                    Name= "directory"
                }
            };
        }

        [Theory]
        public void GetNotExistingUserSpaceDirectory()
        {
            var notExistingUserId = Guid.NewGuid();
            Assert.Throws<ArgumentException>(() => new DirectoryService().GetAvailableFields(notExistingUserId));
        }

        [Theory]
        public void GetUserDirectoriesFromDefaultSpace()
        {
            //expecteResult
            var expectedResult = _expectedFields;
            //setup mocks logic
            _mockedSpaceRepository
                .Setup(n => n.GetDefaultSpaceDirectory(_userId).FieldId)
                .Returns(_directoryId);

            _mockedFieldRepository
                .Setup(n => n.Get(g => g.ParentDirectoryId == _directoryId, null, string.Empty))
                .Returns(_expectedFields);


            var result = new DirectoryService(_mockedSpaceRepository.Object, _mockedFieldRepository.Object)
                                            .GetAvailableFields(_userId);

            Assert.Contains(result, g => g.Type == FieldType.Directory && g.Name == "directory");
            Assert.Contains(result, g => g.Type == FieldType.File && g.Name == "file");
        }

        [Theory]
        public void GetUserSharedFields()
        {
       

            _mockedFieldRepository
                .Setup(n => n.Get(g => g.SharedInformations.Any(s => s.UserId == _userId), null, string.Empty))
                .Returns(_expectedFields);

            var result = new DirectoryService(_mockedSpaceRepository.Object, _mockedFieldRepository.Object).GetSharedFields(_userId);





            Assert.Contains(result, g => g.Type == FieldType.Directory && g.FieldId == Guid.Parse("1547a712-2ac9-495b-87d2-9f8a1884c319"));
            Assert.Contains(result, g => g.Type == FieldType.File && g.FieldId == Guid.Parse("59812153-e8bf-46ed-9110-e2a4e055c0bd"));
        }

        [Theory]
        public void GetNotExistingUserShareFields()
        {
            var notExistingUserId = Guid.NewGuid();
            Assert.Throws<ArgumentException>(() => new DirectoryService()
                                                        .GetSharedFields(notExistingUserId));
        }

        [Theory]
        public void GetFieldsFromDirectory()
        {
            
            new DirectoryService().GetAvailableFields(_userId, _directoryId);
        }

        [Theory]
        public void GetOnlyFieldsFromRoot()
        {
            throw new NotImplementedException();
            new DirectoryService().GetAvailableFields(_userId, _directoryId);
        }

        [Theory]
        public void GetOnlyDirectoryFromRoot()
        {
            throw new NotImplementedException();
            new DirectoryService().GetAvailableFields(_userId, _directoryId);
        }


    

    }
}
