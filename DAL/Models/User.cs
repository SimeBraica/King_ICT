using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models {
    public class User {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MaidenName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BirthDate { get; set; }
        public string Image { get; set; }
        public string BloodGroup { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string EyeColor { get; set; }
        public Hair Hair { get; set; }
        public string Ip { get; set; }
        public Address Address { get; set; }
        public string MacAddress { get; set; }
        public string University { get; set; }
        public Bank Bank { get; set; }
        public Company Company { get; set; }
        public string Ein { get; set; }
        public string Ssn { get; set; }
        public string UserAgent { get; set; }
        public Crypto Crypto { get; set; }
        public string Role { get; set; }
    }
}
