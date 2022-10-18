using Microsoft.AspNetCore.Mvc;

using LandmarkRemark.Models.Landmarks;
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
    }
}