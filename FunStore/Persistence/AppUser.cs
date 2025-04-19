


namespace FunStore.Persistence;

public class AppUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Memberships Memberships { get; set; }

    public Customer? Customer { get; set; }

    public bool IsPremium() => Memberships.HasFlag(Memberships.PremiumUser);

    public bool RolesAlreadySetted(Memberships[] membershipRoles)
    {
        foreach (Memberships membership in membershipRoles)
        {
            if (Memberships.HasFlag(membership))
                continue;

            return false;
        }

        return true;
    }

    public void AddRole(Memberships[] membershipRoles)
    {
        foreach (Memberships membership in membershipRoles)
        {
            Memberships |= membership;
        }

        EnsurePremiumMembership();
    }

    private void EnsurePremiumMembership()
    {
        if (Memberships.HasFlag(Memberships.BookClubUser)
            && Memberships.HasFlag(Memberships.VideoClubUser))
        {
            Memberships |= Memberships.PremiumUser;
        }
    }
}

[Flags]
public enum Memberships
{
    None = 0,
    BookClubUser = 1 << 0,
    VideoClubUser = 1 << 1,
    PremiumUser = 1 << 2,
}