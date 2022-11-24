using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Enums
{
    public enum OrderStatus
    {
        OrderRequested = 1,
        OrderRejected = 2,
        OrderProcessing  = 3,
        OrderDelivered = 4
    }
}
