using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.InputModels.Validators;
using SuperStore.Application.Services;

namespace SuperStore.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<ICategoriesService, CategoriesService>();
        services.AddScoped<ISellersService, SellersService>();
        services.AddValidatorsFromAssemblyContaining<CreateCategoryInputModelValidator>();
        services.AddFluentValidationAutoValidation();

        return services;
    }
}
