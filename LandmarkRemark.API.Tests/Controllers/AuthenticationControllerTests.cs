using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

using LandmarkRemark.API.Controllers;
using LandmarkRemark.Models.DTOs.Authentication;
using LandmarkRemark.Models.Exceptions.Authentication;
using LandmarkRemark.Services.Authentication;

namespace LandmarkRemark.API.Tests.Controllers
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        /// <summary>
        /// Tests generating an auth token works correctly when the user's login details are correct.
        /// </summary>
        [Test]
        public async Task TestGenerateAuthTokenWorksSuccessfullyWhenLoginDetailsCorrect()
        {
            // Set up the controller and services.
            var mockToken = new AuthTokenDTO()
            {
                Token = "TestToken",
                ExpiresIn = 1
            };

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(s => s.GenerateAuthToken(It.IsAny<LoginDetailsDTO>())).ReturnsAsync(mockToken);

            var controller = new AuthenticationController(authenticationService.Object);

            var loginDetails = new LoginDetailsDTO()
            {
                EmailAddress = "anthony.albanese@example.com",
                Password = "anthonyalbanese"
            };

            // Generate an auth token.
            var response = await controller.GenerateAuthToken(loginDetails);
            var result = response.Result as OkObjectResult;
            var token = result.Value as AuthTokenDTO;

            // Verify a token was generated.
            authenticationService.Verify(s => s.GenerateAuthToken(loginDetails));

            Assert.AreEqual(mockToken, token);
        }

        /// <summary>
        /// Tests generating an auth token throws an exception when a user's login details are incorret.
        /// </summary>
        [Test]
        public async Task TestGenerateAuthTokenThrowsExceptionWhenLoginDetailsIncorrect()
        {
            // Set up the controller and services.
            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(s => s.GenerateAuthToken(It.IsAny<LoginDetailsDTO>())).ThrowsAsync(new LoginDetailsIncorrectException("anthony.albanese@example.com"));

            var controller = new AuthenticationController(authenticationService.Object);

            var loginDetails = new LoginDetailsDTO()
            {
                EmailAddress = "anthony.albanese@example.com",
                Password = "anthonyalbanese"
            };

            // Generate an auth token.
            var response = await controller.GenerateAuthToken(loginDetails);
            var result = response.Result as UnauthorizedObjectResult;

            // Verify the correct result was returned.
            Assert.AreEqual(StatusCodes.Status401Unauthorized, result.StatusCode);
            Assert.AreEqual("Email address or password invalid.", result.Value);
        }
    }
}