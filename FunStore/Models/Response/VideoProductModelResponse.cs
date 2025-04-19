namespace FunStore.Models.Response;

public class VideoProductModelResponse : BaseProductModel
{
    public string Author { get; set; } = null!;

    public TimeSpan Duration { get; set; }
}