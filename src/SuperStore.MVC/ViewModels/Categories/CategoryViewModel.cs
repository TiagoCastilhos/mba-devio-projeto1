namespace SuperStore.MVC.ViewModels.Categories;

public class CategoryViewModel : ViewModelBase
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset UpdatedOn { get; set; }
}
