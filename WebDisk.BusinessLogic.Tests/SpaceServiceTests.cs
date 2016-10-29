using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Tests
{
    [TestFixture]
    class SpaceServiceTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNotExistingSpaces()
        {
            var userId = Guid.Empty;
            var mockSpaceRepository = new Mock<Repository<Space>>();
            var mockSharedSpaceRepository = new Mock<Repository<SpaceShare>>();
            var spaceService = new SpaceService().GetSpaces(userId);
        }

        [Test]
        public void TestSpaceCreation()
        {
            var userId = Guid.Empty;
            new SpaceService().Create(userId);
        }

        [Test]
        public void TestShareSpaceCreation()
        {
            var userId = Guid.Empty;
            var spaceId = Guid.Empty;
            new SpaceService().ShareSpace(spaceId,userId);
        }

        [Test]
        public void TestSpaceCreationTwice()
        {
            var userId = Guid.Empty;
            var spaceService = new SpaceService();
            spaceService.Create(userId);
            spaceService.Create(userId);
        }

        [Test]
        public void TestShareSpaceTwice()
        {
            var userId = Guid.Empty;
            var spaceId = Guid.Empty;
            var spaceService = new SpaceService();
            spaceService.ShareSpace(spaceId, userId);
            spaceService.ShareSpace(spaceId, userId);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreatSpaceByNotExistingUser()
        {
            new SpaceService().Create(Guid.Empty);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestShareUserSpace()
        {
            new SpaceService().ShareSpace(Guid.Empty, Guid.Empty);
        }

        [Test]
        public void TestDeletingNotExistingSpaces()
        {
            new SpaceService().Delete(Guid.Empty);
        }
    }
}
