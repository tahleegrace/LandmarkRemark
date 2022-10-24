using LandmarkRemark.Entities;

namespace LandmarkRemark.Repository.Users
{
    /// <summary>
    /// A users repository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Finds the user with the specified email address.
        /// </summary>
        /// <param name="emailAddress">The email address of the user.</param>
        /// <returns>The user with the specified email address.</returns>
        Task<User?> FindByEmailAddress(string emailAddress);
    }
}