using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperStore.Core.Abstractions.Services;
using SuperStore.Core.InputModels;
using SuperStore.Data.Abstractions.Contexts;
using SuperStore.Data.Entities;

namespace SuperStore.Core.Seeds
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(WebApplication application)
        {
            using var scope = application.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ISuperStoreDbContext>();
            var usersService = services.GetRequiredService<IUsersService>();

            await context.Database.MigrateAsync();

            if (context.Sellers.Any())
                return;

            await usersService.CreateAsync(new CreateUserInputModel("test@test.com", "test01", "Senha123@"), CancellationToken.None);

            var seller = await context.Sellers.FirstAsync();

            var clothesCategory = new Category("Roupas", seller);
            var shoesCategory = new Category("Calçados", seller);
            var winterCategory = new Category("Inverno", seller);
            var accessoriesCategory = new Category("Acessórios", seller);

            await context.Categories.AddRangeAsync(clothesCategory, shoesCategory, winterCategory, accessoriesCategory);

            var tShirt = new Product("Camiseta", "Camiseta de algodão", 19.99m, 10, "camiseta.webp", seller, clothesCategory);
            var jeans = new Product("Calça Jeans", "Calça jeans masculina", 49.99m, 5, "jeans.webp", seller, clothesCategory);
            var sneakers = new Product("Tênis", "Tênis esportivo", 89.99m, 8, "tenis.webp", seller, shoesCategory);
            var winterJacket = new Product("Jaqueta de Inverno", "Jaqueta de inverno impermeável", 129.99m, 3, "jaqueta.webp", seller, winterCategory);
            var scarf = new Product("Cachecol", "Cachecol de lã", 29.99m, 15, "cachecol.webp", seller, accessoriesCategory);

            await context.Products.AddRangeAsync(tShirt, jeans, sneakers, winterJacket, scarf);

            await context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
