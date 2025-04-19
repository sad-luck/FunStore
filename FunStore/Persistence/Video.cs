namespace FunStore.Persistence;

public class Video : ProductBase
{
    public string Author { get; set; } = null!;

    public TimeSpan Duration { get; set; }
}