using SuperStore.Core.Extensions;
using SuperStore.Core.Seeds;
using SuperStore.Data.Extensions;
using SuperStore.Data.Options;

namespace SuperStore.MVC;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();

        var environmentOptions = new EnvironmentOptions()
        {
            EnvironmentName = builder.Environment.EnvironmentName
        };

        builder.Services.AddRazorPages();

        builder.Services.AddData(builder.Configuration, environmentOptions);
        builder.Services.AddApplication();
        builder.Services.AddAppAuthorization();

        var app = builder.Build();

        if (!environmentOptions.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();
        app.UseStaticFiles();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        await DbSeeder.SeedAsync(app);
        await app.RunAsync();
    }
}