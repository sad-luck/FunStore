namespace FunStore.Persistence;

public class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int AppUserId { get; set; }

    public AppUser AppUser { get; set; } = null!;
}