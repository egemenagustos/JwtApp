using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Onion.JwpApp.Application.Features.CQRS.Commands;
using Onion.JwpApp.Application.Mappings;
using Onion.JwpApp.Application.ValidationRules.Categories;
using Onion.JwpApp.Application.ValidationRules.Products;
using System.Reflection;

namespace Onion.JwpApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ProductProfile), typeof(AppUserProfile));
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IValidator<CreateCategoryCommandRequest>, CategoryCreateValidator>();
            services.AddTransient<IValidator<UpdateCategoryCommandRequest>, CategoryUpdateValidator>();

            services.AddTransient<IValidator<CreateProductCommandRequest>, ProductCreateValidator>();
            services.AddTransient<IValidator<UpdateProductCommandRequest>, ProductUpdateValidator>();

        }
    }
}
