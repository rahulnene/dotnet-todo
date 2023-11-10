using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_todo.Models;

namespace dotnet_todo.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new()
        {
            new(),
            new() { Id = 1, Name = "Sam" }
        };
        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            var response = new ServiceResponse<List<Character>>
            {
                Data = characters
            };
            return response;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<Character>>
            {
                Data = characters
            };
            return response;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            var response = new ServiceResponse<Character>
            {
                Data = character
            };
            return response;
        }
    }
}