using LandmarkRemark.Repository.Landmarks;
using LandmarkRemark.Repository.Users;

namespace LandmarkRemark.Repository
{
    public static class DependencySetup
    {
        public static void AddLandmarkRemarkRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ILandmarkRepository, LandmarkRepository>();
        }
    }
}