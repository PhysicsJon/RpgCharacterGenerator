using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly ICrudServiceAsync<Character, int> _serviceAsync;

        public CharacterController(ICrudServiceAsync<Character, int> serviceAsync)
        {
            _serviceAsync = serviceAsync;
        }

        // GET: api/Character
        [HttpGet(Name = "Get list of Characters")]
        [ProducesResponseType(typeof(IEnumerable<Character>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _serviceAsync.GetAll().ConfigureAwait(false));
        }

        // GET: api/Character/5
        [HttpGet("{id}", Name = "Get Character")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _serviceAsync.GetById(id).ConfigureAwait(false));
        }

        // POST: api/Character
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] Character character)
        {
            return Created(new Uri("/api/Character"), await _serviceAsync.Create(character).ConfigureAwait(false));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceAsync.DeleteAsync(id).ConfigureAwait(false);
            return Ok();
        }
    }
}