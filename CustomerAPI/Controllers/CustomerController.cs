using CustomerAPI.Interfaces;
using CustomerAPI.Models;
using CustomerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var returnValue = await _customerService.GetCustomers();

            if (returnValue == null)
            {
                return BadRequest();
            }

            return Ok(returnValue);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var returnValue = await _customerService.GetCustomerById(id);

            if (returnValue == null)
            {
                return BadRequest();
            }

            return Ok(returnValue);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            var returnValue = await _customerService.AddCustomer(customer);

            if (returnValue != 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            var returnValue = await _customerService.UpdateCustomer(customer);

            if (returnValue != 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var returnValue = await _customerService.DeleteCustomer(id);

            if (returnValue != 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
