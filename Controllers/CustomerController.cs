using casestudy02.Models;
using casestudy02.Services;
using Microsoft.AspNetCore.Mvc;

namespace casestudy02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService) =>
            _customerService = customerService;

        [HttpGet]
        public async Task<List<Customer>> Get() =>
            await _customerService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public async Task<IActionResult> Post(List<Customer> newCustomers)
        {

            await _customerService.CreateAsync(newCustomers);

            return CreatedAtAction(nameof(Get), newCustomers);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Customer updatedCustomer)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            updatedCustomer.CustomerID = customer.CustomerID;

            await _customerService.UpdateAsync(id, updatedCustomer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            await _customerService.RemoveAsync(id);

            return NoContent();
        }
    }
}