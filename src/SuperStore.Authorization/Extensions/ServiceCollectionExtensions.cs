using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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

    public static IServiceCollection AddApiAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var identityOptions = new IdentityOptions();
        configuration.GetSection(IdentityOptions.SectionName).Bind(identityOptions);

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

        services
            .AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(identityOptions.SigningKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = identityOptions.Audience,
                    ValidIssuer = identityOptions.Issuer
                };
            });

        return services;
    }
}
