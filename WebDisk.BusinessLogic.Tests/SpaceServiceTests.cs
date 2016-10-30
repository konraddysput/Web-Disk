using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebDisk.BusinessLogic.Extensions;
using WebDisk.BusinessLogic.Services;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.BusinessLogic.Interfaces;
using WebDisk.Database.DatabaseModel;
using Xunit;

namespace WebDisk.BusinessLogic.Tests
{
    public class SpaceServiceTests
    {
        private SpaceService _spaceService;

        public SpaceServiceTests()
        {
            _spaceService = new SpaceService();
        }

        [Fact]
        //[ExpectedException(typeof(ArgumentException))]
        public void TestNotExistingSpaces()
        {
            var userId = Guid.Empty;
            Assert.Throws<ArgumentException>(() => _spaceService.GetSpaces(userId));

        }

        [Theory]
        [InlineData("ac43555b-6e69-4b08-b169-2000f00441e8")]
        public void TestExistingSpaces(string stringGuid)
        {
            var userId = Guid.Parse(stringGuid);
            var expected = new List<Space>() { new Space()
            {
                OwnerId = userId
            }
            };

            var expectedServiceResult = expected.ConvertSpace();
            var mockSpaceRepository = new Mock<Repository<Space>>();
            var mockSpaceShareRepository = new Mock<Repository<SpaceShare>>();

            mockSpaceShareRepository
                .Setup(n => n.Get(g => g.SharedUserId == userId, null, string.Empty))
                .Returns(new List<SpaceShare>());

            mockSpaceRepository
                .Setup(n => n.Get(g => g.OwnerId == userId, null, string.Empty))
                .Returns(expected);

            var test = mockSpaceRepository.Object.Get(n=>n.OwnerId == userId);

            var temporarySpaceService = new SpaceService(mockSpaceRepository.Object
                                                        , mockSpaceShareRepository.Object);

            var result = temporarySpaceService.GetSpaces(userId);

            Assert.Equal(expectedServiceResult, result);


        }

        [Fact]
        public void TestSpaceCreation()
        {
            var userId = Guid.Empty;
            _spaceService.Create(userId);
        }

        [Fact]
        public void TestShareSpaceCreation()
        {
            var userId = Guid.Empty;
            var spaceId = Guid.Empty;
            _spaceService.ShareSpace(spaceId, userId);
        }

        [Fact]
        public void TestSpaceCreationTwice()
        {
            var userId = Guid.Empty;

            _spaceService.Create(userId);
            _spaceService.Create(userId);
        }

        [Fact]
        public void TestShareSpaceTwice()
        {
            var userId = Guid.Empty;
            var spaceId = Guid.Empty;

            _spaceService.ShareSpace(spaceId, userId);
            _spaceService.ShareSpace(spaceId, userId);
        }

        [Fact]
        //[ExpectedException(typeof(ArgumentException))]
        public void TestCreatSpaceByNotExistingUser()
        {
            _spaceService.Create(Guid.Empty);
        }
        [Fact]
        //[ExpectedException(typeof(ArgumentException))]
        public void TestShareUserSpace()
        {
            _spaceService.ShareSpace(Guid.Empty, Guid.Empty);
        }

        [Fact]
        public void TestDeletingNotExistingSpaces()
        {
            _spaceService.Delete(Guid.Empty);
        }
    }
}
