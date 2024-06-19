using BAL;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; // Add this for ILogger
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly ILogger<UserController> _logger;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private JWT _jwt;
     
        public UserController(UserService userService, AuthService authService, ILogger<UserController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, JWT jwt)
        {
            _userService = userService;
            _authService = authService;
            _logger = logger;
            _config = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _jwt = new JWT(_config, _httpContextAccessor, _httpClientFactory);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            IActionResult response = Unauthorized();

            var _user = await _authService.GetLogin(user);

            if (_user == null)
            {
                return Forbid();
            }
            var token = await _jwt.GenerateJWT(user);
            var authResponse = new AuthResponse {
                Id = _user.Id,
                Username = _user.Username,
                Email = _user.Email,
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                Gender = _user.Gender,
                Image = _user.Image,
                Token = token,
                RefreshToken = "0"
            };
            return Ok(authResponse);
        }

        [Authorize]
        [HttpGet("me")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUserProfile() {
            var username = _jwt.DecodeToken();
            var user = await _authService.GetUserProfile(username);
            return Ok(user);
        }

    }
}
