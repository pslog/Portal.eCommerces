using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.ViewModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Catalog { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
