using Moq;
using Moq.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NUnit.Framework;

using LandmarkRemark.Entities;
using LandmarkRemark.Repository.Users;

namespace LandmarkRemark.Repository.Tests.Users
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private readonly List<User> testUsers = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirstName = "Anthony",
                LastName = "Albanese",
                EmailAddress = "anthony.albanese@example.com",
                Password = "anthonyalbanese",
                Deleted = false
            },
            new User()
            {
                Id = 2,
                FirstName = "Richard",
                LastName = "Marles",
                EmailAddress = "richard.marles@example.com",
                Password = "richardmarles",
                Deleted = true
            }
        };

        /// <summary>
        /// Tests finding a user by email address works successfully when the user has not been deleted.
        /// </summary>
        [Test]
        public async Task TestFindByEmailAddressWorksSuccessfullyWhenUserNotDeleted()
        {
            // Set up the context and repository.
            var configuration = Mock.Of<IConfiguration>();

            var context = new Mock<LandmarkRemarkContext>(configuration);
            context.SetupGet(c => c.Users).ReturnsDbSet(testUsers);

            var repository = new UserRepository(context.Object);

            // Find the user.
            var user = await repository.FindByEmailAddress("anthony.albanese@example.com");

            // Verify the correct user is returned.
            Assert.IsNotNull(user);
            Assert.AreEqual(testUsers[0], user);
        }

        /// <summary>
        /// Tests finding a user by email address doesn't return a result when the user has been deleted.
        /// </summary>
        [Test]
        public async Task TestFindByEmailAddressReturnsNothingWhenUserIsDeleted()
        {
            // Set up the context and repository.
            var configuration = Mock.Of<IConfiguration>();

            var context = new Mock<LandmarkRemarkContext>(configuration);
            context.SetupGet(c => c.Users).ReturnsDbSet(testUsers);

            var repository = new UserRepository(context.Object);

            // Find the user.
            var user = await repository.FindByEmailAddress("richard.marles@example.com");

            // Verify no user is returned.
            Assert.IsNull(user);
        }
    }
}