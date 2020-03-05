using Microsoft.AspNetCore.Mvc;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;

namespace RpgApi.API.Controllers
{
    [Route("api/character/{characterId}/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IReadUpdateService<Attributes, int> _service;

        public AttributesController(IReadUpdateService<Attributes, int> service)
        {
            _service = service;
        }

        // GET: api/Attributes/5
        [HttpGet]
        public Attributes Get(int id)
        {
            return _service.GetByCharacterId(id);
        }

        // PUT: api/Attributes/5
        [HttpPut]
        public void Put(int id, [FromBody] Attributes attributes)
        {
            _service.Update(id, attributes);
        }
    }
}