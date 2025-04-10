﻿namespace SuperStore.Model.Entities;
public class Product : EntityBase
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public virtual Seller CreatedBy { get; protected set; }
    public virtual Category Category { get; protected set; }
    public decimal Price { get; protected set; }
    public int Quantity { get; protected set; }
    public string ImageUrl { get; protected set; }

    public Guid CreatedById { get; protected set; }
    public Guid CategoryId { get; protected set; }

    protected Product() { }

    public Product(string name, string description, decimal price, int quantity, string imageUrl, Seller createdBy, Category category)
    {
        AssertionConcern.AssertArgumentNotNullOrEmpty(name, nameof(name));
        AssertionConcern.AssertArgumentNotNullOrEmpty(description, nameof(description));
        AssertionConcern.AssertArgumentNotNull(createdBy, nameof(createdBy));
        AssertionConcern.AssertArgumentNotNull(category, nameof(category));
        AssertionConcern.AssertArgumentNotNegative(price, nameof(price));
        AssertionConcern.AssertArgumentNotNegative(quantity, nameof(quantity));

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedBy = createdBy;
        Category = category;
        Price = price;
        Quantity = quantity;
        ImageUrl = imageUrl;
    }

    public void ChangeName(string name)
    {
        AssertionConcern.AssertArgumentNotNullOrEmpty(name, nameof(name));

        Name = name;
    }

    public void ChangeDescription(string description)
    {
        AssertionConcern.AssertArgumentNotNullOrEmpty(description, nameof(description));

        Description = description;
    }

    public void ChangePrice(decimal price)
    {
        AssertionConcern.AssertArgumentNotNegative(price, nameof(price));

        Price = price;
    }

    public void ChangeQuantity(int quantity)
    {
        AssertionConcern.AssertArgumentNotNegative(quantity, nameof(quantity));

        Quantity = quantity;
    }

    public void ChangeImage(string imageUrl)
    {
        ImageUrl = imageUrl;
    }

    public void ChangeCategory(Category category)
    {
        AssertionConcern.AssertArgumentNotNull(category, nameof(category));

        Category = category;
    }
}
