namespace LandmarkRemark.Models.DTOs.Authentication
{
    /// <summary>
    /// Stores an email address and password for logging in to Landmark Remark.
    /// </summary>
    public class LoginDetailsDTO
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}