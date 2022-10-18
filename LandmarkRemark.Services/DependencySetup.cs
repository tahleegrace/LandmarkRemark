using LandmarkRemark.Services.Landmarks;

namespace LandmarkRemark.Services
{
    public static class DependencySetup
    {
        public static void AddLandmarkRemarkServices(this IServiceCollection services)
        {
            services.AddScoped<ILandmarkService, LandmarkService>();
        }
    }
}