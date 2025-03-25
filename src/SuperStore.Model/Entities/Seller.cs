namespace SuperStore.Model.Entities;
public class Seller : EntityBase
{
    private List<Product> _products;
    private List<Category> _categories;

    public string Name { get; protected set; }
    public virtual IReadOnlyCollection<Product> Products => _products;
    public virtual IReadOnlyCollection<Category> Categories => _categories;
    public string UserId { get; protected set; }

    protected Seller() { }

    public Seller(string name, string userId)
    {
        Name = name;
        UserId = userId;
    }
}
