using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;
using Xunit;

namespace WebDisk.BusinessLogic.Tests.ServiceTests.SpaceServiceTests
{
    public class SpaceServiceTests
    {
        private SpaceService _spaceService;
        private Mock<WebDiskDbContext> _dbContext = new Mock<WebDiskDbContext>();
        public SpaceServiceTests()
        {
            _spaceService = new SpaceService(_dbContext.Object);
        }

        [Fact]
        //[ExpectedException(typeof(ArgumentException))]
        public void TestNotExistingSpaces()
        {
            var userId = Guid.Empty;
            Assert.Throws<ArgumentException>(() => _spaceService.GetSpace(userId));

        }

        [Theory]
        [InlineData("ac43555b-6e69-4b08-b169-2000f00441e8")]
        public void TestExistingSpaces(string stringGuid)
        {
            var userId = Guid.Parse(stringGuid);
            var expected = new Space()
            {
                SpaceId = userId
            };

            var expectedServiceResult = new List<Space>()
            {
                expected
            };
            

            //mocking repositories
            var mockSpaceRepository = new Mock<Repository<Space>>();

            //mocking repositories methods
            mockSpaceRepository
                .Setup(n => n.Get(It.IsAny<Expression<Func<Space, bool>>>(), It.IsAny<Func<IQueryable<Space>, IOrderedQueryable<Space>>>(), string.Empty))
                .Returns(expectedServiceResult);

            var temporarySpaceService = new SpaceService(_dbContext.Object);

            var result = temporarySpaceService.GetSpace(userId);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("ac43555b-6e69-4b08-b169-2000f00441e8")]
        public void TestSpaceCreation(string stringUserId)
        {
            var userId = Guid.Parse(stringUserId);

            var user = new ApplicationUser()
            {
                Id = userId
            };


            //setup mock
            var mockSpaceRepository = new Mock<Repository<Space>>();
            var mockUserRepository = new Mock<Repository<ApplicationUser>>();

            mockUserRepository
                .Setup(n => n.GetByID(It.IsAny<Guid>()))
                .Returns(user);

            mockSpaceRepository
                .Setup(n => n.Insert(It.IsAny<Space>()));

            _spaceService.Create(userId);
        }
        [Fact]
        public void TestSpaceCreationWithNotExistingUser()
        {
            Assert.Throws<ArgumentException>(() => _spaceService.Create(Guid.Empty));
        }
        

        [Theory]
        public void TestShareSpace(string stringUserId)
        {
            var userId = Guid.NewGuid();

            var testedUser = new ApplicationUser()
            {
                Id = userId
            };

            var testedSpace = new Space()
            {
                SpaceId = userId
            };
            //mocking 
            var mockSpaceRepository = new Mock<Repository<Space>>();
            var mockUserRepository = new Mock<Repository<ApplicationUser>>();

            mockUserRepository
                .Setup(n => n.GetByID(It.IsAny<Guid>()))
                .Returns(testedUser);

            mockSpaceRepository
                .Setup(n => n.GetByID(It.IsAny<Guid>()))
                .Returns(testedSpace);
            throw new NotImplementedException();

        }

        [Fact]
        public void TestSpaceCreationTwice()
        {
            var userId = Guid.Empty;
            // no mock for method that check if space exists
            throw new NotImplementedException();
            _spaceService.Create(userId);
        }

        [Fact]
        public void TestShareSpaceTwice()
        {
            var userId = Guid.Empty;
            var spaceId = Guid.Empty;

            // no mock for method that check if space is already shared
            throw new NotImplementedException();

            _spaceService.ShareSpace(spaceId, userId);
            _spaceService.ShareSpace(spaceId, userId);
        }

       
        [Fact]
        //[ExpectedException(typeof(ArgumentException))]
        public void TestShareUserSpace()
        {
            //_spaceService.ShareSpace(Guid.Empty, Guid.Empty);
            throw new NotImplementedException();
        }

        [Fact]
        public void TestDeletingNotExistingSpaces()
        {
            //_spaceService.Delete(Guid.Empty);
            throw new NotImplementedException();
        }

        [Fact]
        public void TestSharingOwnSpace()
        {
            throw new NotImplementedException();
        }
    }
}
