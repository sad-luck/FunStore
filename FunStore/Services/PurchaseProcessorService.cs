using FunStore.Models.Response;
using FunStore.Persistence;
using FunStore.Persistence.Builders;
using FunStore.ValidationExceptions;
using Microsoft.EntityFrameworkCore;

namespace FunStore.Services;

public interface IPurchaseProcessorService
{
    Task<PurchaseOrderResponseModel> PurchaseOrder(IEnumerable<int> productIds);
}

public class PurchaseProcessorService : IPurchaseProcessorService
{
    private readonly FunStoreContext _dbcontext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;

    public PurchaseProcessorService(
        FunStoreContext dbcontext,
        ICurrentUserService currentUserService,
        IUserService userService)
    {
        _dbcontext = dbcontext;
        _currentUserService = currentUserService;
        _userService = userService;
    }

    public async Task<PurchaseOrderResponseModel> PurchaseOrder(IEnumerable<int> productIds)
    {
        if (productIds is null || productIds == Enumerable.Empty<int>())
            throw new ValidationException("At least one ID must be provided");

        var uniqueIds = productIds.Distinct();

        var products = await _dbcontext.Products
            .Where(x => uniqueIds.Contains(x.Id))
            .ToListAsync();

        var username = _currentUserService.Username;
        var user = await _userService.GetUserByName(username)
            ?? throw new UserNotFoundException();

        await using var transaction = await _dbcontext.Database.BeginTransactionAsync();

        try
        {
            var membershipProductProcessingResult = string.Empty;

            var orderBuilder = new OrderBuilder();
            orderBuilder.SetCustomer(user.Customer);

            if (products.Any(x => x.IsMembershipProduct()))
                membershipProductProcessingResult = await ProcessMembershipProducts(orderBuilder, user, [.. products.Where(x => x.IsMembershipProduct()).Cast<Membership>()]);

            ProcessSimpleProducts(orderBuilder, user, [.. products.Where(x => x.IsSimpleProduct())]);

            var order = orderBuilder.Build();

            _dbcontext.Orders.Add(order);

            await _dbcontext.SaveChangesAsync();
            await transaction.CommitAsync();

            return new PurchaseOrderResponseModel(order.Id, order.CustomerId, order.Items, order.Total, membershipProductProcessingResult);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            throw;
        }
    }

    private void ProcessSimpleProducts(OrderBuilder orderBuilder, AppUser user, List<ProductBase> products)
    {
        foreach (var product in products)
        {
            if (!product.IsSimpleProduct())
                continue;

            if (user.Memberships.HasFlag(GetRequiredMembership(product.Type)))
            {
                orderBuilder.AddItem(product);
                continue;
            }

            throw new ValidationException($"User does not owned a membership to purchase the product with {product.Id} ID.");
        }
    }

    private async Task<string> ProcessMembershipProducts(OrderBuilder orderBuilder, AppUser user, List<Membership> membershipProducts)
    {
        foreach (var product in membershipProducts)
        {
            if (!product.IsMembershipProduct() || user.Memberships.HasFlag(GetRequiredMembership(product.RelatedProductType)))
                continue;

            orderBuilder.AddItem(product);
        }

        return await _userService.AddMembershipRoleAndRepublishToken(user,
            [.. membershipProducts
                .Where(x => x.IsMembershipProduct())
                .Select(x => GetRequiredMembership(x.RelatedProductType))]);
    }

    private Memberships GetRequiredMembership(ProductType type) => type switch
    {
        ProductType.Video => Memberships.VideoClubUser,
        ProductType.Book => Memberships.BookClubUser,
        _ => throw new ArgumentOutOfRangeException(),
    };
}