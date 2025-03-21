namespace SuperStore.Model.Entities;
public class SalesPerson : Entity
{
    private List<Product> _products;

    public string Name { get; protected set; }
    public virtual IReadOnlyCollection<Product> Products => _products;
    public Guid UserId { get; protected set; }
}
