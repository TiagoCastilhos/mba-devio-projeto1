using System.Text.Json;
using System.Text.Json.Serialization;
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

        //builder.Services.AddAuthentication(opt =>
        //{
        //    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        //}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        //    options =>
        //    {
        //        options.MetadataAddress = builder.Configuration.GetValue<string>("AuthenticationMetadataAddress");

        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateAudience = false,
        //            ClockSkew = TimeSpan.Zero,
        //            ValidIssuers = builder.Configuration.GetValue<string>("AuthenticationValidIssuers")!.Split(';')
        //        };

        //        options.AddIntegrationTestsTokenSupport(options.TokenValidationParameters, builder.Configuration);
        //    });

        builder.Services.AddEndpointsApiExplorer();

        var environmentOptions = new EnvironmentOptions()
        {
            EnvironmentName = builder.Environment.EnvironmentName
        };

        builder.Services.AddData(builder.Configuration, environmentOptions);
        builder.Services.AddApplication();
        builder.Services.AddApplicationAuthorization();

        var app = builder.Build();

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