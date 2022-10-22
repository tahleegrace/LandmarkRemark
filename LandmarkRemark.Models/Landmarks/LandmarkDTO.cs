namespace LandmarkRemark.Models.Landmarks
{
    /// <summary>
    /// DTO object for returning landmarks from the API.
    /// </summary>
    public class LandmarkDTO
    {
        /// <summary>
        /// Gets or sets the ID of the landmark.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the notes about the landmark.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user who created the landmark.
        /// </summary>
        public string UserFullName { get; set; }
    }
}