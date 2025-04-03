namespace SuperStore.Application.InputModels;

public sealed record UpdateProductInputModel(int Id, string Name, string Description, decimal Price, int Quantity, string ImageUrl, int CategoryId);
