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

    protected Product() { }

    public Product(string name, string description, decimal price, int quantity, Seller createdBy, Category category)
    {
        Name = name;
        Description = description;
        CreatedBy = createdBy;
        Category = category;
        Price = price;
        Quantity = quantity;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeDescription(string description)
    {
        Description = description;
    }

    public void ChangePrice(decimal price)
    {
        Price = price;
    }

    public void ChangeQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public void ChangeCategory(Category category)
    {
        Category = category;
    }
}
