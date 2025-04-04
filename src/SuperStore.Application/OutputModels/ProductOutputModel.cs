﻿using SuperStore.Model.Entities;

namespace SuperStore.Application.OutputModels;
public sealed class ProductOutputModel(Product product)
{
    public int Id { get; } = product.Id;
    public string Name { get; } = product.Name;
    public string Description { get; } = product.Description;
    public decimal Price { get; } = product.Price;
    public int Quantity { get; } = product.Quantity;
    public string ImageUrl { get; } = product.ImageUrl;
    public string Category { get; } = product.Category.Name;
    public DateTimeOffset CreatedOn { get; } = product.CreatedOn;
    public DateTimeOffset UpdatedOn { get; } = product.UpdatedOn;
}
