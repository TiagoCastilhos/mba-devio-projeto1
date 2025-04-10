﻿namespace SuperStore.Model.Entities;

public class Category : EntityBase
{
    private List<Product> _products;

    public string Name { get; protected set; }
    public virtual IReadOnlyCollection<Product> Products => _products;
    public virtual Seller CreatedBy { get; protected set; }

    public Guid CreatedById { get; protected set; }

    protected Category() { }

    public Category(string name, Seller createdBy)
    {
        AssertionConcern.AssertArgumentNotNullOrEmpty(name, nameof(name));
        AssertionConcern.AssertArgumentNotNull(createdBy, nameof(createdBy));

        Id = Guid.NewGuid();
        Name = name;
        CreatedBy = createdBy;
    }

    public void ChangeName(string name)
    {
        AssertionConcern.AssertArgumentNotNullOrEmpty(name, nameof(name));

        Name = name;
    }
}