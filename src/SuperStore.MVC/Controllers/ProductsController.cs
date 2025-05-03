using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core.Abstractions.Services;
using SuperStore.Core.Exceptions;
using SuperStore.Core.InputModels;
using SuperStore.MVC.ViewModels.Products;

namespace SuperStore.MVC.Controllers;

[Authorize]
[Route("Products")]
public class ProductsController : Controller
{
    private readonly IProductsService _productsService;
    private readonly ICategoriesService _categoriesService;
    private readonly IValidator<CreateProductInputModel> _createProductValidator;
    private readonly IValidator<UpdateProductInputModel> _updateProductValidator;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ProductsController(
        IProductsService productsService,
        ICategoriesService categoriesService,
        IValidator<CreateProductInputModel> createProductValidator,
        IValidator<UpdateProductInputModel> updateProductValidator,
        IWebHostEnvironment hostingEnvironment)
    {
        _productsService = productsService;
        _categoriesService = categoriesService;
        _createProductValidator = createProductValidator;
        _updateProductValidator = updateProductValidator;
        _hostingEnvironment = hostingEnvironment;
    }

    [AllowAnonymous]
    [HttpGet("Showcase")]
    public async Task<IActionResult> ShowcaseAsync()
    {
        var products = await _productsService.ShowcaseAsync(Request.HttpContext.RequestAborted);

        return View("Index", products.Select(p => new ProductViewModel(p)));
    }

    [HttpGet("Index")]
    public async Task<IActionResult> IndexAsync()
    {
        var products = await _productsService.GetAsync(null, Request.HttpContext.RequestAborted);

        return View(products.Select(p => new ProductViewModel(p)));
    }

    [HttpGet("Create")]
    public async Task<IActionResult> CreateAsync()
    {
        await FillCategoriesAsync();
        return View();
    }

    [HttpPost("Create")]
    [RequestSizeLimit(4 * 1024 * 1024)]
    public async Task<IActionResult> CreateAsync(ProductViewModel viewModel, IFormFile? image)
    {
        var inputModel = new CreateProductInputModel(viewModel.Name, viewModel.Description, viewModel.Price,
            viewModel.Quantity, viewModel.ImageUrl, viewModel.Category);

        var validationResult = await _createProductValidator.ValidateAsync(inputModel, Request.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            viewModel.SetErrors(errors);

            return View(viewModel);
        }

        if (image != null)
        {
            var imagePath = await UploadImageAsync(image);
            inputModel.ImageUrl = imagePath;
        }

        try
        {
            await _productsService.CreateAsync(inputModel, Request.HttpContext.RequestAborted);
            TempData["SuccessMessage"] = $"Produto '{viewModel.Name}' criado com sucesso!";
        }
        catch (ServiceApplicationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Index", "Products");
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> EditAsync(Guid id)
    {
        var product = await _productsService.GetAsync(id, Request.HttpContext.RequestAborted);

        if (product == null)
            return RedirectToAction("Index", "Products");

        await FillCategoriesAsync();

        return View(new ProductViewModel(product));
    }

    [HttpPost("Edit")]
    [RequestSizeLimit(4 * 1024 * 1024)]
    public async Task<IActionResult> EditAsync(ProductViewModel viewModel, IFormFile? image)
    {
        var inputModel = new UpdateProductInputModel(viewModel.Id, viewModel.Name, viewModel.Description, viewModel.Price,
            viewModel.Quantity, viewModel.ImageUrl, viewModel.Category);

        var validationResult = await _updateProductValidator.ValidateAsync(inputModel, Request.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            viewModel.SetErrors(errors);

            await FillCategoriesAsync();
            return View(viewModel);
        }

        if (image != null)
        {
            var imagePath = await UploadImageAsync(image);
            inputModel.ImageUrl = imagePath;
        }

        try
        {
            await _productsService.UpdateAsync(inputModel, Request.HttpContext.RequestAborted);
            TempData["SuccessMessage"] = $"Produto '{viewModel.Name}' editado com sucesso!";
        }
        catch (ServiceApplicationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Index", "Products");
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await _productsService.DeleteAsync(id, CancellationToken.None);
            TempData["SuccessMessage"] = $"Produto deletado com sucesso!";
        }
        catch (ServiceApplicationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Index", "Products");
    }

    private async Task FillCategoriesAsync()
    {
        var categories = await _categoriesService.GetAsync(Request.HttpContext.RequestAborted);
        ViewData["Categories"] = categories;
    }

    private async Task<string> UploadImageAsync(IFormFile image)
    {
        var imageUrl = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageUrl);

        using var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write);
        using var imageStream = image.OpenReadStream();

        await imageStream.CopyToAsync(fileStream, Request.HttpContext.RequestAborted);
        return imageUrl;
    }
}