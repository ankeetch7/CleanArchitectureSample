using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Domain.Enums
{
    public enum ProductStatus
    {
        [Display(Name = "In Stock")]
        InStock = 0,
        [Display(Name = "Out of Stock")]
        OutOfStock = 1,
        [Display(Name = "Damaged")]
        Damaged = 2
    }
}
