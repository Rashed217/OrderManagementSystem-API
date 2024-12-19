using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;

namespace OrderManagementSystem.Services
{
    public interface IUserService
    {
        User GetUser(string email, string password);
        User Register(RegisterDto model);
    }
}