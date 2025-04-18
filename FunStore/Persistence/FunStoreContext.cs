using Microsoft.EntityFrameworkCore;

namespace FunStore.Persistence;

public class FunStoreContext : DbContext
{
    public FunStoreContext(DbContextOptions<FunStoreContext> options)
            : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ProductBase> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
           .HasOne(u => u.Customer)
           .WithOne(c => c.AppUser)
           .HasForeignKey<Customer>(u => u.AppUserId)
           .IsRequired();

        modelBuilder.Entity<ProductBase>()
            .HasDiscriminator(p => p.Type)
            .HasValue<Membership>(ProductType.Membership)
            .HasValue<Video>(ProductType.Video)
            .HasValue<Book>(ProductType.Book);
    }
}