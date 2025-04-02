namespace SuperStore.MVC.ViewModels.Products;

public class ProductViewModel : ViewModelBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset UpdatedOn { get; set; }
}
