namespace UM.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UmContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public UserRepository(UmContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<User> AddAsync(User user)
    {
        return (await _context.Users.AddAsync(user)).Entity;
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await _context
            .Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}