//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication.Models.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class product_OrderDetails
    {
        public int ID { get; set; }
        public System.Guid GUID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal PriceOfUnit { get; set; }
        public Nullable<decimal> TotalDiscount { get; set; }
        public decimal TotalOrder { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual product_Orders product_Orders { get; set; }
        public virtual product_Products product_Products { get; set; }
    }
}
