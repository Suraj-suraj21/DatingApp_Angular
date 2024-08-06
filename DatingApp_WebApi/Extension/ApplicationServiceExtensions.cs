using AngularAPI.Data;
using DatingApp_WebApi.Interfaces;
using DatingApp_WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_WebApi.Extension
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();

            services.AddDbContext<Datacontext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors();

            services.AddScoped<ITokenService, TokenService>();

            return services;

        }
    }
}
