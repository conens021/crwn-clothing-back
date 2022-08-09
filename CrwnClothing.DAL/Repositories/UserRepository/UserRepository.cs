
using CrwnClothing.DAL.Entities;
using CrwnClothing.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CrwnClothing.DAL.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly CrwnClothingContext _context;

        public UserRepository(CrwnClothingContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);

            _context.SaveChanges();


            return user;
        }

        public User DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetSafeUser(int id)
        {
            User user = _context.Users.AsNoTracking().Where(user => user.Id == id).FirstOrDefault()!;


            return user;
        }

        public User GetUser(int id)
        {
            User user = _context.Users.Where(user => user.Id == id).FirstOrDefault()!;


            return user;
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

        public User UpdateUser(User user)

        {
            _context.Users.Attach(user);

            _context.Entry(user).State = EntityState.Modified;

            var entry = _context.Entry(user);

            PropertyInfo[] properties = typeof(User).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(user, null) == null)
                {
                    entry.Property(property.Name).IsModified = false;
                }
            }

            _context.SaveChanges();


            return user;
        }
    }
}
