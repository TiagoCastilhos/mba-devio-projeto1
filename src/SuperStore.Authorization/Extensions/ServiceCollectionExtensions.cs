using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SuperStore.Authorization.Abstractions.Services;
using SuperStore.Authorization.InputModels.Validators;
using SuperStore.Authorization.Services;
using SuperStore.Data.Contexts;
using IdentityOptions = SuperStore.Authorization.Options.IdentityOptions;

namespace SuperStore.Authorization.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
    {
        services
            .AddDefaultIdentity<IdentityUser>()
            .AddEntityFrameworkStores<SuperStoreDbContext>();
        //TODO Localize error messages
        //https://learn.microsoft.com/en-us/answers/questions/549950/localize-aspnetcore-identity-error-messages

        services.Configure<Microsoft.AspNetCore.Identity.IdentityOptions>(ConfigureIdentityOptions);

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            options.LoginPath = "/Identities/SignIn";
            options.AccessDeniedPath = "/Identities/AccessDenied";
            options.SlidingExpiration = true;
        });

        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<ISignInService, SignInService>();
        services.AddValidatorsFromAssemblyContaining<CreateUserInputModelValidator>();

        return services;
    }

    public static IServiceCollection AddApiAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var identityOptions = new IdentityOptions();
        configuration.GetSection(IdentityOptions.SectionName).Bind(identityOptions);

        services.AddSingleton(identityOptions);

        services
            .AddIdentityCore<IdentityUser>(ConfigureIdentityOptions)
            .AddEntityFrameworkStores<SuperStoreDbContext>()
            .AddSignInManager();

        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IIdentitiesService, IdentitiesService>();
        services.AddValidatorsFromAssemblyContaining<CreateUserInputModelValidator>();

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

    private static void ConfigureIdentityOptions(Microsoft.AspNetCore.Identity.IdentityOptions options)
    {
        options.SignIn.RequireConfirmedAccount = false;

        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        options.User.RequireUniqueEmail = true;
    }
}
