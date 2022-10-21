using AutoMapper;
using Moq;
using NUnit.Framework;

using LandmarkRemark.Entities;
using LandmarkRemark.Mappings.Landmarks;
using LandmarkRemark.Models.Landmarks;
using LandmarkRemark.Repository.Landmarks;
using LandmarkRemark.Services.Landmarks;
using NetTopologySuite.Geometries;

namespace LandmarkRemark.Services.Tests.Landmarks
{
    [TestFixture]
    public class LandmarkServiceTests
    {
        /// <summary>
        /// Tests creating a new landmark.
        /// </summary>
        [Test]
        public async Task TestCreateLandmarkWorksSuccessfully()
        {
            // Set up the repository, mapper and service.
            var repository = new Mock<ILandmarkRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<LandmarkMappingProfile>()).CreateMapper();
            var landmarkService = new LandmarkService(repository.Object, mapper);

            // Set up the test data.
            var request = new CreateLandmarkRequest()
            {
                // Parliament Houe, Canberra.
                Notes = "This is a test",
                Longitude = 149.125241,
                Latitude = -35.307003,
                UserId = 1
            };

            // Create the landmark.
            var result = await landmarkService.Create(request);

            // Verify a new landmark was created.
            repository.Verify(r => r.Create(It.Is<Landmark>(l => l.Notes == request.Notes && l.Location.X == request.Longitude
                && l.Location.Y == request.Latitude && l.Location.SRID == 4326 && l.UserId == request.UserId)));

            // Verify the correct DTO is returned.
            Assert.AreEqual(request.Notes, result.Notes);
            Assert.AreEqual(request.Longitude, result.Longitude);
            Assert.AreEqual(request.Latitude, result.Latitude);
        }

        /// <summary>
        /// Tests finding landmarks by user ID.
        /// </summary>
        [Test]
        public async Task TestFindByUserIdWorksSuccessfully()
        {
            // Set up the repository, mapper and service.
            var repository = new Mock<ILandmarkRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<LandmarkMappingProfile>()).CreateMapper();
            var landmarkService = new LandmarkService(repository.Object, mapper);

            // Set up the test data.
            var landmarks = new List<Landmark>()
            {
                new Landmark()
                {
                    Id = 1,
                    Notes = "Parliament House, Canberra",
                    Location = new Point(149.125241, -35.307003),
                    UserId = 1
                }
            };

            repository.Setup(r => r.FindByUserId(It.IsAny<int>())).ReturnsAsync(landmarks);

            // Search for landmarks.
            int userId = 1;

            var result = await landmarkService.FindByUserId(userId);

            // Verify the correct landmark is returned.
            repository.Verify(r => r.FindByUserId(userId));

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(landmarks[0].Notes, result[0].Notes);
            Assert.AreEqual(landmarks[0].Location.X, result[0].Longitude);
            Assert.AreEqual(landmarks[0].Location.Y, result[0].Latitude);
        }
    }
}