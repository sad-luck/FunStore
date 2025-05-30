﻿
namespace FunStore.Persistence;

public abstract class ProductBase
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public ProductType Type { get; set; }

    public bool IsSimpleProduct() => Type != ProductType.Membership;

    public bool IsMembershipProduct() => Type == ProductType.Membership;
}

public enum ProductType
{
    Membership = 0,
    Video = 1,
    Book = 2,
}