namespace FunStore.Models.Response;

public class BookProductModelResponse : BaseProductModel
{
    public string Author { get; set; } = null!;

    public int PageCount { get; set; }
}