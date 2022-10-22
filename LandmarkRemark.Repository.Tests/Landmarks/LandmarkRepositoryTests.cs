using Moq;
using Moq.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NUnit.Framework;

using LandmarkRemark.Entities;
using LandmarkRemark.Repository.Landmarks;

namespace LandmarkRemark.Repository.Tests.Landmarks
{
    [TestFixture]
    public class LandmarkRepositoryTests
    {
        /// <summary>
        /// Tests creating a new landmark.
        /// </summary>
        [Test]
        public async Task TestCreateLandmarkWorksSucessfully()
        {
            // Set up the context and repository.
            var configuration = Mock.Of<IConfiguration>();

            var landmarks = new List<Landmark>();
            var context = new Mock<LandmarkRemarkContext>(configuration);
            context.SetupGet(x => x.Landmarks).ReturnsDbSet(landmarks);

            var repository = new LandmarkRepository(context.Object);

            // Set up the test data.
            var landmark = new Landmark()
            {
                Notes = "This is a test",
                Location = new Point(149.125241, -35.307003), // Parliament House, Canberra.
                UserId = 1
            };

            // Create the landmark.
            await repository.Create(landmark);

            // Verify a new landmark was added and saved.
            context.Verify(c => c.Landmarks.Add(It.Is<Landmark>(l => l.Created.Day == DateTime.UtcNow.Day
                && l.Updated.Day == DateTime.UtcNow.Day && !l.Deleted)));
            context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        /// <summary>
        /// Tests finding landmarks by user ID when the user has some saved landmarks.
        /// </summary>
        [Test]
        public async Task TestFindByUserIdWorksSuccesfullyWhenUserHasLandmarks()
        {
            // Set up the context and repository.
            var configuration = Mock.Of<IConfiguration>();

            var landmarks = new List<Landmark>()
            {
                new Landmark()
                {
                    Notes = "Parliament House, Canberra",
                    Location = new Point(149.125241, -35.307003),
                    UserId = 1
                },
                new Landmark()
                {
                    Notes = "Parliament House, Brisbane",
                    Location = new Point(153.0252065, -27.4754275),
                    UserId = 2
                },
            };

            var context = new Mock<LandmarkRemarkContext>(configuration);
            context.SetupGet(x => x.Landmarks).ReturnsDbSet(landmarks);

            var repository = new LandmarkRepository(context.Object);

            // Search for landmarks.
            var userId = 1;

            var result = await repository.FindByUserId(userId);

            // Make sure one landmark is returned.
            Assert.AreEqual(1, result.Count);
        }

        /// <summary>
        /// Tests finding landmarks by user ID when the user has no saved landmarks.
        /// </summary>
        [Test]
        public async Task TestFindByUserIdWorksSuccesfullyWhenUserHasNoLandmarks()
        {
            // Set up the context and repository.
            var configuration = Mock.Of<IConfiguration>();

            var landmarks = new List<Landmark>()
            {
                new Landmark()
                {
                    Notes = "Parliament House, Canberra",
                    Location = new Point(149.125241, -35.307003),
                    UserId = 2
                },
                new Landmark()
                {
                    Notes = "Parliament House, Brisbane",
                    Location = new Point(153.0252065, -27.4754275),
                    UserId = 2
                },
            };

            var context = new Mock<LandmarkRemarkContext>(configuration);
            context.SetupGet(x => x.Landmarks).ReturnsDbSet(landmarks);

            var repository = new LandmarkRepository(context.Object);

            // Search for landmarks.
            var userId = 1;

            var result = await repository.FindByUserId(userId);

            // Make sure no landmarks are returned.
            Assert.AreEqual(0, result.Count);
        }

        /// <summary>
        /// Tests that deleted landmarks are not returned when searching by user ID.
        /// </summary>
        [Test]
        public async Task TestFindByUserIdDoesNotReturnDeletedLandmarks()
        {
            // Set up the context and repository.
            var configuration = Mock.Of<IConfiguration>();

            var landmarks = new List<Landmark>()
            {
                new Landmark()
                {
                    Notes = "Parliament House, Canberra",
                    Location = new Point(149.125241, -35.307003),
                    UserId = 1,
                    Deleted = true
                },
                new Landmark()
                {
                    Notes = "Parliament House, Brisbane",
                    Location = new Point(153.0252065, -27.4754275),
                    UserId = 2,
                    Deleted = false
                },
            };

            var context = new Mock<LandmarkRemarkContext>(configuration);
            context.SetupGet(x => x.Landmarks).ReturnsDbSet(landmarks);

            var repository = new LandmarkRepository(context.Object);

            // Search for landmarks.
            var userId = 1;

            var result = await repository.FindByUserId(userId);

            // Make sure no landmarks are returned.
            Assert.AreEqual(0, result.Count);
        }


        /// <summary>
        /// Tests finding all landmarks.
        /// </summary>
        [Test]
        public async Task TestFindAllWorksSuccesfully()
        {
            // Set up the context and repository.
            var configuration = Mock.Of<IConfiguration>();

            var landmarks = new List<Landmark>()
            {
                new Landmark()
                {
                    Notes = "Parliament House, Canberra",
                    Location = new Point(149.125241, -35.307003),
                    UserId = 1
                },
                new Landmark()
                {
                    Notes = "Parliament House, Brisbane",
                    Location = new Point(153.0252065, -27.4754275),
                    UserId = 2
                },
            };

            var context = new Mock<LandmarkRemarkContext>(configuration);
            context.SetupGet(x => x.Landmarks).ReturnsDbSet(landmarks);

            var repository = new LandmarkRepository(context.Object);

            // Search for landmarks.
            var result = await repository.FindAll();

            // Make sure 2 landmarks are returned.
            Assert.AreEqual(2, result.Count);
        }

        /// <summary>
        /// Tests that deleted landmarks are not returned when searching for all landmarks.
        /// </summary>
        [Test]
        public async Task TestFindAllDoesNotReturnDeletedLandmarks()
        {
            // Set up the context and repository.
            var configuration = Mock.Of<IConfiguration>();

            var landmarks = new List<Landmark>()
            {
                new Landmark()
                {
                    Notes = "Parliament House, Canberra",
                    Location = new Point(149.125241, -35.307003),
                    UserId = 1,
                    Deleted = true
                },
                new Landmark()
                {
                    Notes = "Parliament House, Brisbane",
                    Location = new Point(153.0252065, -27.4754275),
                    UserId = 2,
                    Deleted = false
                },
            };

            var context = new Mock<LandmarkRemarkContext>(configuration);
            context.SetupGet(x => x.Landmarks).ReturnsDbSet(landmarks);

            var repository = new LandmarkRepository(context.Object);

            // Search for landmarks.
            var result = await repository.FindAll();

            // Make sure one landmarks is returned.
            Assert.AreEqual(1, result.Count);
        }
    }
}