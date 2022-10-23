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

        /// <summary>
        /// Finds the landmarks for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The landmarks for the specified user.</returns>
        Task<List<LandmarkDTO>> FindByUserId(int userId);

        /// <summary>
        /// Returns all landmarks that have been added.
        /// </summary>
        /// <returns>All landmarks that have been added.</returns>
        Task<List<LandmarkDTO>> FindAll();

        /// <summary>
        /// Finds the landmarks matching the specified search query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The landmarks matching the specified search query.</returns>
        Task<List<LandmarkDTO>> Search(string query);
    }
}