using Kolokwium_2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kolokwium_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _service;
        public AlbumController(IAlbumService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbums(int id)
        {
            if (await _service.GetAId(id).FirstOrDefaultAsync() is null)
                return NotFound("Nie znaleziono albumu o podanym id");
            return Ok(_service.GetAlbums(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusician(int id)
        {
            if (await _service.GetId(id).FirstOrDefaultAsync() is null)
                return NotFound("Nie znaleziono muzyka o podanym id");
            await _service.DeleteMusician(id);
            return Created("","");
        }
    }
}
