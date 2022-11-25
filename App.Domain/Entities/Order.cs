using App.Domain.Common;
using App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string OrderAddress { get; set; }
        public string OrderEmail { get; set; }
        public string OrderPhone { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; private set; }
        public Guid CustomerId { get; set; }
        public Customer User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public void SetOrderStatusToRequested()
        {
            OrderStatus = OrderStatus.OrderRequested;
        }
        public void SetOrderStatusToProcessing()
        {
            OrderStatus = OrderStatus.OrderProcessing;
        }
        public void SetOrderStatusToDelivered()
        {
            OrderStatus = OrderStatus.OrderDelivered;
        }
        public void SetOrderStatusToRejected()
        {
            OrderStatus = OrderStatus.OrderRejected;
        }

        public void CreateOrderDetail(OrderDetail orderDetail, Product product)
        {
            SetOrderStatusToRequested();
            OrderDetails.Add(orderDetail);
            orderDetail.UpdateProductQuantity(product, orderDetail.Quantity);
        }
    }

}
