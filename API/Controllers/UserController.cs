using BAL;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly UserService userService;

        public UserController(HttpClient httpClient) {
            userService = new UserService(httpClient);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO user) {
            IActionResult response = Unauthorized();
            var _teacher = await userService.GetUser(user);
            if (_teacher != null) {
                //var token = _jwt.GenerateJWT(_teacher);
                response = Ok();
            }
            return response;
        }

    }
}
