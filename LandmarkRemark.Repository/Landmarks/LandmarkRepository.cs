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
            // Update the timestamps on the landmark.
            landmark.Created = DateTime.UtcNow;
            landmark.Updated = DateTime.UtcNow;

            // Save the landmark.
            this._context.Landmarks.Add(landmark);

            await this._context.SaveChangesAsync();

            // Get the user who created the landmark. .Include isn't supported when adding a new record.
            var user = await this._context.Users.Where(u => u.Id == landmark.UserId).FirstOrDefaultAsync();
            landmark.User = user;

            return landmark;
        }

        /// <summary>
        /// Finds the landmarks for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The landmarks for the specified user.</returns>
        public async Task<List<Landmark>> FindByUserId(int userId)
        {
            return await this._context.Landmarks.Include(l => l.User)
                                                .Where(l => l.UserId == userId && !l.Deleted)
                                                .ToListAsync();
        }

        /// <summary>
        /// Returns all landmarks that have been added.
        /// </summary>
        /// <returns>All landmarks that have been added.</returns>
        public async Task<List<Landmark>> FindAll()
        {
            return await this._context.Landmarks.Include(l => l.User)
                                                .Where(l => !l.Deleted)
                                                .ToListAsync();
        }
    }
}