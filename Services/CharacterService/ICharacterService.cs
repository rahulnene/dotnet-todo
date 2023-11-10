using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_todo.Models;

namespace dotnet_todo.Services.CharacterService
{
    public interface ICharacterService
    {

        Task<List<Character>> GetAllCharacters();
        Task<Character> GetCharacterById(int id);
        Task<List<Character>> AddCharacter(Character newCharacter);
    }
}