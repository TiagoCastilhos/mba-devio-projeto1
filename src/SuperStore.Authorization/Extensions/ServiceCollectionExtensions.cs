using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SuperStore.Data.Contexts;

namespace SuperStore.Authorization.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
    {
        services
            .AddIdentityCore<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<SuperStoreDbContext>();

        return services;
    }
}
