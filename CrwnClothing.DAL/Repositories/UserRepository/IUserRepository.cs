
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Repositories.UserRepository
{
    public interface IUserRepository : IRepository<User>
    {
        public User GetUserByUsername(string username);
        public User GetUserByUsernameOrEmail(string username,string email);
    }
}
