using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models {
    public class Company {
        public string Department { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public Address Address { get; set; }
    }
}
