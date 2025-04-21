namespace SuperStore.Data.Entities;

public class Category : EntityBase
{
    public string Name { get; set; }
    public virtual IReadOnlyCollection<Product> Products { get; set; }
    public virtual Seller CreatedBy { get; set; }

    public Guid CreatedById { get; set; }

    protected Category() { }

    public Category(string name, Seller createdBy)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedBy = createdBy;
    }
}