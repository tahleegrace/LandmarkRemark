using LandmarkRemark.Models.DTOs.Authentication;

namespace LandmarkRemark.Services.Authentication
{
    /// <summary>
    /// A service that provides authentication functions.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Generates an auth token.
        /// </summary>
        /// <param name="loginDetails">The login details of the user.</param>
        /// <returns>The auth token.</returns>
        Task<AuthTokenDTO?> GenerateAuthToken(LoginDetailsDTO loginDetails);
    }
}