using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.Models;

namespace WebApplication.Models.ViewModels
{
    public class ProductDetailsPartialViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Catalog { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsNewProduct { get; set; }
        public string Tags { get; set; }
        public int Quantity { get; set; }
        public ICollection<share_Images> share_Images { get; set; }
    }
}
