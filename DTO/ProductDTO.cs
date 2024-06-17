using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO;
public class ProductDto {
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string ShortDescription { get; set; }
    public List<string> Images { get; set; }
}