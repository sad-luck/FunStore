namespace FunStore.Persistence;

public class Membership : ProductBase
{
    public int Duration { get; set; }

    public DateTime? Expiration { get; set; }
}