namespace SuperStore.Model.Entities;

public class Category : EntityBase
{
    private List<Product> _products;

    public string Name { get; protected set; }
    public virtual IReadOnlyCollection<Product> Products => _products;
    public Seller CreatedBy { get; protected set; }

    public int CreatedById { get; protected set; }

    protected Category() { }

    public Category(string name, Seller createdBy)
    {
        Name = name;
        CreatedBy = createdBy;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }
}