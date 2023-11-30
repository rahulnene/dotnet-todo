using AutoMapper;
using dotnet_todo.Data;
using dotnet_todo.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_todo.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new()
        {
            new() { Id = 1, Name = "Sam" }
        };
        private readonly IRepository<Character> _repository;
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper, IRepository<Character> repository)
        {
            _mapper = mapper;
            _repository = repository;

        }
        public async Task<List<Character>?> AddCharacter(Character newCharacter)
        {
            await _repository.Add(_mapper.Map<Character>(newCharacter));
            return await GetAllCharacters();
        }

        public async Task<List<Character>?> DeleteCharacter(int id)
        {
            await _repository.Delete(id);
            return await GetAllCharacters();
        }

        public async Task<List<Character>?> GetAllCharacters()
        {
            return await _repository.GetAll() as List<Character>;
        }


        public async Task<Character?> GetCharacterById(int id)
        {
            await _repository.Get(id);
            return _mapper.Map<Character>(characters.FirstOrDefault(c => c.Id == id));
        }

        public async Task<List<Character>?> UpdateCharacter(Character updatedCharacter)
        {
            await _repository.Update(updatedCharacter);
            return await GetAllCharacters();

        }
    }
}