using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie_API.Logger;
using Movie_API.Models;
using Movie_API.Services;

namespace Movie_API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IAuthManager _authManager;

        public AccountController(UserManager<User> userManager, IMapper mapper, ILoggerManager logger, IAuthManager authManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInfo($"Registration attempt for {userDTO.Email}");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid bill body object sent from client.");
                return BadRequest("Invalid model object");
            }

            try
            {
                var user = _mapper.Map<User>(userDTO);
                user.UserName = userDTO.Email;

                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    _logger.LogError("User registration attempt failed.");
                    return BadRequest(ModelState);
                }

                _logger.LogInfo($"{userDTO.Email} successfully registered");
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside {nameof(Register)} action: {ex.Message}");
                return StatusCode(500, $"Internal server error in the {nameof(Register)}");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO userDTO)
        {
            _logger.LogInfo($"Login attempt for {userDTO.Email}");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid bill body object sent from client.");
                return BadRequest("Invalid model object");
            }

            try
            {
                if (!await _authManager.ValidateUser(userDTO))
                {
                    return Unauthorized();
                }

                _logger.LogInfo("Tryed to assign token");
                return Accepted(new { Token = await _authManager.CreateToken() });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside {nameof(Login)} action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
