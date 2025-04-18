namespace FunStore.Persistence;

public abstract class ProductBase
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public ProductType Type { get; set; }
}

public enum ProductType
{
    Membership = 0,
    Video = 1,
    Book = 2,
}