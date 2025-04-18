
using FunStore.Configurations;
using FunStore.Persistence;
using FunStore.ValidationExceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FunStore.Services;

public interface IUserService
{
    Task<string> Login(string username, string password);
    Task Register(string username, string password, string firsName, string lastName);
}

public class UserService : IUserService
{
    private readonly FunStoreContext _dbcontext;
    private readonly JwtSettings _jwtSettings;

    public UserService(FunStoreContext dbcontext, IOptions<JwtSettings> jwtSettings)
    {
        _dbcontext = dbcontext;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<string> Login(string username, string password)
    {
        var user = await _dbcontext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());

        if (user is not null)
        {
            if (user.Password != password)
            {
                throw new ValidationException("Provided password is incorrect");
            }

            return GenerateJwtToken(user);
        }

        throw new UserNotFoundException();
    } 

    public async Task Register(string username, string password, string firsName, string lastName)
    {
        if (await _dbcontext.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
        {
            throw new ValidationException($"User with '{username}' username already exists");
        }

        await using var transaction = await _dbcontext.Database.BeginTransactionAsync();

        try
        {
            var user = new AppUser { Username = username.ToLower(), Password = password, Memberships = Memberships.None };
            var customer = new Customer { FirstName = firsName, LastName = lastName, AppUser = user };

            _dbcontext.Users.Add(user);
            _dbcontext.Customers.Add(customer);

            await _dbcontext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GenerateJwtToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (user.Memberships.HasFlag(Memberships.BookClubUser))
        {
            claims.Add(new Claim(ClaimTypes.Role, "BookClubUser"));
        }

        if (user.Memberships.HasFlag(Memberships.VideoClubUser))
        {
            claims.Add(new Claim(ClaimTypes.Role, "VideoClubUser"));
        }

        if (user.Memberships.HasFlag(Memberships.PremiumUser))
        {
            claims.Add(new Claim(ClaimTypes.Role, "PremiumUser"));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: creds);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}