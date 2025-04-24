
namespace FunStore.Persistence;

public class Order
{
    public int Id { get; set; }

    public decimal Total { get; set; }

    public IList<string> Items { get; set; } = new List<string>();

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public void AddItem(ProductBase p)
    {
        if (p is null)
            return;

        Items.Add(p.Title);
        Total += p.Price;
    }
}