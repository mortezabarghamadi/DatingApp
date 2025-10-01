using Data.Context;
using DatingApp.Api.Services.Implementation;
using DatingApp.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DatingAppContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DatingAppConnectionString"));
            });
            return services;
        }
    }
}
