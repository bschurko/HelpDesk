using HelpDesk.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private readonly ICodeRepository repository;

        public CodeController(ICodeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("statuses")]
        public async Task<IActionResult> GetAllStatuses()
        {
            var entities = await repository.GetAllStatuses();
            return Ok(entities);
        }

        [HttpGet("priorities")]
        public async Task<IActionResult> GetAllPriotiries()
        {
            var entities = await repository.GetAllPriorities();
            return Ok(entities);
        }

    }
}
