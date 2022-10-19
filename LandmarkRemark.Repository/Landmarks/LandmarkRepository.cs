using Microsoft.EntityFrameworkCore;

using LandmarkRemark.Entities;

namespace LandmarkRemark.Repository.Landmarks
{
    /// <summary>
    /// The landmarks repository.
    /// </summary>
    public class LandmarkRepository : ILandmarkRepository
    {
        private readonly LandmarkRemarkContext _context;

        /// <summary>
        /// Creates a new instance of LandmarkRepository.
        /// </summary>
        /// <param name="context">The context.</param>
        public LandmarkRepository(LandmarkRemarkContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Creates a new landmark.
        /// </summary>
        /// <param name="landmark">The landmark.</param>
        /// <returns>The new landmark.</returns>
        public async Task<Landmark> Create(Landmark landmark)
        {
            landmark.Created = DateTime.UtcNow;
            landmark.Updated = DateTime.UtcNow;

            this._context.Landmarks.Add(landmark);

            await this._context.SaveChangesAsync();

            return landmark;
        }

        /// <summary>
        /// Finds the landmarks for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The landmarks for the specified user.</returns>
        public async Task<List<Landmark>> FindByUserId(int userId)
        {
            return await this._context.Landmarks.Where(l => l.UserId == userId && !l.Deleted).ToListAsync();
        }
    }
}