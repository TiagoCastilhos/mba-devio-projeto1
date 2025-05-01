namespace SuperStore.Data.Entities;
public class Product : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual Seller CreatedBy { get; set; }
    public virtual Category Category { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }

    public Guid CreatedById { get; set; }
    public Guid CategoryId { get; set; }

    protected Product() { }

    public Product(string name, string description, decimal price, int quantity, string? imageUrl, Seller createdBy, Category category)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedBy = createdBy;
        Category = category;
        Price = price;
        Quantity = quantity;
        ImageUrl = imageUrl;
        CreatedOn = DateTime.UtcNow;
        UpdatedOn = DateTime.UtcNow;
    }
}
