namespace UM.Infrastructure.Repositories.Queries;

public class UserQueryRepository : IUserQueryRepository
{
    private readonly UmQueryContext _context;

    public UserQueryRepository(UmQueryContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        var user = await _context.Users
            .Include(u => u.Status)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _context.Users
            .Include(u => u.Status)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        return user != null;
    }

    public async Task<bool> UserExistsAsync(long id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        return user != null;
    }
}