using BAL;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JWT _jwt;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="authService">The authentication service.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="jwt">The JWT instance.</param>
        public UserController(UserService userService, AuthService authService, ILogger<UserController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, JWT jwt) {
            _userService = userService;
            _authService = authService;
            _logger = logger;
            _config = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _jwt = new JWT(_config, _httpContextAccessor, _httpClientFactory);
        }
        /// <summary>
        /// Logs in a user by validating their credentials and generating a JWT for them.
        /// </summary>
        /// <param name="user">The user object containing the username and password.</param>
        /// <returns>An authentication response with user details and JWT token.</returns>
        /// <response code="200">Returns the authentication response if login is successful.</response>
        /// <response code="401">If the credentials are invalid or unauthorized.</response>
        /// <response code="403">If the login attempt is forbidden.</response>
        /// <response code="500">If there is an error while processing the login request.</response>

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO user) {
            _logger.LogInformation("Login attempt for user: {Username}", user.Username);
            IActionResult response = Unauthorized();

            var _user = await _authService.GetLogin(user);

            if (_user == null) {
                _logger.LogWarning("Login failed for user: {Username}", user.Username);
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

            _logger.LogInformation("Login successful for user: {Username}", user.Username);
            return Ok(authResponse);
        }
        /// <summary>
        /// Retrieves the profile of the currently logged-in user.
        /// </summary>
        /// <returns>The user profile.</returns>
        /// <response code="200">Returns the user profile.</response>
        /// <response code="401">If the user is not authorized or the token is invalid.</response>
        /// <response code="404">If the user profile is not found.</response>
        /// <response code="500">If there is an error while retrieving the user profile.</response>

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<User>> GetUserProfile() {
            _logger.LogInformation("Getting user profile");
            var username = _jwt.DecodeToken();
            if (string.IsNullOrEmpty(username)) {
                _logger.LogWarning("Failed to decode token");
                return Unauthorized();
            }

            var user = await _authService.GetUserProfile(username);
            if (user == null) {
                _logger.LogWarning("User profile not found for username: {Username}", username);
                return NotFound();
            }

            _logger.LogInformation("User profile retrieved for username: {Username}", username);
            return Ok(user);
        }
    }
}
