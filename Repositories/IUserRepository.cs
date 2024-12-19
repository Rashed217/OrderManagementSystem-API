using OrderManagementSystem.Model;

namespace OrderManagementSystem.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetByEmail(string email);
        User GetById(int id);
        User GetUser(string email, string password);
    }
}