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
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get-all-customers")]
        public async Task<ActionResult> GetAllCustomers()
        {
            var customers = await _unitOfWork.Customers
                                                        .GetAll()
                                                        .ToListAsync();

            return Ok(customers);
        }

    }
}
