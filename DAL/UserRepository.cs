using DAL.Models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DAL {
    public class UserRepository : IDisposable {

        private readonly HttpClient? _httpClient;

        public UserRepository(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public void Dispose() {
            _httpClient.Dispose();
        }
        public async Task<UserDTO> GetUserAsync(UserDTO user) {

            var userResponse = await _httpClient.GetFromJsonAsync<UserResponse>($"https://dummyjson.com/users");
            UserDTO _user = null;
            var existingUser = userResponse.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (existingUser != null) {
                _user = new UserDTO { Username = existingUser.Username };
            }
            return _user;
        }
    }
}
