using FunStore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FunStore.Services;

public interface IProductService
{
    Task<List<Book>> GetBooksAsync();
    Task<List<Membership>> GetMembershipsAsync();
    Task<List<Video>> GetVideosAsync();
}

public class ProductService : IProductService
{
    private readonly FunStoreContext _dbcontext;

    public ProductService(FunStoreContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<List<Membership>> GetMembershipsAsync()
    {
        return await _dbcontext.Products.OfType<Membership>().ToListAsync();
    }

    public async Task<List<Video>> GetVideosAsync()
    {
        return await _dbcontext.Products.OfType<Video>().ToListAsync();
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        return await _dbcontext.Products.OfType<Book>().ToListAsync();
    }
}