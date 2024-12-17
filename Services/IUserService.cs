using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;

namespace OrderManagementSystem.Services
{
    public interface IUserService
    {
        string Login(LoginDto model);
        User Register(RegisterDto model);
        User UpdateUser(UpdateUserDto model);
    }
}