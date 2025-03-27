using Lab5TestTask.Data;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// UserService implementation.
/// Implement methods here.
/// </summary>
public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> GetUserAsync()
    {
        var groupedAndOrderedUsers = await _dbContext.Sessions
            .GroupBy(s => s.User)
            .Select(g => new { g.Key, Count = g.Count() })
            .OrderByDescending(u => u.Count)
            .FirstAsync();
        return groupedAndOrderedUsers.Key;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _dbContext.Sessions.Where(s => s.DeviceType == Enums.DeviceType.Mobile)
            .GroupBy(s => s.User)
            .Select(g => g.Key)
            .ToListAsync();
    }
}
