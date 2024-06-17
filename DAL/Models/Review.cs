using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models {
    public class Review {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
    }

}
