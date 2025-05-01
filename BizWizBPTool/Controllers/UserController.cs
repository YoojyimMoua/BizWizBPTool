using BizWizBPTool.Models;
using BizWizBPTool.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity.Data;

namespace BizWizBPTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            return Ok(await _userRepository.GetAllAsync());
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> 
            GetUserById(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            await _userRepository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
        }
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUserById(int userId)
        {
            await _userRepository.DeleteUserAsync(userId);
            return NoContent();
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<User>> UpdateUser(int userId, User user)
        {
            if (userId != user.UserId)
            {
                return BadRequest();
            }
            await _userRepository.UpdateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            // Check if the email is already registered
            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already registered.");
            }

            // Hash the password
            user.Password = HashPassword(user.Password);

            // Save the user
            await _userRepository.AddUserAsync(user);

            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
        }

        // Helper method to hash passwords
        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
        {
            // Check if the user exists
            var user = await _userRepository.GetByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Verify the password
            if (!VerifyPassword(loginRequest.Password, user.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        // Helper method to verify the password
        private bool VerifyPassword(string password, string hashedPassword)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            var hashedInput = Convert.ToBase64String(hash);
            return hashedInput == hashedPassword;
        }

        // Helper method to generate JWT token
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Email, user.Email)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyUltraSuperSecureJwtKey12345678!@#")); // Replace with a secure key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourapp",
                audience: "yourapp",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
