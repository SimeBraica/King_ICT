using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using DTO;
using DAL.Models;
using System.Net.Http;

namespace API {
    public class JWT {

        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        public JWT(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory) {
            _config = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<string> GenerateJWT(UserDTO user) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var audience = _config["Jwt:Audience"];
            var issuer = _config["Jwt:Issuer"];
            TimeZoneInfo croatiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
            DateTime expiresLocalTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, croatiaTimeZone).AddMinutes(30);

            string username = user.Username;
            string role = await GetUserRole(username);


            var jwt_description = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {new Claim("username", username),
                                                    new Claim("role", role),
                                                   }),
                Expires = expiresLocalTime,
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = credentials
            };

            var token = new JwtSecurityTokenHandler().CreateToken(jwt_description);
            var encryptedToken = new JwtSecurityTokenHandler().WriteToken(token);

            _httpContextAccessor.HttpContext.Response.Cookies.Append("token", encryptedToken,
                new CookieOptions {
                    Expires = expiresLocalTime,
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });

            var response = new { token = encryptedToken, username = user.Username };
            return JsonSerializer.Serialize(response);
        }

        private async Task<string> GetUserRole(string username) {
            using (var httpClient = _httpClientFactory.CreateClient()) {
                var userResponse = await httpClient.GetFromJsonAsync<UserResponse>($"https://dummyjson.com/users");
                return userResponse.Users.Where(u => u.Username == username).ToString();
            }
  
        }
    }
}
