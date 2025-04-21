using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core.Abstractions.Services;
using SuperStore.Core.InputModels;
using SuperStore.MVC.ViewModels.Categories;

namespace SuperStore.MVC.Controllers;

[Authorize]
[Route("Categories")]
public class CategoriesController : Controller
{
    private readonly ICategoriesService _categoriesService;
    private readonly IValidator<CreateCategoryInputModel> _createCategoryValidator;
    private readonly IValidator<UpdateCategoryInputModel> _updateCategoryValidator;

    public CategoriesController(
        ICategoriesService categoriesService,
        IValidator<CreateCategoryInputModel> createCategoryValidator,
        IValidator<UpdateCategoryInputModel> updateCategoryValidator)
    {
        _categoriesService = categoriesService;
        _createCategoryValidator = createCategoryValidator;
        _updateCategoryValidator = updateCategoryValidator;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> IndexAsync()
    {
        var categories = await _categoriesService.GetAsync(Request.HttpContext.RequestAborted);

        return View(categories.Select(c => new CategoryViewModel
        {
            Id = c.Id,
            Name = c.Name,
            CreatedOn = c.CreatedOn,
            UpdatedOn = c.UpdatedOn
        }));
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateAsync(CategoryViewModel viewModel)
    {
        var inputModel = new CreateCategoryInputModel(viewModel.Name);

        var validationResult = await _createCategoryValidator.ValidateAsync(inputModel, Request.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            viewModel.SetErrors(errors);

            return View(viewModel);
        }

        await _categoriesService.CreateAsync(inputModel, Request.HttpContext.RequestAborted);
        return RedirectToAction("Index", "Categories");
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var category = await _categoriesService.GetAsync(id, Request.HttpContext.RequestAborted);

        if (category == null)
            return RedirectToAction("Index", "Categories");

        return View(new CategoryViewModel
        {
            CreatedOn = category.CreatedOn,
            Id = category.Id,
            Name = category.Name,
            UpdatedOn = category.UpdatedOn
        });
    }

    [HttpPost("Edit")]
    public async Task<IActionResult> EditAsync(CategoryViewModel viewModel)
    {
        var inputModel = new UpdateCategoryInputModel(viewModel.Id, viewModel.Name);

        var validationResult = await _updateCategoryValidator.ValidateAsync(inputModel, Request.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            viewModel.SetErrors(errors);

            return View(viewModel);
        }

        await _categoriesService.UpdateAsync(inputModel, Request.HttpContext.RequestAborted);
        return RedirectToAction("Index", "Categories");
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _categoriesService.DeleteAsync(id, Request.HttpContext.RequestAborted);
        return RedirectToAction("Index", "Categories");
    }
}
