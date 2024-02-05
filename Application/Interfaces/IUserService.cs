using Data.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Task<User?> GetUserByEmail(string email);
        public Task<bool> AnyUserByEmail(string email);
        public Task UpdateUser(User user);
        public Task AddUser(User user);
    }
}
