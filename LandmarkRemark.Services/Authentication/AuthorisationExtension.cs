using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LandmarkRemark.Services.Authentication
{
    // SOURCE: https://referbruv.com/blog/getting-started-with-securing-apis-using-jwt-bearer-authentication-hands-on/
    public static class AuthorisationExtension
    {
        // Extension method for Adding 
        // JwtBearer Middleware to the Pipeline
        public static IServiceCollection AddBearerAuthentication(
            this IServiceCollection services, IConfiguration configuration)
        {
            var validationParams = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Authentication:AuthToken:Key"])),
                ValidIssuer = configuration["Authentication:AuthToken:Issuer"],
                ValidAudience = configuration["Authentication:AuthToken:Audience"]
            };

            var events = new JwtBearerEvents()
            {
                OnForbidden = (context) =>
                {
                    return Task.CompletedTask;
                },

                // invoked when the token validation fails
                OnAuthenticationFailed = (context) =>
                {
                    Console.WriteLine(context.Exception);
                    return Task.CompletedTask;
                },

                // invoked when a request is received
                OnMessageReceived = (context) =>
                {
                    return Task.CompletedTask;
                },

                // invoked when token is validated
                OnTokenValidated = (context) =>
                {
                    return Task.CompletedTask;
                }
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme
                    = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme
                    = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = validationParams;
                options.Events = events;
            });

            return services;
        }
    }
}