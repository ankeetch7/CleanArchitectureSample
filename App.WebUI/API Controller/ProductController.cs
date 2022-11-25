using App.Application.Command.Product;
using App.Application.Interfaces;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace App.WebUI.API_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get-all-products")]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _unitOfWork.Products
                                                        .GetAll()
                                                        .ToListAsync();
            return Ok(products);
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                UnitPrice = command.UnitPrice,
                SellingUnitPrice = command.SellingUnitPrice,
                Quantity = command.Quantity,
                Image = command.Image
            };

            _unitOfWork.Products.Add(product);
            await _unitOfWork.SaveChangesAsync();

            return Ok(product);
        }
    }
}
