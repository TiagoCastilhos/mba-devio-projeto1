using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.InputModels;
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

    [Authorize]
    [HttpGet("Index")]
    public async Task<IActionResult> IndexAsync()
    {
        var products = await _productsService.GetAsync(Request.HttpContext.RequestAborted);

        return View(products.Select(p => new ProductViewModel(p)));
    }

    [Authorize]
    [HttpGet("Create")]
    public async Task<IActionResult> CreateAsync()
    {
        var categories = await _categoriesService.GetAsync(Request.HttpContext.RequestAborted);
        ViewData["Categories"] = categories;

        return View();
    }

    [Authorize]
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

        await _productsService.CreateAsync(inputModel, Request.HttpContext.RequestAborted);
        return RedirectToAction("Index", "Products");
    }

    [Authorize]
    [HttpGet("Update")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    [HttpPost("Edit")]
    public async Task<IActionResult> EditAsync(ProductViewModel viewModel)
    {
        var inputModel = new UpdateProductInputModel(viewModel.Id, viewModel.Name, viewModel.Description, viewModel.Price,
            viewModel.Quantity, viewModel.ImageUrl, viewModel.Category);

        var validationResult = await _updateProductValidator.ValidateAsync(inputModel, Request.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            viewModel.SetErrors(errors);

            return View(viewModel);
        }

        await _productsService.UpdateAsync(inputModel, Request.HttpContext.RequestAborted);
        return RedirectToAction("Index", "Products");
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productsService.DeleteAsync(id, CancellationToken.None);
        return RedirectToAction("Index", "Products");
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