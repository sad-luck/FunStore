
namespace FunStore.Persistence;

public class Order
{
    public int Id { get; set; }

    public decimal Total { get; set; }

    public List<string> Items { get; set; } = new();

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public void AddItem(ProductBase p)
    {
        Items.Add(p.Title);
        Total += p.Price;
    }
}