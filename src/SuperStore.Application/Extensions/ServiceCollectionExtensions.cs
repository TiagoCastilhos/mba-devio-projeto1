using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SuperStore.Application.Extensions;
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
