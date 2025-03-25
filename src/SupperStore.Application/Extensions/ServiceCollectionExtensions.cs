using SupperStore.Application.Abstractions.Services;
using SupperStore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SupperStore.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<ICategoriesService, CategoriesService>();
        services.AddScoped<ISellersService, SellersService>();

        return services;
    }
}
