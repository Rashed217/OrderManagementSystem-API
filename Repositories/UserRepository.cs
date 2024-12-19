using OrderManagementSystem.Controllers;
using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;
using System.Linq;

namespace OrderManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UID == id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }


        public User GetUser(string email, string password)
        {
            return _context.Users
                .Where(u => u.Email == email && u.Password == password)
                .FirstOrDefault();
        }
    }
}
