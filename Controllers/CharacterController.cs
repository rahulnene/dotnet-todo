using dotnet_todo.Models;
using dotnet_todo.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_todo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Character>>> Get()
        {
            var result = await _characterService.GetAllCharacters();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Character>>> GetSingle(int id)
        {
            try
            {
                return Ok(await _characterService.GetCharacterById(id));
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<List<Character>>> AddCharacter(Character newCharacter)
        {
            try
            {
                var response = await _characterService.AddCharacter(newCharacter);
                if (response == null)
                {
                    return BadRequest();
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return UnprocessableEntity();
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<List<Character>>> UpdateCharacter(Character updatedCharacter)
        {
            List<Character>? response;
            try
            {
                response = await _characterService.UpdateCharacter(updatedCharacter);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                {
                    return NotFound(ex.Message);
                }
                return BadRequest();
            }
            return Ok(response);
        }
        [HttpDelete("/delete/{id}")]
        public async Task<ActionResult<List<Character>>> DeleteCharacter(int id)
        {
            try
            {
                var response = await _characterService.DeleteCharacter(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                {
                    return NotFound(ex.Message);
                }
                return BadRequest();
            }
        }
    };
}
