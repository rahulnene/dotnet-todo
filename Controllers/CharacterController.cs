using dotnet_todo.DTOs.Characters;
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
        public async Task<ActionResult<List<GetCharacterDTO>>> Get()
        {
            var result = await _characterService.GetAllCharactersFromDatabase();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<GetCharacterDTO>>> GetSingle(int id)
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
        public async Task<ActionResult<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
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

        [HttpPut]
        public async Task<ActionResult<List<GetCharacterDTO>>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            GetCharacterDTO? response;
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GetCharacterDTO>>> DeleteCharacter(int id)
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
        [HttpPut("Save")]
        public async Task<ActionResult> SaveCharactersToDatabase()
        {
            var characters = await _characterService.GetAllCharacters();
            try
            {
                await _characterService.SaveCharactersToDatabase(characters);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    };
}
