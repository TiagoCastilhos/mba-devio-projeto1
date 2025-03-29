using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using SuperStore.Application.Extensions;
using SuperStore.Authorization.Extensions;
using SuperStore.CrossCutting.Options;
using SuperStore.Data.Extensions;

namespace SuperStore.API;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SuperStore API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        builder.Services.AddEndpointsApiExplorer();

        var environmentOptions = new EnvironmentOptions()
        {
            EnvironmentName = builder.Environment.EnvironmentName
        };

        builder.Services.AddData(builder.Configuration, environmentOptions);
        builder.Services.AddApplication();
        builder.Services.AddApiAuthorization(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SuperStore API v1"));
        }

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        await ProvideInfrastructureAsync(environmentOptions, builder.Services);
        await app.RunAsync();
    }

    private static async Task ProvideInfrastructureAsync(EnvironmentOptions environmentOptions, IServiceCollection services)
    {
        if (!environmentOptions.IsDevelopment())
            return;

        await services.CreateDatabaseIfNotExistsAsync();
    }
}