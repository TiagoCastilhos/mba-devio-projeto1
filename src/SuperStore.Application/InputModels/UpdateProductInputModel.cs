﻿namespace SuperStore.Application.InputModels;

public sealed record UpdateProductInputModel(int Id, string Name, string Description, decimal Price, int Quantity, string ImageUrl, string Category)
{
    public string ImageUrl { get; set; }
}
