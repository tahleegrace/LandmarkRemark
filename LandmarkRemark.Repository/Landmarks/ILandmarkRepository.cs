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
    }
}