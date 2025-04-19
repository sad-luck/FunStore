using FunStore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FunStore.Services;

public interface IPurchaseProcessorService
{
    Task<object?> PurchaseOrder(IEnumerable<int> productIds);
}

public class PurchaseProcessorService : IPurchaseProcessorService
{
    private readonly FunStoreContext _dbcontext;

    public PurchaseProcessorService(FunStoreContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<object?> PurchaseOrder(IEnumerable<int> productIds)
    {
        var products = await _dbcontext.Products
            .Where(x => productIds.Contains(x.Id))
            .ToListAsync();

        return products;
    }
}