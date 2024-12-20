﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OrderManagementSystem.Services;
using OrderManagementSystem.Model;
using OrderManagementSystem.DTO;
using Microsoft.AspNetCore.Authorization;

namespace OrderManagementSystem.Controllers
{
    [Authorize]
    [ApiController] // Indicates that this class is an API controller, which automatically handles model validation and other HTTP-related tasks.
    [Route("api/[Controller]")] // Defines the route for the controller, where [Controller] will be replaced with the class name (in this case, "User").
    public class UserController : ControllerBase
    {
        // Declares the dependencies for the controller: IUserService for business logic and IConfiguration for accessing configuration settings.
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        // Constructor that initializes the dependencies through dependency injection.
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        // Action method for handling POST requests to add a new user.
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDto model)
        {
            try
            {
                var user = _userService.Register(model);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        // Action method for handling GET requests to log in a user.
        [HttpGet("Login")] // Specifies that this method will handle HTTP GET requests.
        public IActionResult login(string email, string password) // Accepts email and password as query parameters.
        {
            // Calls the GetUser method from the injected IUserService to retrieve the user based on the provided email and password.
            var user = _userService.GetUser(email, password);

            if (user != null) // If the user is found, proceed to generate a JWT token.
            {
                // Generates a JWT token for the user and returns it in the response.
                string token = GenerateJwtToken(user.UID.ToString(), user.Name);
                return Ok(token); // Returns the token if the login is successful.
            }
            else
            {
                // Returns a BadRequest response if the provided credentials are invalid.
                return BadRequest("Invalid Credentials");
            }
        }

        // NonAction attribute indicates that this method is not an action method for handling HTTP requests.
        [NonAction]
        public string GenerateJwtToken(string userId, string username)
        {
            // Retrieves JWT settings from the configuration.
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]; // Retrieves the secret key used for signing the JWT token.

            // Defines the claims (user data) that will be included in the JWT token.
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId), // The subject of the token (user ID).
            new Claim(JwtRegisteredClaimNames.UniqueName, username), // The unique name (username).
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // A unique identifier for the JWT token (Jti).
        };

            // Creates a symmetric security key using the secret key.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Defines the signing credentials using HMAC SHA-256.

            // Creates the JWT token with the defined claims and expiry time from the configuration.
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])), // Expiry time in minutes retrieved from configuration.
                signingCredentials: creds
            );

            // Converts the JWT token to a string and returns it.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Endpoint to update user information
        [HttpPut("update/{email}")]
        public IActionResult UpdateUser(string email, [FromBody] UpdateUserDto model)
        {
            try
            {
                var updatedUser = _userService.UpdateUser(email, model);
                return Ok(updatedUser); // Return the updated user
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return error message if something goes wrong
            }
        }

        // Delete user by email
        [HttpDelete("{email}")]
        public IActionResult DeleteUser(string email)
        {
            try
            {
                var result = _userService.DeleteUser(email); // Call DeleteUser method from UserService
                if (result)
                {
                    return Ok(new { message = "User deleted successfully." }); // Return success response
                }
                return NotFound(new { message = "User not found." }); // If user not found
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // If an exception occurs
            }
        }
    }
}
