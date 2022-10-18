namespace LandmarkRemark.Models.Landmarks
{
    /// <summary>
    /// A request for creating a new landmark.
    /// </summary>
    public class CreateLandmarkRequest
    {
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the notes about the landmark.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the landmark.
        /// </summary>
        // TODO: This should be worked out based on who is currently logged in.
        public int UserId { get; set; }
    }
}