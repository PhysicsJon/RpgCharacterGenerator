using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Domain.Models;
using RpgApi.Infrastructure.Interfaces;

namespace RpgApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICrudService<Character, int> _service;

        public CharacterController(ICrudService<Character, int> service)
        {
            _service = service;
        }

        // GET: api/Character
        [HttpGet(Name = "Get list of Characters")]
        [ProducesResponseType(typeof(IEnumerable<Character>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Character>> Get()
        {
            return Ok(_service.GetAll());
        }

        // GET: api/Character/5
        [HttpGet("{id}", Name = "Get Character")]
        public ActionResult<Character> Get(int id)
        {
            return _service.GetById(id);
        }

        // POST: api/Character
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Character> Post([FromBody] Character character)
        {
            return Created(new Uri("/api/Character"), _service.Create(character));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}