namespace SuperStore.Application.InputModels;

public sealed record CreateProductInputModel(string Name, string Description, decimal Price, int Quantity, int CategoryId);
