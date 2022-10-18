using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using NetTopologySuite.Geometries;

namespace LandmarkRemark.Entities
{
    /// <summary>
    /// A saved landmark.
    /// </summary>
    public class Landmark : BaseEntity
    {
        /// <summary>
        /// Gets or sets the ID of the landmark.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the location of the landmark.
        /// </summary>
        // Note: I've chosen to use the built-in SQL spatial data types as this provides a lot more f
        public Point Location { get; set; }

        /// <summary>
        /// Gets or sets the notes saved against the landmark.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the user who created the landmark.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the landmark.
        /// </summary>
        public int UserId { get; set; }
    }
}