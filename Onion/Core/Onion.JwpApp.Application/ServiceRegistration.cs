using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Onion.JwpApp.Application.Mappings;
using System.Reflection;

namespace Onion.JwpApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ProductProfile), typeof(AppUserProfile));
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
