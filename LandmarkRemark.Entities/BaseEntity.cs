namespace LandmarkRemark.Entities
{
    /// <summary>
    /// An entity in Landmark Remark.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// The date the entity was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The date the entity was last modified.
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Gets or sets whether the entity has been deleted.
        /// </summary>
        public bool Deleted { get; set; } = false;
    }
}