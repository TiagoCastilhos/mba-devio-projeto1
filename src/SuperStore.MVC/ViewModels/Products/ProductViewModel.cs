using SuperStore.Application.OutputModels;

namespace SuperStore.MVC.ViewModels.Products;

public class ProductViewModel : ViewModelBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? Category { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    public ProductViewModel()
    {
    }

    public ProductViewModel(ProductOutputModel outputModel)
    {
        Id = outputModel.Id;
        Name = outputModel.Name;
        Description = outputModel.Description;
        Price = outputModel.Price;
        Quantity = outputModel.Quantity;
        Category = outputModel.Category;
        ImageUrl = outputModel.ImageUrl;
        CreatedOn = outputModel.CreatedOn;
        UpdatedOn = outputModel.UpdatedOn;
    }
}
