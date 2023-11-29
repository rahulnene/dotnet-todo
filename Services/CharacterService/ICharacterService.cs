using dotnet_todo.DTOs.Characters;

namespace dotnet_todo.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<List<GetCharacterDTO>?> GetAllCharacters();
        Task<List<GetCharacterDTO>?> GetAllCharactersFromDatabase();
        Task<GetCharacterDTO?> GetCharacterById(int id);
        Task<List<GetCharacterDTO>?> AddCharacter(AddCharacterDTO newCharacter);
        Task<GetCharacterDTO?> UpdateCharacter(UpdateCharacterDTO updatedCharacter);
        Task<List<GetCharacterDTO>?> DeleteCharacter(int id);
        Task SaveCharactersToDatabase(List<GetCharacterDTO> characters);

    }
}