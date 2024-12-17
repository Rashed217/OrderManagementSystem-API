using Microsoft.IdentityModel.Tokens;
using OrderManagementSystem.DTO;
using OrderManagementSystem.Model;
using OrderManagementSystem.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public string Login(LoginDto model)
        {
            var user = _userRepo.GetByEmail(model.Email);
            if (user != null || (model.Password != user.Password))
            {
                throw new Exception("Invalid Incredentials.");
            }
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UID.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User UpdateUser(UpdateUserDto model)
        {
            var existingUser = _userRepo.GetById(model.ID); // Fetch user by ID
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            existingUser.Name = model.Name ?? existingUser.Name;
            existingUser.Email = model.Email ?? existingUser.Email;
            existingUser.Phone = model.Phone ?? existingUser.Phone;
            existingUser.Role = model.Role ?? existingUser.Role;

            _userRepo.UpdateUser(existingUser);
            return existingUser;
        }
    }
}