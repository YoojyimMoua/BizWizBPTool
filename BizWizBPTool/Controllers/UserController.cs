using BizWizBPTool.Models;
using BizWizBPTool.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    }
}
