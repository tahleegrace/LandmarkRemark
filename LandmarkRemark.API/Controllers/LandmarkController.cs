using Microsoft.AspNetCore.Mvc;

using LandmarkRemark.Models.DTOs.Landmarks;
using LandmarkRemark.Services.Landmarks;

namespace LandmarkRemark.API.Controllers
{
    /// <summary>
    /// Provides functionality for managing landmarks.
    /// </summary>
    [Route("api/landmarks")]
    [ApiController]
    public class LandmarkController : ControllerBase
    {
        private readonly ILandmarkService _landmarkService;

        /// <summary>
        /// Creates a new instance of LandmarkController.
        /// </summary>
        /// <param name="landmarkService">The landmark service.</param>
        public LandmarkController(ILandmarkService landmarkService)
        {
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
            return Ok(await this._landmarkService.Create(request));
        }

        /// <summary>
        /// Finds the landmarks for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The landmarks for the specified user.</returns>
        [HttpGet("my-landmarks")]
        public async Task<ActionResult<List<LandmarkDTO>>> FindByUserId(int userId)
        {
            return Ok(await this._landmarkService.FindByUserId(userId));
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