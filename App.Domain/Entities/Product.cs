using App.Domain.Common;
using App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public Product()
        {
            ProductCategories = new List<ProductCategory>();
            OrderDetails = new List<OrderDetail>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
