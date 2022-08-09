
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        public User GetUser(int id);
        public User GetSafeUser(int id);
        public User GetUserByUsername(string username);
        public User GetUserByUsernameOrEmail(string username,string email);
        public User CreateUser(User user);
        public User UpdateUser(User user);
        public User DeleteUser(User user);
        IEnumerable<User> GetAll();
    }
}
