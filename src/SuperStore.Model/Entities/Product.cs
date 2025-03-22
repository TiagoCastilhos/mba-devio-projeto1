namespace SuperStore.Model.Entities;

public class Product : EntityBase
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public Seller CreatedBy { get; protected set; }
    public Category Category { get; protected set; }
    public decimal Price { get; protected set; }
    public int Quantity { get; protected set; }

    public int CreatedById { get; protected set; }
    public int CategoryId { get; protected set; }
}
