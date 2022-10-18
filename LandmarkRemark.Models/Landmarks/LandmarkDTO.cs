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
        public decimal Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the notes about the landmark.
        /// </summary>
        public string Notes { get; set; }
    }
}