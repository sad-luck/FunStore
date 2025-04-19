using FunStore.Models.Response;
using FunStore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FunStore.Services;

public interface IProductService
{
    Task<List<BookProductModelResponse>> GetBooksAsync();

    Task<List<MembershipProductModelResponse>> GetMembershipsAsync();

    Task<List<VideoProductModelResponse>> GetVideosAsync();
}

public class ProductService : IProductService
{
    private readonly FunStoreContext _dbcontext;

    public ProductService(FunStoreContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<List<MembershipProductModelResponse>> GetMembershipsAsync()
    {
        var result = await _dbcontext.Products
            .OfType<Membership>()
            .AsNoTracking()
            .ToListAsync();

        return [.. result.Select(x => new MembershipProductModelResponse { Id = x.Id, Expiration = x.Expiration, Duration = x.Duration, Price = x.Price, Title = x.Title })];
    }

    public async Task<List<VideoProductModelResponse>> GetVideosAsync()
    {
        var result = await _dbcontext.Products
            .OfType<Video>()
            .AsNoTracking()
            .ToListAsync();

        return [.. result.Select(x => new VideoProductModelResponse { Id = x.Id, Author = x.Author, Duration = x.Duration, Price = x.Price, Title = x.Title })];
    }

    public async Task<List<BookProductModelResponse>> GetBooksAsync()
    {
        var result = await _dbcontext.Products
            .OfType<Book>()
            .AsNoTracking()
            .ToListAsync();

        return [.. result.Select(x => new BookProductModelResponse { Id = x.Id, Author = x.Author, PageCount = x.PageCount, Price = x.Price, Title = x.Title })];
    }
}