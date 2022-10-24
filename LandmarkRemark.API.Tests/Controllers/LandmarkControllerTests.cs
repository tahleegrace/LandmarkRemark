using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

using LandmarkRemark.API.Controllers;
using LandmarkRemark.Models.DTOs.Landmarks;
using LandmarkRemark.Models.Exceptions.Landmarks;
using LandmarkRemark.Services.Authentication;
using LandmarkRemark.Services.Landmarks;

namespace LandmarkRemark.API.Tests.Controllers
{
    [TestFixture]
    public class LandmarkControllerTests
    {
        private const int CurrentUserId = 1;

        /// <summary>
        /// Tests creating a new landmark.
        /// </summary>
        [Test]
        public async Task TestCreateLandmarkWorksSuccessfullyWhenUserIsLoggedIn()
        {
            // Set up the controller and services.
            var mockLandmarkDTO = new LandmarkDTO()
            {
                Notes = "This is a test",
                Longitude = 149.125241,
                Latitude = -35.307003,
                UserFullName = "Anthony Albanese"
            };

            var landmarkService = new Mock<ILandmarkService>();
            landmarkService.Setup(s => s.Create(It.IsAny<CreateLandmarkRequest>(), It.IsAny<int>())).ReturnsAsync(mockLandmarkDTO);

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(s => s.GetCurrentUserId(It.IsAny<ClaimsPrincipal>())).Returns(CurrentUserId);

            var landmarkController = new LandmarkController(authenticationService.Object, landmarkService.Object);

            // Set up the test data.
            var request = new CreateLandmarkRequest()
            {
                // Parliament House, Canberra.
                Notes = "This is a test",
                Longitude = 149.125241,
                Latitude = -35.307003
            };

            // Create the landmark.
            var response = await landmarkController.Create(request);
            var result = response.Result as OkObjectResult;
            var newLandmark = result.Value as LandmarkDTO;

            // Verify a new landmark was created.
            landmarkService.Verify(s => s.Create(It.Is<CreateLandmarkRequest>(l => l == request), It.Is<int>(id => id == CurrentUserId)));

            // Verify the correct DTO was returned.
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual(mockLandmarkDTO, newLandmark);
        }

        /// <summary>
        /// Tests creating a landmark throws an exception when a landmark is not provided.
        /// </summary>
        [Test]
        public async Task TestCreateLandmarkThrowsExceptionWhenLandmarkNotProvided()
        {
            // Set up the controller and services.
            var landmarkService = new Mock<ILandmarkService>();
            landmarkService.Setup(s => s.Create(It.IsAny<CreateLandmarkRequest>(), It.IsAny<int>())).ThrowsAsync(new LandmarkNotProvidedException());

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(s => s.GetCurrentUserId(It.IsAny<ClaimsPrincipal>())).Returns(CurrentUserId);

            var landmarkController = new LandmarkController(authenticationService.Object, landmarkService.Object);

            // Create the landmark.
            var response = await landmarkController.Create(null);
            var result = response.Result as BadRequestObjectResult;

            // Verify the correct DTO was returned.
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual("Please provide a landmark to create.", result.Value);
        }

        /// <summary>
        /// Tests finding the landmarks for the current user.
        /// </summary>
        [Test]
        public async Task TestFindMyLandmarksWorksSuccessfullyWhenUserIsLoggedIn()
        {
            // Set up the controller and services.
            var mockLandmarkDTO = new LandmarkDTO()
            {
                Notes = "This is a test",
                Longitude = 149.125241,
                Latitude = -35.307003,
                UserFullName = "Anthony Albanese"
            };

            var landmarkService = new Mock<ILandmarkService>();
            landmarkService.Setup(s => s.FindMyLandmarks(It.IsAny<int>())).ReturnsAsync(new List<LandmarkDTO>() { mockLandmarkDTO });

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(s => s.GetCurrentUserId(It.IsAny<ClaimsPrincipal>())).Returns(CurrentUserId);

            var landmarkController = new LandmarkController(authenticationService.Object, landmarkService.Object);

            // Find the landmarks for the user.
            var response = await landmarkController.FindMyLandmarks();
            var result = response.Result as OkObjectResult;
            var landmarks = result.Value as List<LandmarkDTO>;

            // Verify the correct DTO was returned.
            landmarkService.Verify(s => s.FindMyLandmarks(CurrentUserId));

            Assert.AreEqual(mockLandmarkDTO, landmarks[0]);
        }

        /// <summary>
        /// Tests finding all landmarks.
        /// </summary>
        [Test]
        public async Task TestFindAllWorksSuccessfully()
        {
            // Set up the controller and services.
            var mockLandmarkDTO = new LandmarkDTO()
            {
                Notes = "This is a test",
                Longitude = 149.125241,
                Latitude = -35.307003,
                UserFullName = "Anthony Albanese"
            };

            var landmarkService = new Mock<ILandmarkService>();
            landmarkService.Setup(s => s.FindAll()).ReturnsAsync(new List<LandmarkDTO>() { mockLandmarkDTO });

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(s => s.GetCurrentUserId(It.IsAny<ClaimsPrincipal>())).Returns(CurrentUserId);

            var landmarkController = new LandmarkController(authenticationService.Object, landmarkService.Object);

            // Find the landmarks for the user.
            var response = await landmarkController.FindAll();
            var result = response.Result as OkObjectResult;
            var landmarks = result.Value as List<LandmarkDTO>;

            // Verify the correct DTO was returned.
            landmarkService.Verify(s => s.FindAll());

            Assert.AreEqual(mockLandmarkDTO, landmarks[0]);
        }

        /// <summary>
        /// Tests searching for landmarks.
        /// </summary>
        [Test]
        public async Task TestSearchWorksSuccessfully()
        {
            // Set up the controller and services.
            var mockLandmarkDTO = new LandmarkDTO()
            {
                Notes = "This is a test",
                Longitude = 149.125241,
                Latitude = -35.307003,
                UserFullName = "Anthony Albanese"
            };

            var landmarkService = new Mock<ILandmarkService>();
            landmarkService.Setup(s => s.Search(It.IsAny<string>())).ReturnsAsync(new List<LandmarkDTO>() { mockLandmarkDTO });

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(s => s.GetCurrentUserId(It.IsAny<ClaimsPrincipal>())).Returns(CurrentUserId);

            var landmarkController = new LandmarkController(authenticationService.Object, landmarkService.Object);

            // Find the landmarks for the user.
            var response = await landmarkController.Search("test");
            var result = response.Result as OkObjectResult;
            var landmarks = result.Value as List<LandmarkDTO>;

            // Verify the correct DTO was returned.
            landmarkService.Verify(s => s.Search("test"));

            Assert.AreEqual(mockLandmarkDTO, landmarks[0]);
        }
    }
}