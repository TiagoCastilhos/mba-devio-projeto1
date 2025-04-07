namespace SuperStore.MVC.ViewModels.Categories;

public class CategoryViewModel : ViewModelBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}
