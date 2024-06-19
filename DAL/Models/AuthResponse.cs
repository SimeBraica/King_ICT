using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AuthResponse
    {
        public int Id { get; set; } = 0;
        public string Username { get; set; } = null;
        public string Email { get; set; } = null;
        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;
        public string Gender { get; set; } = null;
        public string Image { get; set; } = null;
        public string Token { get; set; } = null;
        public string RefreshToken { get; set; } = null;
    }
}
