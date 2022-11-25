using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Command.ProductCategory
{
    public class CreateProductCategoryCommand
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
