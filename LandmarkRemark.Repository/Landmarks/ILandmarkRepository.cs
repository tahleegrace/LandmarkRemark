using LandmarkRemark.Entities;

namespace LandmarkRemark.Repository.Landmarks
{
    /// <summary>
    /// A landmarks repository.
    /// </summary>
    public interface ILandmarkRepository
    {
        /// <summary>
        /// Creates a new landmark.
        /// </summary>
        /// <param name="landmark">The landmark.</param>
        /// <returns>The new landmark.</returns>
        Task<Landmark> Create(Landmark landmark);

        /// <summary>
        /// Finds the landmarks for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The landmarks for the specified user.</returns>
        Task<List<Landmark>> FindByUserId(int userId);

        /// <summary>
        /// Returns all landmarks that have been added.
        /// </summary>
        /// <returns>All landmarks that have been added.</returns>
        Task<List<Landmark>> FindAll();
    }
}