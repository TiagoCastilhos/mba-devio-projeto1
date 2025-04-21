using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core.Abstractions.Services;
using SuperStore.Core.InputModels;
using SuperStore.Core.OutputModels;

namespace SuperStore.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [AllowAnonymous]
    [HttpGet("Showcase")]
    [ProducesResponseType(typeof(IEnumerable<ProductOutputModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ShowcaseAsync()
    {
        var products = await _productsService.ShowcaseAsync(Request.HttpContext.RequestAborted);

        return Ok(products);
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<ProductOutputModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var products = await _productsService.GetAsync(Request.HttpContext.RequestAborted);

        return Ok(products);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetAsync))]
    [ProducesResponseType(typeof(ProductOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var product = await _productsService.GetAsync(id, Request.HttpContext.RequestAborted);

        return Ok(product);
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(ProductOutputModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductInputModel inputModel)
    {
        var product = await _productsService.CreateAsync(inputModel, Request.HttpContext.RequestAborted);
        return CreatedAtAction(nameof(GetAsync), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateProductInputModel inputModel)
    {
        if (id != inputModel.Id)
            return BadRequest("Id do produto da rota deve ser o mesmo do corpo da mensagem.");

        var product = await _productsService.UpdateAsync(inputModel, Request.HttpContext.RequestAborted);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _productsService.DeleteAsync(id, Request.HttpContext.RequestAborted);
        return NoContent();
    }
}
