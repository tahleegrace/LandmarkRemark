using LandmarkRemark.Services.Authentication;
using LandmarkRemark.Services.Landmarks;

namespace LandmarkRemark.Services
{
    public static class DependencySetup
    {
        public static void AddLandmarkRemarkServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ILandmarkService, LandmarkService>();
        }
    }
}