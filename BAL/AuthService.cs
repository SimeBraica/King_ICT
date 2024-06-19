using DAL;
using DTO;
using System.Threading.Tasks;
using DAL.Models;
using System.Net.Http;

namespace BAL
{
    public class AuthService
    {
        private readonly AuthRepository _authRepository;

        public AuthService(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<AuthResponse> GetLogin(UserDTO user)
        {
            var _user = await _authRepository.GetLoginAsync(user);
            return _user;
        }


        public async Task<User> GetUserProfile(string username) {
            var _user = await _authRepository.GetUserAsync(username);
            return _user;
        }
    }
}
