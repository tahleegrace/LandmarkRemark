using Microsoft.AspNetCore.Mvc;

namespace LandmarkRemark.API.Controllers
{
    /// <summary>
    /// Provides functionality for managing landmarks.
    /// </summary>
    [Route("api/landmarks")]
    [ApiController]
    public class LandmarkController : ControllerBase
    {
        [HttpGet("test")]
        public async Task<ActionResult<string>> Test()
        {
            // TODO: Remove this method once a working landmark endpoint is implemented.
            return "This is a test";
        }
    }
}