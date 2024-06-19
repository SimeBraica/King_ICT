using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DAL.Models;
using DTO;

namespace DAL
{
    public class AuthRepository
    {
        private readonly HttpClient _httpClient;

        public AuthRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponse> GetLoginAsync(UserDTO userDto)
        {
            var userResponse = await _httpClient.GetFromJsonAsync<UserResponse>("https://dummyjson.com/users");
            var existingUser = userResponse.Users.FirstOrDefault(u => u.Username == userDto.Username && u.Password == userDto.Password);

            if (existingUser == null)
            {
                return null; 
            }

            var loginResponse = await _httpClient.PostAsJsonAsync("https://dummyjson.com/auth/login", userDto);

            if (!loginResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var authResponse = new AuthResponse
            {
                Id = existingUser.Id,
                Username = existingUser.Username,
                Email = existingUser.Email,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                Gender = existingUser.Gender,
                Image = existingUser.Image,
                Token = "0",
                RefreshToken = "0"
            };

            return authResponse;
        }
    }
}
