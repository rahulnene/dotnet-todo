using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_todo.DTOs.Characters;
using dotnet_todo.Models;

namespace dotnet_todo.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters();
        Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharactersFromDatabase();
        Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter);
        Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter);
        Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id);
        Task<ServiceResponse<bool>> SaveCharactersToDatabase(List<GetCharacterDTO> characters);

    }
}