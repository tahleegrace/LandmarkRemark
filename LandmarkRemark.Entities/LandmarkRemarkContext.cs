using Microsoft.EntityFrameworkCore;

using LandmarkRemark.Entities.Configuration;

namespace LandmarkRemark.Entities
{
    /// <summary>
    /// The Landmark Remark database context.
    /// </summary>
    public class LandmarkRemarkContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Landmark> Landmarks { get; set; }

        /// <summary>
        /// Creates a new instance of LandmarkRemarkContext.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public LandmarkRemarkContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(this._configuration.GetConnectionString("LandmarkRemarkContext"), x => x.UseNetTopologySuite());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}