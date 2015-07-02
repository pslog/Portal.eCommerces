using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.ViewModels
{
    public class ProductPartialViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Catagory { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
