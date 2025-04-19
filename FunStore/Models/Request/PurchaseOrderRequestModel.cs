namespace FunStore.Models.Request;

public class PurchaseOrderRequestModel
{
    public IEnumerable<int> ProductIds { get; set; } = Enumerable.Empty<int>();
}