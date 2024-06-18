using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models {
    public class Bank {
        public string CardExpire { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string Currency { get; set; }
        public string Iban { get; set; }
    }
}
