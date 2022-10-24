namespace LandmarkRemark.Models.DTOs.Authentication
{
    // Adapted from https://referbruv.com/blog/getting-started-with-securing-apis-using-jwt-bearer-authentication-hands-on/

    /// <summary>
    /// An auth token used for calling Landmark Remark APIs.
    /// </summary>
    public class AuthTokenDTO
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets in how many minutes the token expires.
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}