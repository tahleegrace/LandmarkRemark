using System.Security.Claims;

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

        /// <summary>
        /// Gets the ID of the currently logged in user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The ID of the currently logged in user.</returns>
        int? GetCurrentUserId(ClaimsPrincipal user);
    }
}