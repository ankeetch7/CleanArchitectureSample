using App.Application.Command.Category;
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
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get-all-category")]
        public async Task<ActionResult> GetAllCategories()
        {
            var categories = await _unitOfWork.Categories
                                                .GetAll()
                                                .ToListAsync();
            return Ok(categories);
        }

        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var category = new Category
            {
                Name = command.Name,
                Description = command.Description,
                Image = command.Image
            };

            _unitOfWork.Categories.Add(category);
            await _unitOfWork.SaveChangesAsync();

            return Ok(category);
        }
    }
}
