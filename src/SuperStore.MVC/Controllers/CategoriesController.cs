﻿using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.InputModels;
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
        var categories = await _categoriesService.GetAsync(CancellationToken.None);

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

        var validationResult = await _createCategoryValidator.ValidateAsync(inputModel, CancellationToken.None);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            viewModel.SetErrors(errors);

            return View(viewModel);
        }

        await _categoriesService.CreateAsync(inputModel, CancellationToken.None);
        return RedirectToAction("Index", "Categories");
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoriesService.GetAsync(id, CancellationToken.None);

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

        var validationResult = await _updateCategoryValidator.ValidateAsync(inputModel, CancellationToken.None);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            viewModel.SetErrors(errors);

            return View(viewModel);
        }

        await _categoriesService.UpdateAsync(inputModel, CancellationToken.None);
        return RedirectToAction("Index", "Categories");
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _categoriesService.DeleteAsync(id, CancellationToken.None);
        return RedirectToAction("Index", "Categories");
    }
}
