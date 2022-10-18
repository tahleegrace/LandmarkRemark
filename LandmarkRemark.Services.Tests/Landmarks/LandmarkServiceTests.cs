using AutoMapper;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

using LandmarkRemark.Entities;
using LandmarkRemark.Mappings.Landmarks;
using LandmarkRemark.Models.Landmarks;
using LandmarkRemark.Repository.Landmarks;
using LandmarkRemark.Services.Landmarks;

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

            // Create tbe landmark.
            var result = await landmarkService.Create(request);

            // Verify a new landmark was created.
            repository.Verify(r => r.Create(It.Is<Landmark>(l => l.Notes == request.Notes && l.Location.X == request.Longitude
                && l.Location.Y == request.Latitude && l.UserId == request.UserId)));

            // Verify the correct DTO is returned.
            Assert.AreEqual(request.Notes, result.Notes);
            Assert.AreEqual(request.Longitude, result.Longitude);
            Assert.AreEqual(request.Latitude, result.Latitude);
        }
    }
}