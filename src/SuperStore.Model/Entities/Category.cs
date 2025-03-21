namespace SuperStore.Model.Entities;

public class Category : Entity
{
    private List<Product> _products;

    public string Name { get; protected set; }
    public virtual IReadOnlyCollection<Product> Products => _products;
    public SalesPerson CreatedBy { get; protected set; }

    public int CreatedById { get; protected set; }
}