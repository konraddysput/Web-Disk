using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Extensions;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.BaseModels;
using WebDisk.Database.DatabaseModel;
using WebDisk.Database.DatabaseModel.Types;
using Xunit;

namespace WebDisk.BusinessLogic.Tests.ServiceTests.DirectoryServiceTests
{
    public class DirectoryServiceTests
    {

        //db ids
        private Guid _userId = Guid.NewGuid();
        private Guid _spaceId = Guid.NewGuid();
        private Guid _directoryId = Guid.NewGuid();

        private Guid _expectedSharedFileId = Guid.NewGuid();


        //default repository data
        private IEnumerable<Space> _userSpace;
        private IEnumerable<Directory> _expectedDirectories;
        private IEnumerable<File> _expectedFiles;


        //mocks setup
        private Mock<Repository<Space>> _mockedSpaceRepository = new Mock<Repository<Space>>();
        private Mock<Repository<Directory>> _mockedDirectoryRepository = new Mock<Repository<Directory>>();
        private Mock<Repository<File>> _mockedFileRepository = new Mock<Repository<File>>();

        public DirectoryServiceTests()
        {
            //setup results
            _userSpace = new List<Space>() { new Space()
            {
                SpaceId = _userId,
                DefaultDirectoryId = _directoryId
            } };

            _expectedDirectories = new List<Directory>()
            {
                new Directory()
                {
                    FieldId = Guid.Parse("1547a712-2ac9-495b-87d2-9f8a1884c319")
                }
            };

            _expectedFiles = new List<File>()
            {
                new File()
                {
                    FieldId = Guid.Parse("59812153-e8bf-46ed-9110-e2a4e055c0bd")
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
            var expectedResult = _expectedFiles
                                    .Cast<FieldBase>()
                                    .Concat(_expectedDirectories
                                                .Cast<FieldBase>());
            //setup mocks logic
            _mockedSpaceRepository
                .Setup(n => n.GetDefaultSpaceDirectory(_userId).FieldId)
                .Returns(_directoryId);

            _mockedDirectoryRepository
                .Setup(n => n.Get(g => g.ParentDirectoryId == _directoryId, null, string.Empty))
                .Returns(_expectedDirectories);

            _mockedFileRepository
                .Setup(n => n.Get(g => g.ParentDirectoryId == _directoryId, null, string.Empty))
                .Returns(_expectedFiles);

            var result = new DirectoryService(_mockedSpaceRepository.Object,
                                              _mockedDirectoryRepository.Object, _mockedFileRepository.Object)
                                            .GetAvailableFields(_userId);

            Assert.Contains(result, g => g.Type == FieldType.Directory && g.Name == "directory");
            Assert.Contains(result, g => g.Type == FieldType.File && g.Name == "file");
        }

        [Theory]
        public void GetUserSharedFields()
        {
            var expectedResult = _expectedDirectories.Cast<FieldBase>()
                                        .Concat(_expectedFiles.Cast<FieldBase>());
            //setup mocks logic
            _mockedDirectoryRepository
                .Setup(n => n.Get(g => g.SharedInformations.Any(s => s.UserId == _userId), null, string.Empty))
                .Returns(_expectedDirectories);

            _mockedFileRepository
                .Setup(n => n.Get(g => g.SharedInformations.Any(s => s.UserId == _userId), null, string.Empty))
                .Returns(_expectedFiles);

            var result = new DirectoryService(_mockedSpaceRepository.Object, _mockedDirectoryRepository.Object
                                            , _mockedFileRepository.Object).GetSharedFields(_userId);





            Assert.Contains(result, g => g.Type == FieldType.Directory && g.FieldId == Guid.Parse("1547a712-2ac9-495b-87d2-9f8a1884c319"));
            Assert.Contains(result, g => g.Type == FieldType.File && g.FieldId == Guid.Parse("59812153-e8bf-46ed-9110-e2a4e055c0bd"));
        }

        [Theory]
        public void GetNotExistingUserShareFields()
        {
            var notExistingUserId = Guid.NewGuid();
            Assert.Throws<ArgumentException>(() => new DirectoryService().GetSharedFields(notExistingUserId));
        }

        [Theory]
        public void GetFieldsFromDirectory()
        {
            throw new NotImplementedException();
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
