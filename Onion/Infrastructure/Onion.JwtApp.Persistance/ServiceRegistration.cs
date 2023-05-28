using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onion.JwpApp.Application.Interfaces;
using Onion.JwtApp.Persistance.Contexts;
using Onion.JwtApp.Persistance.Repositories;

namespace Onion.JwtApp.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServcies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OnionContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Local"));
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
