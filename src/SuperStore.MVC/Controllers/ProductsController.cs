using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.InputModels;
using SuperStore.MVC.ViewModels.Products;

namespace SuperStore.MVC.Controllers;

[Route("Products")]
public class ProductsController : Controller
{
    private readonly IProductsService _productsService;
    private readonly IValidator<CreateProductInputModel> _createProductValidator;
    private readonly IValidator<UpdateProductInputModel> _updateProductValidator;

    public ProductsController(
        IProductsService productsService,
        IValidator<CreateProductInputModel> createProductValidator,
        IValidator<UpdateProductInputModel> updateProductValidator)
    {
        _productsService = productsService;
        _createProductValidator = createProductValidator;
        _updateProductValidator = updateProductValidator;
    }

    [HttpGet("Showcase")]
    public async Task<IActionResult> ShowcaseAsync()
    {
        var products = await _productsService.GetAsync(Request.HttpContext.RequestAborted);

        return View(products.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CreatedOn = p.CreatedOn,
            UpdatedOn = p.UpdatedOn,
            Quantity = p.Quantity
        }));
    }

    [Authorize]
    [HttpGet("Index")]
    public async Task<IActionResult> IndexAsync()
    {
        //get products from seller
        var userIdClaim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);

        var products = await _productsService.GetAsync(userIdClaim.Value, CancellationToken.None);

        //create a ctor for ProductViewModel that receives a ProductOutputModel
        return View(products.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CreatedOn = p.CreatedOn,
            UpdatedOn = p.UpdatedOn,
            Quantity = p.Quantity
        }));
    }

    [Authorize]
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> CreateAsync(ProductViewModel viewModel)
    {
        var inputModel = new CreateProductInputModel(viewModel.Name, viewModel.Description, viewModel.Price,
            viewModel.Quantity, viewModel.ImageUrl, viewModel.CategoryId);

        var validationResult = await _createProductValidator.ValidateAsync(inputModel, CancellationToken.None);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            viewModel.SetErrors(errors);

            return View(viewModel);
        }

        await _productsService.CreateAsync(inputModel, CancellationToken.None);

        return View(viewModel);
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
            viewModel.Quantity, viewModel.ImageUrl, viewModel.CategoryId);

        var validationResult = await _updateProductValidator.ValidateAsync(inputModel, CancellationToken.None);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            viewModel.SetErrors(errors);

            return View(viewModel);
        }

        await _productsService.UpdateAsync(inputModel, CancellationToken.None);

        return View(viewModel);
    }
}