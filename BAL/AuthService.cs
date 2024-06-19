using DAL;
using DTO;
using System.Threading.Tasks;
using DAL.Models;

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
    }
}
