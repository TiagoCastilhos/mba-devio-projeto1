using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.InputModels;
using SuperStore.Application.OutputModels;

namespace SuperStore.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<CategoryOutputModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync()
    {
        var categories = await _categoriesService.GetAsync(Request.HttpContext.RequestAborted);
        return Ok(categories);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IEnumerable<CategoryOutputModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(int id)
    {
        var categories = await _categoriesService.GetAsync(Request.HttpContext.RequestAborted);
        return Ok(categories);
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(CategoryOutputModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryInputModel inputModel)
    {
        var category = await _categoriesService.CreateAsync(inputModel, Request.HttpContext.RequestAborted);
        return CreatedAtAction(nameof(GetAsync), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CategoryOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateCategoryInputModel inputModel)
    {
        if (id != inputModel.Id)
            return BadRequest("Product ID mismatch.");

        var category = await _categoriesService.UpdateAsync(inputModel, Request.HttpContext.RequestAborted);
        return Ok(category);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _categoriesService.DeleteAsync(id, Request.HttpContext.RequestAborted);
        return NoContent();
    }
}
