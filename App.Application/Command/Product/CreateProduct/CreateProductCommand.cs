using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Command.Product.CreateProduct
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
    public class ProductVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductStatus { get; set; }
        public string CreatedBy { get; set; }

    }
}
