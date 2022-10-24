using Microsoft.AspNetCore.Mvc;

using LandmarkRemark.Models.DTOs.Authentication;
using LandmarkRemark.Models.Exceptions.Authentication;
using LandmarkRemark.Services.Authentication;

namespace LandmarkRemark.API.Controllers
{
    /// <summary>
    /// Provides authentication functionality.
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Creates a new instance of AuthenticationController.
        /// </summary>
        /// <param name="tokenService">The token service.</param>
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        /// <summary>
        /// Generates an auth token.
        /// </summary>
        /// <param name="loginDetails">The login details of the user.</param>
        /// <returns>The auth token.</returns>
        [HttpPost]
        public async Task<ActionResult<AuthTokenDTO>> GenerateAuthToken(LoginDetailsDTO loginDetails)
        {
            if (loginDetails == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await this._authenticationService.GenerateAuthToken(loginDetails);

                return result;
            }
            catch (LoginDetailsIncorrectException)
            {
                return Unauthorized("Email address or password invalid.");
            }
        }
    }
}