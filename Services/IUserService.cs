using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;

namespace OrderManagementSystem.Services
{
    public interface IUserService
    {
        bool DeleteUser(string email);
        User GetUser(string email, string password);
        User Register(RegisterDto model);
        User UpdateUser(string email, UpdateUserDto model);
    }
}