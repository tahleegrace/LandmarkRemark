using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

using LandmarkRemark.API.Controllers;
using LandmarkRemark.Models.Landmarks;
using LandmarkRemark.Services.Landmarks;

namespace LandmarkRemark.API.Tests.Controllers
{
    [TestFixture]
    public class LandmarkControllerTests
    {
        /// <summary>
        /// Tests creating a new landmark.
        /// </summary>
        [Test]
        public async Task TestCreateLandmarkWorksSuccessfully()
        {
            // Set up the controller and service.
            var mockLandmarkDTO = new LandmarkDTO()
            {
                Notes = "This is a test",
                Longitude = 149.125241,
                Latitude = -35.307003
            };

            var service = new Mock<ILandmarkService>();
            service.Setup(s => s.Create(It.IsAny<CreateLandmarkRequest>())).ReturnsAsync(mockLandmarkDTO);

            var landmarkController = new LandmarkController(service.Object);

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
            var response = await landmarkController.Create(request);
            var result = response.Result as OkObjectResult;
            var newLandmark = result.Value as LandmarkDTO;
            
            // Verify a new landmark was created.
            service.Verify(s => s.Create(It.Is<CreateLandmarkRequest>(l => l == request)));

            // Verify the correct DTO was returned.
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual(mockLandmarkDTO, newLandmark);
        }
    }
}