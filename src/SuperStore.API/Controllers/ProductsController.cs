using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.InputModels;
using SuperStore.Application.OutputModels;

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
    public async Task<IActionResult> GetAsync()
    {
        var products = await _productsService.GetAsync(Request.HttpContext.RequestAborted);

        return Ok(products);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        var product = await _productsService.GetAsync(id, Request.HttpContext.RequestAborted);

        return Ok(product);
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(ProductOutputModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductInputModel inputModel)
    {
        var product = await _productsService.CreateAsync(inputModel, Request.HttpContext.RequestAborted);
        return CreatedAtAction(nameof(GetAsync), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateProductInputModel inputModel)
    {
        if (id != inputModel.Id)
            return BadRequest("Product ID mismatch.");

        var product = await _productsService.UpdateAsync(inputModel, Request.HttpContext.RequestAborted);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await _productsService.DeleteAsync(id, Request.HttpContext.RequestAborted);
        return NoContent();
    }
}
