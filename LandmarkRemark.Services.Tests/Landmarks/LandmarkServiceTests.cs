using AutoMapper;
using Moq;
using NetTopologySuite.Geometries;
using NUnit.Framework;

using LandmarkRemark.Entities;
using LandmarkRemark.Mappings.Landmarks;
using LandmarkRemark.Models.DTOs.Landmarks;
using LandmarkRemark.Models.Exceptions.Landmarks;
using LandmarkRemark.Repository.Landmarks;
using LandmarkRemark.Services.Landmarks;

namespace LandmarkRemark.Services.Tests.Landmarks
{
    [TestFixture]
    public class LandmarkServiceTests
    {
        private readonly List<User> testUsers = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirstName = "Anthony",
                LastName = "Albanese",
                EmailAddress = "anthony.albanese@example.com"
            },
            new User()
            {
                Id = 2,
                FirstName = "Richard",
                LastName = "Marles",
                EmailAddress = "richard.marles@example.com"
            }
        };

        private const int CurrentUserId = 1;

        /// <summary>
        /// Tests creating a new landmark.
        /// </summary>
        [Test]
        public async Task TestCreateLandmarkWorksSuccessfully()
        {
            // Set up the repository, mapper and service.
            var repository = new Mock<ILandmarkRepository>();
            var mapper = new MapperConfiguration(config => config.AddProfile<LandmarkMappingProfile>()).CreateMapper();
            var landmarkService = new LandmarkService(repository.Object, mapper);

            repository.Setup(r => r.Create(It.IsAny<Landmark>())).ReturnsAsync
            (
                new Landmark()
                {
                    Id = 1,
                    Notes = "Parliament House, Canberra",
                    Location = new Point(149.125241, -35.307003),
                    UserId = CurrentUserId,
                    User = testUsers[0]
                }
            );

            // Set up the test data.
            var request = new CreateLandmarkRequest()
            {
                Notes = "Parliament House, Canberra",
                Longitude = 149.125241,
                Latitude = -35.307003
            };

            // Create the landmark.
            var result = await landmarkService.Create(request, CurrentUserId);

            // Verify a new landmark was created.
            repository.Verify(r => r.Create(It.Is<Landmark>(l => l.Notes == request.Notes && l.Location.X == request.Longitude
                && l.Location.Y == request.Latitude && l.Location.SRID == 4326 && l.UserId == CurrentUserId)));

            // Verify the correct DTO is returned.
            Assert.AreEqual(request.Notes, result.Notes);
            Assert.AreEqual(request.Longitude, result.Longitude);
            Assert.AreEqual(request.Latitude, result.Latitude);
            Assert.AreEqual("Anthony Albanese", result.UserFullName);
        }

        /// <summary>
        /// Tests creating a landmark throws an exception when a landmark is not provided.
        /// </summary>
        [Test]
        public void TestCreateLandmarkThrowsExceptionWhenLandmarkNotProvided()
        {
            // Set up the repository, mapper and service.
            var repository = new Mock<ILandmarkRepository>();
            var mapper = new MapperConfiguration(config => config.AddProfile<LandmarkMappingProfile>()).CreateMapper();
            var landmarkService = new LandmarkService(repository.Object, mapper);

            // Create the landmark.
            Assert.ThrowsAsync<LandmarkNotProvidedException>(async () =>
            {
                await landmarkService.Create(null, CurrentUserId);
            });
        }

        /// <summary>
        /// Tests finding the landmarks for the current user.
        /// </summary>
        [Test]
        public async Task TestFindMyLandmarksWorksSuccessfully()
        {
            // Set up the repository, mapper and service.
            var repository = new Mock<ILandmarkRepository>();
            var mapper = new MapperConfiguration(config => config.AddProfile<LandmarkMappingProfile>()).CreateMapper();
            var landmarkService = new LandmarkService(repository.Object, mapper);

            // Set up the test data.
            var landmarks = new List<Landmark>()
            {
                new Landmark()
                {
                    Id = 1,
                    Notes = "Parliament House, Canberra",
                    Location = new Point(149.125241, -35.307003),
                    UserId = CurrentUserId,
                    User = testUsers[0]
                }
            };

            repository.Setup(r => r.FindByUserId(It.IsAny<int>())).ReturnsAsync(landmarks);

            // Search for landmarks.
            var result = await landmarkService.FindMyLandmarks(CurrentUserId);

            // Verify the correct landmark is returned.
            repository.Verify(r => r.FindByUserId(CurrentUserId));

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(landmarks[0].Notes, result[0].Notes);
            Assert.AreEqual(landmarks[0].Location.X, result[0].Longitude);
            Assert.AreEqual(landmarks[0].Location.Y, result[0].Latitude);
            Assert.AreEqual("Anthony Albanese", result[0].UserFullName);
        }

        /// <summary>
        /// Tests finding all landmarks.
        /// </summary>
        [Test]
        public async Task TestFindAllWorksSuccessfully()
        {
            // Set up the repository, mapper and service.
            var repository = new Mock<ILandmarkRepository>();
            var mapper = new MapperConfiguration(config => config.AddProfile<LandmarkMappingProfile>()).CreateMapper();
            var landmarkService = new LandmarkService(repository.Object, mapper);

            // Set up the test data.
            var landmarks = new List<Landmark>()
            {
                new Landmark()
                {
                    Id = 1,
                    Notes = "Parliament House, Canberra",
                    Location = new Point(149.125241, -35.307003),
                    UserId = 1,
                    User = testUsers[0]
                }
            };

            repository.Setup(r => r.FindAll()).ReturnsAsync(landmarks);

            // Search for landmarks.
            var result = await landmarkService.FindAll();

            // Verify the correct landmark is returned.
            repository.Verify(r => r.FindAll());

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(landmarks[0].Notes, result[0].Notes);
            Assert.AreEqual(landmarks[0].Location.X, result[0].Longitude);
            Assert.AreEqual(landmarks[0].Location.Y, result[0].Latitude);
            Assert.AreEqual("Anthony Albanese", result[0].UserFullName);
        }

        /// <summary>
        /// Tests searching for landmarks
        /// </summary>
        [Test]
        public async Task TestSearchWorksSuccessfully()
        {
            // Set up the repository, mapper and service.
            var repository = new Mock<ILandmarkRepository>();
            var mapper = new MapperConfiguration(config => config.AddProfile<LandmarkMappingProfile>()).CreateMapper();
            var landmarkService = new LandmarkService(repository.Object, mapper);

            // Set up the test data.
            var landmarks = new List<Landmark>()
            {
                new Landmark()
                {
                    Id = 1,
                    Notes = "Parliament House, Canberra",
                    Location = new Point(149.125241, -35.307003),
                    UserId = 1,
                    User = testUsers[0]
                }
            };

            repository.Setup(r => r.Search(It.IsAny<string>())).ReturnsAsync(landmarks);

            // Search for landmarks.
            var result = await landmarkService.Search("Canberra");

            // Verify the correct landmark is returned.
            repository.Verify(r => r.Search("Canberra"));

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(landmarks[0].Notes, result[0].Notes);
            Assert.AreEqual(landmarks[0].Location.X, result[0].Longitude);
            Assert.AreEqual(landmarks[0].Location.Y, result[0].Latitude);
            Assert.AreEqual("Anthony Albanese", result[0].UserFullName);
        }
    }
}