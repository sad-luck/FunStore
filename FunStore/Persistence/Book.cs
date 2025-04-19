namespace FunStore.Persistence;

public class Book : ProductBase
{
    public string Author { get; set; } = null!;

    public int PageCount { get; set; }
}