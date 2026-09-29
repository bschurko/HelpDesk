using HelpDesk.Api.Repository;
using HelpDesk.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await repository.GetAll();
            return Ok(entities);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await repository.Get(id);
            return Ok(entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(User entity)
        {
            bool updated = await repository.Update(entity);
            return Ok(updated);
        }

        [HttpPost]
        public async Task<IActionResult> Add(User entity)
        {
            var added = await repository.Add(entity);
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
