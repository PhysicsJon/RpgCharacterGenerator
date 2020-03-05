using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;

namespace RpgApi.API.Controllers
{
    [Route("api/character/{characterId}/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IReadUpdateServiceAsync<Attributes, int> _serviceAsync;

        public AttributesController(IReadUpdateServiceAsync<Attributes, int> serviceAsync)
        {
            _serviceAsync = serviceAsync;
        }

        // GET: api/Attributes/5
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _serviceAsync.GetByCharacterIdAsync(id).ConfigureAwait(false));
        }

        // PUT: api/Attributes/5
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Attributes attributes)
        {
            await _serviceAsync.UpdateAsync(id, attributes).ConfigureAwait(false);
            return NoContent();
        }
    }
}