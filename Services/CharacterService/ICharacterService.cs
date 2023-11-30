using dotnet_todo.Models;

namespace dotnet_todo.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<List<Character>?> GetAllCharacters();
        Task<Character> GetCharacterById(int id);
        Task<int> AddCharacter(Character newCharacter);
        Task<List<Character>?> UpdateCharacter(Character updatedCharacter);
        Task DeleteCharacter(int id);

    }
}