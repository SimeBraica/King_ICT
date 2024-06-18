using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL {
    public class UserService {

        private readonly HttpClient? _httpClient;

        public UserService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<UserDTO> GetUser(UserDTO user) {
            using (var repo = new UserRepository(_httpClient)) {
                var _user = repo.GetUserAsync(user);
                if (_user != null) return await _user;
                else return null;
            }
        }

    }
}
