using LandmarkRemark.Repository.Landmarks;

namespace LandmarkRemark.Repository
{
    public static class DependencySetup
    {
        public static void AddLandmarkRemarkRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILandmarkRepository, LandmarkRepository>();
        }
    }
}