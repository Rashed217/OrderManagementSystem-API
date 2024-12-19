using OrderManagementSystem.Controllers;
using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;
using OrderManagementSystem.Repositories;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepo, IConfiguration configuration)
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


        public User GetUser(string email, string password)
        {
            return _userRepo.GetUser(email, password);

        }

        // New method to update a user's details
        public User UpdateUser(string email, UpdateUserDto model)
        {
            var user = _userRepo.GetByEmail(email); // Fetch the user from repository
            if (user == null)
            {
                throw new Exception("User not found."); // Handle user not found scenario
            }

            // Update user properties with values from the model
            user.Name = model.Name ?? user.Name; // Only update if new value is provided
            user.Phone = model.Phone ?? user.Phone;
            user.Role = model.Role ?? user.Role;

            // You may want to add validation or business rules here
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = model.Password; // Update password only if it's provided
            }

            // Save the updated user to the repository
            _userRepo.UpdateUser(user);

            return user; // Return the updated user object
        }

        // New method to delete a user by email
        public bool DeleteUser(string email)
        {
            var user = _userRepo.GetByEmail(email); // Fetch the user from repository
            if (user == null)
            {
                throw new Exception("User not found."); // Handle user not found scenario
            }

            _userRepo.DeleteUser(user); // Call DeleteUser on the repository to remove the user
            return true; // Return true indicating successful deletion
        }
    }
}
