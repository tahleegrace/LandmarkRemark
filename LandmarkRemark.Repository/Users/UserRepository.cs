using Microsoft.EntityFrameworkCore;

using LandmarkRemark.Entities;

namespace LandmarkRemark.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private LandmarkRemarkContext _context { get; set; }

        /// <summary>
        /// Creates a new instance of UserRepository.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UserRepository(LandmarkRemarkContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Finds the user with the specified email address.
        /// </summary>
        /// <param name="emailAddress">The email address of the user.</param>
        /// <returns>The user with the specified email address.</returns>
        public async Task<User?> FindByEmailAddress(string emailAddress)
        {
            var user = await this._context.Users.Where(u => u.EmailAddress == emailAddress && !u.Deleted)
                                                .FirstOrDefaultAsync();

            return user;
        }
    }
}