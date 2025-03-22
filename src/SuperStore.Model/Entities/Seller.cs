namespace SuperStore.Model.Entities;
public class Seller : EntityBase
{
    private List<Product> _products;
    private List<Category> _categories;

    public string Name { get; protected set; }
    public virtual IReadOnlyCollection<Product> Products => _products;
    public virtual IReadOnlyCollection<Category> Categories => _categories;
    public Guid UserId { get; protected set; }
}
