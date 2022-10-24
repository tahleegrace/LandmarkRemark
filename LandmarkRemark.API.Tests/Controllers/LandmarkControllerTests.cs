using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

using LandmarkRemark.API.Controllers;
using LandmarkRemark.Models.DTOs.Landmarks;
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
                Latitude = -35.307003,
                UserFullName = "Anthony Albanese"
            };

            var service = new Mock<ILandmarkService>();
            service.Setup(s => s.Create(It.IsAny<CreateLandmarkRequest>())).ReturnsAsync(mockLandmarkDTO);

            var landmarkController = new LandmarkController(service.Object);

            // Set up the test data.
            var request = new CreateLandmarkRequest()
            {
                // Parliament House, Canberra.
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

        /// <summary>
        /// Tests finding landmarks by user ID.
        /// </summary>
        [Test]
        public async Task TestFindByUserIdWorksSuccessfully()
        {
            // Set up the controller and service.
            var mockLandmarkDTO = new LandmarkDTO()
            {
                Notes = "This is a test",
                Longitude = 149.125241,
                Latitude = -35.307003,
                UserFullName = "Anthony Albanese"
            };

            var service = new Mock<ILandmarkService>();
            service.Setup(s => s.FindByUserId(It.IsAny<int>())).ReturnsAsync(new List<LandmarkDTO>() { mockLandmarkDTO });

            var landmarkController = new LandmarkController(service.Object);

            // Find the landmarks for the user.
            int userId = 1;

            var response = await landmarkController.FindByUserId(userId);
            var result = response.Result as OkObjectResult;
            var landmarks = result.Value as List<LandmarkDTO>;

            // Verify the correct DTO was returned.
            service.Verify(s => s.FindByUserId(userId));

            Assert.AreEqual(mockLandmarkDTO, landmarks[0]);
        }

        /// <summary>
        /// Tests finding all landmarks.
        /// </summary>
        [Test]
        public async Task TestFindAllWorksSuccessfully()
        {
            // Set up the controller and service.
            var mockLandmarkDTO = new LandmarkDTO()
            {
                Notes = "This is a test",
                Longitude = 149.125241,
                Latitude = -35.307003,
                UserFullName = "Anthony Albanese"
            };

            var service = new Mock<ILandmarkService>();
            service.Setup(s => s.FindAll()).ReturnsAsync(new List<LandmarkDTO>() { mockLandmarkDTO });

            var landmarkController = new LandmarkController(service.Object);

            // Find the landmarks for the user.
            var response = await landmarkController.FindAll();
            var result = response.Result as OkObjectResult;
            var landmarks = result.Value as List<LandmarkDTO>;

            // Verify the correct DTO was returned.
            service.Verify(s => s.FindAll());

            Assert.AreEqual(mockLandmarkDTO, landmarks[0]);
        }

        /// <summary>
        /// Tests searching for landmarks.
        /// </summary>
        [Test]
        public async Task TestSearchWorksSuccessfully()
        {
            // Set up the controller and service.
            var mockLandmarkDTO = new LandmarkDTO()
            {
                Notes = "This is a test",
                Longitude = 149.125241,
                Latitude = -35.307003,
                UserFullName = "Anthony Albanese"
            };

            var service = new Mock<ILandmarkService>();
            service.Setup(s => s.Search(It.IsAny<string>())).ReturnsAsync(new List<LandmarkDTO>() { mockLandmarkDTO });

            var landmarkController = new LandmarkController(service.Object);

            // Find the landmarks for the user.
            var response = await landmarkController.Search("test");
            var result = response.Result as OkObjectResult;
            var landmarks = result.Value as List<LandmarkDTO>;

            // Verify the correct DTO was returned.
            service.Verify(s => s.Search("test"));

            Assert.AreEqual(mockLandmarkDTO, landmarks[0]);
        }
    }
}