namespace FunStore.Models.Response;

public class MembershipProductModelResponse : BaseProductModel
{
    public int Duration { get; set; }

    public DateTime? Expiration { get; set; }
}