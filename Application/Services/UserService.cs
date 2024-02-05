using Application.Interfaces;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly TaskifyDbContext _taskifyDbContext;
        public UserService(TaskifyDbContext taskifyDbContext)
        {
            _taskifyDbContext = taskifyDbContext;
        }

        public Task<bool> AnyUserByEmail(string email) => _taskifyDbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Email == email.ToLower());

        public Task<User?> GetUserByEmail(string email) => _taskifyDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email.ToLower());

        public async Task UpdateUser(User user)
        {
            _taskifyDbContext.Users.Update(user);
            await _taskifyDbContext.SaveChangesAsync();
        }

        public async Task AddUser(User user)
        {
            _taskifyDbContext.Users.Add(user);
            await _taskifyDbContext.SaveChangesAsync();
        }

    }
}
