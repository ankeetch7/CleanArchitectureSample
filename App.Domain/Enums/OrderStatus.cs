using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Domain.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Requested")]
        OrderRequested = 1,
        [Display(Name = "Rejected")]
        OrderRejected = 2,
        [Display(Name = "Processing")]
        OrderProcessing  = 3,
        [Display(Name = "Delivered")]
        OrderDelivered = 4
    }
}
