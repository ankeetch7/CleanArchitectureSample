using App.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public Category()
        {
            ProductCategories = new List<ProductCategory>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
