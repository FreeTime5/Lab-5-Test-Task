using Lab5TestTask.Data;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// SessionService implementation.
/// Implement methods here.
/// </summary>
public class SessionService : ISessionService
{
    private readonly ApplicationDbContext _dbContext;

    public SessionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Session> GetSessionAsync()
    {
        return await _dbContext.Sessions.Where(s => s.DeviceType == Enums.DeviceType.Desktop)
            .OrderBy(s => s.StartedAtUTC)
            .FirstAsync();
    }

    public async Task<List<Session>> GetSessionsAsync()
    {
        var date2025 = new DateTime(2025, 1, 1);
        return await _dbContext.Sessions.Where(s => s.EndedAtUTC < date2025)
            .Join(_dbContext.Users.Where(u => u.Status == Enums.UserStatus.Active),
            s => s.UserId,
            u => u.Id,
            (s, u) => s)
            .ToListAsync();
    }
}
