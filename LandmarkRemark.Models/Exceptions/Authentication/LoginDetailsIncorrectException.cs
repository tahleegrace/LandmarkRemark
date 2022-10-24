namespace LandmarkRemark.Models.Exceptions.Authentication
{
    /// <summary>
    /// An exception that occurs when a user's login details are incorrect.
    /// </summary>
    public class LoginDetailsIncorrectException : Exception
    {
        /// <summary>
        /// Gets the email address of the user.
        /// </summary>
        public string EmailAddress { get; }

        /// <summary>
        /// Creates a new instance of LoginDetailsIncorrectException.
        /// </summary>
        /// <param name="emailAddress">The email address of the user.</param>
        public LoginDetailsIncorrectException(string emailAddress)
        {
            this.EmailAddress = emailAddress;
        }
    }
}