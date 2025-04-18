namespace FunStore.Persistence;

public class AppUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Memberships Memberships { get; set; }

    public Customer? Customer { get; set; }
}

[Flags]
public enum Memberships
{
    None = 0,
    BookClubUser = 1 << 0,
    VideoClubUser = 1 << 1,
    PremiumUser = 1 << 2,
}