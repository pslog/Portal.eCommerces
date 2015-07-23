using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.Models;

namespace WebApplication.Models.ViewModels
{
    public class OrderProductViewModels
    {
        public int ID { get; set; }
        public System.Guid GUID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string OrderCode { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal FeeShip { get; set; }
        public decimal TotalOrder { get; set; }
        public int OrderStatus { get; set; }
        public string OrderNote { get; set; }
        public string NameOfRecipient { get; set; }
        public string PhoneOfRecipient { get; set; }
        public string AddressOfRecipient { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual ICollection<product_OrderDetails> product_OrderDetails { get; set; }
    }
}
