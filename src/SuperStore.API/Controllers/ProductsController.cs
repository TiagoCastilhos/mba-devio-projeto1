﻿using Microsoft.AspNetCore.Mvc;
using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.OutputModels;

namespace SuperStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<ProductOutputModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync()
    {
        var products = await _productsService.GetAsync(Request.HttpContext.RequestAborted);

        return Ok(products);
    }
}
