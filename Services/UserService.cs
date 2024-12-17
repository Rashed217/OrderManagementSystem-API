using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;
using OrderManagementSystem.Repositories;

namespace OrderManagementSystem.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepo;
        private readonly IConfiguration _configuration;

        public UserService(UserRepository userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public User Register(RegisterDto model)
        {
            var existingUser = _userRepo.GetByEmail(model.Email);
            if (existingUser != null)
            {
                throw new Exception("Email is already registered.");
            }
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Phone = model.Phone,
                Role = model.Role,
                CreatedAt = DateTime.UtcNow,
            };

            _userRepo.AddUser(user);
            return user;
        }
    }
}
