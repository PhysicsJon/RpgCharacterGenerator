using Microsoft.AspNetCore.Mvc;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RpgApi.API.Controllers
{
    [Route("api/character/{characterId}/[controller]")]
    public class CharacterDetailsController : ControllerBase
    {
        private readonly IReadUpdateService<CharacterDetail, int> _service;

        public CharacterDetailsController(IReadUpdateService<CharacterDetail, int> service)
        {
            _service = service;
        }

        // GET api/<controller>/5
        [HttpGet]
        public CharacterDetail Get(int characterId)
        {
            return _service.GetByCharacterId(characterId);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put(int characterId, [FromBody] CharacterDetail detail)
        {
            _service.Update(characterId, detail);
        }
    }
}