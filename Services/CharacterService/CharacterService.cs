using AutoMapper;
using dotnet_todo.DTOs.Characters;
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
        private readonly CharacterDbContext _context; private readonly IMapper _mapper;

        public CharacterService(IMapper mapper, CharacterDbContext context)
        {
            _mapper = mapper;
            _context = context;

        }
        public async Task<List<GetCharacterDTO>?> AddCharacter(AddCharacterDTO newCharacter)
        {
            try
            {
                var characters = _mapper.Map<List<Character>>(await GetAllCharactersFromDatabase());
                var character = _mapper.Map<Character>(newCharacter);
                if (characters == null)
                {
                    character.Id = 1;
                }
                else
                {
                    character.Id = characters.Max(c => c.Id) + 1;
                }
                characters.Add(character);
                await SaveCharactersToDatabase(characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList());
                return characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<GetCharacterDTO>?> DeleteCharacter(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id) ?? throw new Exception($"Character with id {id} not found.");
            characters.Remove(character);
            var response = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            await SaveCharactersToDatabase(characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList());
            return response;
        }

        public async Task<List<GetCharacterDTO>?> GetAllCharacters()
        {
            return characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
        }


        public async Task<List<GetCharacterDTO>?> GetAllCharactersFromDatabase()
        {
            var characters = await _context.Characters.ToListAsync();
            return characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
        }

        public async Task<GetCharacterDTO?> GetCharacterById(int id)
        {
            var characters = await GetAllCharactersFromDatabase();
            var character = characters.FirstOrDefault(c => c.Id == id);
            return character == null ? throw new Exception($"Character with id {id} not found.") : _mapper.Map<GetCharacterDTO>(character);
        }

        public async Task SaveCharactersToDatabase(List<GetCharacterDTO> characters)
        {
            try
            {
                using var db = _context;
                foreach (var characterDto in characters)
                {
                    var existingCharacter = db.Characters.AsTracking().FirstOrDefault(c => c.Id == characterDto.Id);
                    if (existingCharacter != null)
                    {
                        // Update the existing entity
                        var updatedCharacter = _mapper.Map<Character>(characterDto);
                        db.Entry(existingCharacter).CurrentValues.SetValues(updatedCharacter);
                    }
                    else
                    {
                        // Create a new Character object without an Id
                        var newCharacter = _mapper.Map<Character>(characterDto);
                        await db.Characters.AddAsync(newCharacter);
                    }
                    db.SaveChanges();
                }
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error saving characters to database.");
            }
        }

        public async Task<GetCharacterDTO?> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            try
            {
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id) ?? throw new Exception($"Character with id {updatedCharacter.Id} not found.");
                character.Name = updatedCharacter.Name;
                character.Class = updatedCharacter.Class;
                character.Defense = updatedCharacter.Defense;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Strength = updatedCharacter.Strength;
                await SaveCharactersToDatabase(characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList());
                return _mapper.Map<GetCharacterDTO>(character);
            }
            catch (Exception)
            {
                throw new Exception("Error updating character.");
            }

        }
    }
}