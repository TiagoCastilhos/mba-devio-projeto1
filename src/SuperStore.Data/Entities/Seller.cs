namespace SuperStore.Data.Entities;
public class Seller : EntityBase
{
    public string Name { get; set; }
    public virtual IReadOnlyCollection<Product> Products { get; set; }
    public virtual IReadOnlyCollection<Category> Categories { get; set; }
    public string UserId { get; set; }

    protected Seller() { }

    public Seller(string name, string userId)
    {
        Id = Guid.Parse(userId);
        Name = name;
        UserId = userId;
    }
}
