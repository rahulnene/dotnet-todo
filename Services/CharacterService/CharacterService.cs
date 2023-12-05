using AutoMapper;
using dotnet_todo.Data;
using dotnet_todo.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_todo.Services.CharacterService
{
    public class CharacterService(IRepository<Character> repository) : ICharacterService
    {
        private readonly IRepository<Character> _repository = repository;
        private readonly IMapper _mapper;

        public async Task<int> AddCharacter(Character newCharacter)
        {
            var AllChars = await _repository.GetAll();
            var AllIds = AllChars.Select(h => h.Id);
            var RNG = new Random();
            int rand_id = RNG.Next();
            while (AllIds.Contains(rand_id))
            {
                rand_id = RNG.Next();
            }
            newCharacter.Id = rand_id;
            var entity_id = await _repository.Add(newCharacter);
            return entity_id;
        }

        public async Task DeleteCharacter(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Character>?> GetAllCharacters()
        {
            var result = await _repository.GetAll();
            return result;
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