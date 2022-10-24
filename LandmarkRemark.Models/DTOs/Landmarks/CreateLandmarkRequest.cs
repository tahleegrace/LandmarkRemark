namespace LandmarkRemark.Models.DTOs.Landmarks 
{
    /// <summary>
    /// A request for creating a new landmark.
    /// </summary>
    public class CreateLandmarkRequest
    {
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
    }
}