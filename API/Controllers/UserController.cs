using BAL;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly UserService userService;
        private readonly JWT _jwt;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IHttpClientFactory httpClientFactory) {
            _config = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _jwt = new JWT(_config, _httpContextAccessor, _httpClientFactory);
            userService = new UserService(httpClient);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO user) {
            IActionResult response = Unauthorized();
            var _user = await userService.GetUser(user);
            if (_user != null) {
                var token =  _jwt.GenerateJWT(_user);
                response = Ok(new {token});
            }
            return response;
        }

    }
}
