using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperStore.CrossCutting.Options;
using SuperStore.Data.Abstractions.Contexts;
using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Data.Contexts;
using SuperStore.Data.Repositories;

namespace SuperStore.Data.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(
        this IServiceCollection services, IConfiguration configuration, EnvironmentOptions environment)
    {
        services.AddDbContext<ISuperStoreDbContext, SuperStoreDbContext>(options =>
        {
            if (environment.IsDevelopment())
                options.UseSqlite(configuration.GetConnectionString("SuperStoreDb"));
            else
                options.UseSqlServer(configuration.GetConnectionString("SuperStoreDb"));
        });

        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<ISellersRepository, SellersRepository>();
        
        return services;
    }

    public static async Task CreateDatabaseIfNotExistsAsync(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();

        var context = serviceProvider.GetRequiredService<ISuperStoreDbContext>();
        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();
    }
}
