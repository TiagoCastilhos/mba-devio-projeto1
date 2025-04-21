using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core.Abstractions.Services;
using SuperStore.Core.InputModels;
using SuperStore.Core.OutputModels;

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
    public async Task<IActionResult> GetAllAsync()
    {
        var categories = await _categoriesService.GetAsync(Request.HttpContext.RequestAborted);
        return Ok(categories);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetAsync))]
    [ProducesResponseType(typeof(CategoryOutputModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var categories = await _categoriesService.GetAsync(id, Request.HttpContext.RequestAborted);
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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateCategoryInputModel inputModel)
    {
        if (id != inputModel.Id)
            return BadRequest("Id da categoria da rota deve ser o mesmo do corpo da mensagem.");

        var category = await _categoriesService.UpdateAsync(inputModel, Request.HttpContext.RequestAborted);
        return Ok(category);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _categoriesService.DeleteAsync(id, Request.HttpContext.RequestAborted);
        return NoContent();
    }
}
