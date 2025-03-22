namespace SuperStore.Model.Entities;

public class Category : EntityBase
{
    private List<Product> _products;

    public string Name { get; protected set; }
    public virtual IReadOnlyCollection<Product> Products => _products;
    public Seller CreatedBy { get; protected set; }

    public int CreatedById { get; protected set; }
}