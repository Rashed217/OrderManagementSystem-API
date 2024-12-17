using OrderManagementSystem.Model;

namespace OrderManagementSystem.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetByEmail(string email);
        User GetById(int id);
        void UpdateUser(User user);
    }
}