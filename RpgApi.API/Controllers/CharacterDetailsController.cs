using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RpgApi.API.Controllers
{
    [Route("api/character/{characterId}/[controller]")]
    public class CharacterDetailsController : ControllerBase
    {
        private readonly IReadUpdateServiceAsync<CharacterDetail, int> _serviceAsync;

        public CharacterDetailsController(IReadUpdateServiceAsync<CharacterDetail, int> serviceAsync)
        {
            _serviceAsync = serviceAsync;
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<IActionResult> Get(int characterId)
        {
            return Ok(await _serviceAsync.GetByCharacterIdAsync(characterId).ConfigureAwait(false));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IActionResult> Put(int characterId, [FromBody] CharacterDetail detail)
        {
            await _serviceAsync.UpdateAsync(characterId, detail).ConfigureAwait(false);
            return NoContent();
        }
    }
}