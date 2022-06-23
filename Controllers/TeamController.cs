using Kolokwium_2.Models.DTO;
using Kolokwium_2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kolokwium_2.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class AlbumController : ControllerBase
    {
        private readonly ITeamService _service;
        public AlbumController(ITeamService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            if (await _service.GetAId(id).FirstOrDefaultAsync() is null)
                return NotFound("Nie znaleziono zespołu o podanym id");
            return Ok(await _service.GetTeam(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(MemberPut member)
        {
            await _service.AddMember(member);
            return Created("", "");
        }
    }
}
