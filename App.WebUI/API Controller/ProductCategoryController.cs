using App.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.WebUI.API_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get-all-product-categories")]
        public async Task<ActionResult> GetAllProductCategories()
        {
            var productCategories = await _unitOfWork.ProductCategories
                                                                        .GetAll()
                                                                        .ToListAsync();
            return Ok(productCategories);
        }
    }
}
