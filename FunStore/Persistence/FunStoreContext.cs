using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
    public DbSet<Order> Orders { get; set; }

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

        modelBuilder.Entity<Customer>()
            .HasMany(x => x.Orders)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);

        modelBuilder.Entity<Order>()
           .Property(nameof(Order.Items))
           .HasConversion(new ValueConverter<IList<string>, string>(v => string.Join(";", v), v => v.Split(new[] { ';' })));
    }
}