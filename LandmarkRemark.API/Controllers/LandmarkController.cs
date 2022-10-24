using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using LandmarkRemark.Models.DTOs.Landmarks;
using LandmarkRemark.Models.Exceptions.Landmarks;
using LandmarkRemark.Services.Landmarks;
using LandmarkRemark.Services.Authentication;

namespace LandmarkRemark.API.Controllers
{
    /// <summary>
    /// Provides functionality for managing landmarks.
    /// </summary>
    [Route("api/landmarks")]
    [ApiController]
    [Authorize]
    public class LandmarkController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILandmarkService _landmarkService;

        /// <summary>
        /// Creates a new instance of LandmarkController.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="landmarkService">The landmark service.</param>
        public LandmarkController(IAuthenticationService authenticationService, ILandmarkService landmarkService)
        {
            this._authenticationService = authenticationService;
            this._landmarkService = landmarkService;
        }

        /// <summary>
        /// Creates a new landmark.
        /// </summary>
        /// <param name="request">The landmark.</param>
        /// <returns>The newly created landmark.</returns>
        [HttpPost]
        public async Task<ActionResult<LandmarkDTO>> Create(CreateLandmarkRequest request)
        {
            try
            {
                var currentUserId = this._authenticationService.GetCurrentUserId(User);

                if (currentUserId != null)
                {
                    return Ok(await this._landmarkService.Create(request, currentUserId.Value));
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (LandmarkNotProvidedException)
            {
                return BadRequest("Please provide a landmark to create.");
            }
        }

        /// <summary>
        /// Finds the landmarks for the specified user.
        /// </summary>
        /// <returns>The landmarks for the specified user.</returns>
        [HttpGet("my-landmarks")]
        public async Task<ActionResult<List<LandmarkDTO>>> FindMyLandmarks()
        {
            var currentUserId = this._authenticationService.GetCurrentUserId(User);

            if (currentUserId != null)
            {
                return Ok(await this._landmarkService.FindMyLandmarks(currentUserId.Value));
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Finds all landmarks.
        /// </summary>
        /// <returns>All landmarks.</returns>
        [HttpGet]
        public async Task<ActionResult<List<LandmarkDTO>>> FindAll()
        {
            return Ok(await this._landmarkService.FindAll());
        }

        /// <summary>
        /// Finds the landmarks matching the specified search query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The landmarks matching the specified search query.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<List<LandmarkDTO>>> Search(string query)
        {
            return Ok(await this._landmarkService.Search(query));
        }
    }
}