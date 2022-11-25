using App.Application.Command.Order;
using App.Application.Interfaces;
using App.Application.Services;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.WebUI.API_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        public OrderController(IUnitOfWork unitOfWork,
                                ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        [HttpGet("get-all-order")]
        public async Task<ActionResult> GetAllOrders()
        {
            var orders = await _unitOfWork.Orders
                                            .GetAll()
                                            .ToListAsync();
            return Ok(orders);
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var loggedinUserId =  _currentUserService.CustomerId;
            var order = new Order
            {
                Amount = command.Amount,
                OrderAddress = command.OrderAddress,
                OrderEmail = command.OrderEmail,
                OrderPhone = command.OrderPhone,
                OrderDate = DateTime.UtcNow,
                CustomerId = Guid.Parse(loggedinUserId)
            };

            var orderdetail = new OrderDetail
            {
                ProductId = command.ProductId,
                Price = command.Price,
                Quantity = command.Quantity
            };

            var productDetail = _unitOfWork.Products.GetById(command.ProductId);

            order.CreateOrderDetail(orderdetail, productDetail);

            _unitOfWork.Orders.Add(order);
            await _unitOfWork.SaveChangesAsync();

            return Ok(order);
        }

    }
}
