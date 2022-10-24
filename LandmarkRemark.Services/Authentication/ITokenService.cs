using LandmarkRemark.Entities;
using LandmarkRemark.Models.DTOs.Authentication;

namespace LandmarkRemark.Services.Authentication
{
    /// <summary>
    /// A service that generates authentication tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates an authentication token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>An authentication token for the specified user.</returns>
        AuthTokenDTO Generate(User user);
    }
}