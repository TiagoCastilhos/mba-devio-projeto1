using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core.Abstractions.Services;
using SuperStore.MVC.Models;
using SuperStore.MVC.ViewModels.Products;

namespace SuperStore.MVC.Controllers;

public class HomeController : Controller
{
    private readonly IProductsService _productsService;

    public HomeController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet("")]
    public async Task<IActionResult> IndexAsync()
    {
        var products = await _productsService.ShowcaseAsync(Request.HttpContext.RequestAborted);

        return View(products.Select(p => new ProductViewModel(p)));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
