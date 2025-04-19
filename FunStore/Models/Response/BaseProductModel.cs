namespace FunStore.Models.Response;

public class BaseProductModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }
}