using Microsoft.AspNetCore.Mvc;
using SuperStore.Application.Abstractions.Services;

namespace SuperStore.MVC.Controllers;

public class ProductsController : Controller
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    public async Task<IActionResult> GetAsync()
    {
        var products = await _productsService.GetAsync(Request.HttpContext.RequestAborted);

        return View(products);
    }
}