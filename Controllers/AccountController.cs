using Microsoft.AspNetCore.Mvc;
using BankingApi.Models;

namespace BankingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private static List<Account> accounts = new();

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(accounts);
        }

       [HttpPost("create")]
public IActionResult Create([FromBody] Account account)
{
    accounts.Add(account);
    return Ok(account);
}

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var account = accounts.FirstOrDefault(x => x.Id == id);

            if (account == null)
                return NotFound();

            accounts.Remove(account);

            return Ok("Account deleted");
        }
    }
}