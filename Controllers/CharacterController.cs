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
        public async Task<ActionResult<List<Character>?>> Get()
        {
            var result = await _characterService.GetAllCharacters();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Character>?>> GetSingle(int id)
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
        [HttpPost("new")]
        public async Task<ActionResult<int>> AddCharacter(Character newCharacter)
        {
            try
            {
                var response = await _characterService.AddCharacter(newCharacter);
                return Ok(response);
            }
            catch (Exception)
            {
                return UnprocessableEntity();
            }
        }

        [HttpPatch("update")]
        public async Task<ActionResult> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            try
            {
                await _characterService.UpdateCharacter(updatedCharacter);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                {
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<List<Character>?>> DeleteCharacter(int id)
        {
            try
            {
                await _characterService.DeleteCharacter(id);
                return Ok();
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
