using Microsoft.AspNetCore.Mvc;
using BankingApi.Models;

namespace BankingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private static List<Customer> customers = new();

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(customers);
        }

        [HttpPost("create")]
        public IActionResult Create(Customer customer)
        {
            customers.Add(customer);
            return Ok(customer);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var customer = customers.FirstOrDefault(x => x.Id == id);

            if (customer == null)
                return NotFound();

            customers.Remove(customer);

            return Ok("Customer deleted");
        }
    }
}