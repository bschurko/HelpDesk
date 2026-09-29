using HelpDesk.Api.Repository;
using HelpDesk.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository repository;

        public TicketController(ITicketRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await repository.GetAll();
            return Ok(tickets);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var ticket = await repository.Get(id);
            return Ok(ticket);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Ticket ticket)
        {
            bool updated = await repository.Update(ticket);
            return Ok(updated);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Ticket ticket)
        {
            var added = await repository.Add(ticket);
            return Ok(added);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await repository.Delete(id);
            return Ok(deleted);
        }
    }
}
