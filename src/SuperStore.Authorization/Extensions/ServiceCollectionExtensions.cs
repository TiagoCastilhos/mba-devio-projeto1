using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SuperStore.Authorization.Abstractions.Services;
using SuperStore.Authorization.Services;
using SuperStore.Data.Contexts;
using IdentityOptions = SuperStore.Authorization.Options.IdentityOptions;

namespace SuperStore.Authorization.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
    {
        services
            .AddIdentityCore<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<SuperStoreDbContext>()
            .AddSignInManager();

        services.AddScoped<IUsersService, UsersService>();

        return services;
    }

    public static IServiceCollection AddApiAuthorization(this IServiceCollection services, IdentityOptions identityOptions)
    {
        services.AddSingleton(identityOptions);

        services
            .AddIdentityCore<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<SuperStoreDbContext>()
            .AddSignInManager();

        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IIdentitiesService, IdentitiesService>();

        return services;
    }
}
