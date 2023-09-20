
using CrwnClothing.DAL.Entities;
using CrwnClothing.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CrwnClothing.DAL.Repositories.UserRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly CrwnClothingContext _context;

        public UserRepository(CrwnClothingContext context) : base(context)
        {
            _context = context;
        }

        public User GetUserByUsername(string username)
        {
            User? user = _context.Users.Where
                 (user => user.Username == username || user.Email == username).FirstOrDefault();

            return user!;
        }

        public User GetUserByUsernameOrEmail(string username, string email)
        {
            User? user =
                _context.Users.Where(user => user.Username == username || user.Email == email).FirstOrDefault();

            return user!;
        }
    }
}
