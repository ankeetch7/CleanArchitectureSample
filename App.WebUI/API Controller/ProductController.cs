using App.Application.Command.Product.CreateProduct;
using App.Application.Command.Product.UpdateProduct;
using App.Application.Interfaces;
using App.Domain.Entities;
using App.Infrastructure.Repositories;
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
        public async Task<ActionResult<List<ProductVm>>> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAllProducts();
            return Ok(products);
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            await _unitOfWork.Products.CreateProduct(command);
            return Ok();
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            await _unitOfWork.Products.UpdateProduct(command);
            return Ok();
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            await _unitOfWork.Products.DeleteProduct(id);
            return Ok();
        }
    }
}
