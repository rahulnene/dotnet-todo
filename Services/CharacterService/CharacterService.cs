using AutoMapper;
using dotnet_todo.Data;
using dotnet_todo.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_todo.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character>? _characters;
        private readonly IRepository<Character> _repository;
        private readonly IMapper _mapper;

        public CharacterService(IRepository<Character> repository)
        {
            _repository = repository;

        }
        public async Task<int> AddCharacter(Character newCharacter)
        {
            var entity_id = await _repository.Add(newCharacter);
            return entity_id;
        }

        public async Task DeleteCharacter(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Character>?> GetAllCharacters()
        {
            return await _repository.GetAll() as List<Character>;
        }


        public async Task<Character> GetCharacterById(int id)
        {
            return await _repository.Get(id);
        }

        public async Task UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            await _repository.Update(updatedCharacter);
        }
    }
}