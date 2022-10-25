using Moq;
using NUnit.Framework;

using LandmarkRemark.Entities;
using LandmarkRemark.Models.DTOs.Authentication;
using LandmarkRemark.Models.Exceptions.Authentication;
using LandmarkRemark.Repository.Users;
using LandmarkRemark.Services.Authentication;

namespace LandmarkRemark.Services.Tests.Authentication
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private readonly List<User> testUsers = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirstName = "Anthony",
                LastName = "Albanese",
                EmailAddress = "anthony.albanese@example.com",
                Password = "anthonyalbanese"
            },
            new User()
            {
                Id = 2,
                FirstName = "Richard",
                LastName = "Marles",
                EmailAddress = "richard.marles@example.com",
                Password = "richardmarles"
            }
        };

        /// <summary>
        /// Tests generating an auth token works correctly when the user's login details are correct.
        /// </summary>
        [Test]
        public async Task TestGenerateAuthTokenWorksSuccessfullyWhenLoginDetailsCorrect()
        {
            // Set up the repository and services.
            var userRepository = new Mock<IUserRepository>();
            var tokenService = new Mock<ITokenService>();
            var authenticationService = new AuthenticationService(userRepository.Object, tokenService.Object);

            userRepository.Setup(r => r.FindByEmailAddress(It.IsAny<string>())).ReturnsAsync(testUsers[0]);

            var mockToken = new AuthTokenDTO()
            {
                Token = "TestToken",
                ExpiresIn = 1
            };

            tokenService.Setup(s => s.Generate(It.IsAny<User>())).Returns(mockToken);

            // Generate an auth token.
            var token = await authenticationService.GenerateAuthToken(new LoginDetailsDTO()
            {
                EmailAddress = "anthony.albanese@example.com",
                Password = "anthonyalbanese"
            });

            // Make sure the correct token is returned.
            Assert.AreEqual(mockToken, token);
        }

        /// <summary>
        /// Tests generating an auth token throws an exception when a user doesn't exist with the specified email address.
        /// </summary>
        [Test]
        public void TestGenerateAuthTokenThrowsExceptionWhenEmailAddressIncorrect()
        {
            // Set up the repository and services.
            var userRepository = new Mock<IUserRepository>();
            var tokenService = new Mock<ITokenService>();
            var authenticationService = new AuthenticationService(userRepository.Object, tokenService.Object);

            userRepository.Setup(r => r.FindByEmailAddress(It.IsAny<string>())).ReturnsAsync((User?) null);

            // Generate an auth token.
            Assert.ThrowsAsync<LoginDetailsIncorrectException>(() => authenticationService.GenerateAuthToken(new LoginDetailsDTO()
            {
                EmailAddress = "scott.morrison@example.com",
                Password = "cronullasharks"
            }));
        }

        /// <summary>
        /// Tests generating an auth token throws an exception when the user's password is incorrect.
        /// </summary>
        [Test]
        public void TestGenerateAuthTokenThrowsExceptionWhenPasswordIncorrect()
        {
            // Set up the repository and services.
            var userRepository = new Mock<IUserRepository>();
            var tokenService = new Mock<ITokenService>();
            var authenticationService = new AuthenticationService(userRepository.Object, tokenService.Object);

            userRepository.Setup(r => r.FindByEmailAddress(It.IsAny<string>())).ReturnsAsync(testUsers[0]);

            // Generate an auth token.
            Assert.ThrowsAsync<LoginDetailsIncorrectException>(() => authenticationService.GenerateAuthToken(new LoginDetailsDTO()
            {
                EmailAddress = "anthony.albanese@example.com",
                Password = "southsydneyrabbitohs"
            }));
        }
    }
}