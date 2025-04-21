using BizWizBPTool.Models;
using BizWizBPTool.Data;
using Microsoft.EntityFrameworkCore;

namespace BizWizBPTool.Repositories;


public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync() { 
        return await _context.Users.ToListAsync(); 
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var userInDb = await _context.Users.FindAsync(userId);

        if (userInDb == null)
        {
            throw new KeyNotFoundException($"User with id {userId} was not found.");
        }

        _context.Users.Remove(userInDb);
        await _context.SaveChangesAsync();
    }
    
}
