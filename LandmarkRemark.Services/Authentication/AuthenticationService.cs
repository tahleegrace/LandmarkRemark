using LandmarkRemark.Models.DTOs.Authentication;
using LandmarkRemark.Models.Exceptions.Authentication;
using LandmarkRemark.Repository.Users;

namespace LandmarkRemark.Services.Authentication
{
    /// <summary>
    /// Provides authentication functions for Landmark Remark.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Creates a new instance of AuthenticationService.
        /// </summary>
        /// <param name="userRepository">The users repository.</param>
        /// <param name="tokenService">The token service.</param>
        public AuthenticationService(IUserRepository userRepository, ITokenService tokenService)
        {
            this._userRepository = userRepository;
            this._tokenService = tokenService;
        }

        /// <summary>
        /// Generates an auth token.
        /// </summary>
        /// <param name="loginDetails">The login details of the user.</param>
        /// <returns>The auth token.</returns>
        public async Task<AuthTokenDTO?> GenerateAuthToken(LoginDetailsDTO loginDetails)
        {
            // TODO: Handle passwords securely using a hash here.

            // Make sure a user exists with the specified email address and password.
            var user = await this._userRepository.FindByEmailAddress(loginDetails.EmailAddress);

            if (user == null || user.Password != loginDetails.Password)
            {
                throw new LoginDetailsIncorrectException(loginDetails.EmailAddress);
            }

            return this._tokenService.Generate(user);
        }
    }
}