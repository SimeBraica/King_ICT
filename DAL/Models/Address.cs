using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models {
    public class Address {
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public string PostalCode { get; set; }
        public Coordinates Coordinates { get; set; }
        public string Country { get; set; }
    }
}
