using App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Command.Order
{
    public class CreateOrderCommand
    {
        public decimal Amount { get; set; }
        public string OrderAddress { get; set; }
        public string OrderEmail { get; set; }
        public string OrderPhone { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
    }
}
