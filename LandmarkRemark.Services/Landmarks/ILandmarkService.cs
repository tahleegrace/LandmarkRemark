using LandmarkRemark.Models.Landmarks;

namespace LandmarkRemark.Services.Landmarks
{
    /// <summary>
    /// A service that provides functionality for managing landmarks.
    /// </summary>
    public interface ILandmarkService
    {
        /// <summary>
        /// Creates a new landmark.
        /// </summary>
        /// <param name="request">The landmark.</param>
        /// <returns>The newly created landmark.</returns>
        Task<LandmarkDTO> Create(CreateLandmarkRequest request);
    }
}