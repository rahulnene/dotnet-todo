using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            var response = new ServiceResponse<List<GetCharacterDTO>>
            {
                Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList()
            };
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            try
            {
                var character = characters.FirstOrDefault(c => c.Id == id) ?? throw new Exception($"Character with id {id} not found.");
                characters.Remove(character);
                serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDTO>>
            {
                Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList()
            };
            return response;
        }


        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharactersFromDatabase()
        {
            var characters = await _context.Characters.ToListAsync();
            var response = new ServiceResponse<List<GetCharacterDTO>>
            {
                Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList()
            };
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            var response = new ServiceResponse<GetCharacterDTO>
            {
                Data = _mapper.Map<GetCharacterDTO>(character)
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> SaveCharactersToDatabase(List<GetCharacterDTO> characters)
        {
            var serviceResponse = new ServiceResponse<bool>();
            var counter = 0;
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
                    counter += db.ChangeTracker.Entries().Count(e => e.State == EntityState.Added || e.State == EntityState.Modified);
                    db.SaveChanges();
                }
                await db.SaveChangesAsync();
                int numberOfObjectsWritten = counter;
                Console.WriteLine($"{numberOfObjectsWritten} records saved to database");
                if (numberOfObjectsWritten > 0)
                {
                    serviceResponse.Data = true;
                    serviceResponse.Message = $"{numberOfObjectsWritten} characters saved to database.";
                }
                else
                {
                    serviceResponse.Data = false;
                    serviceResponse.Message = "No characters were saved to the database.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error saving characters: {ex}";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            try
            {
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id) ?? throw new Exception($"Character with id {updatedCharacter.Id} not found.");
                character.Name = updatedCharacter.Name;
                character.Class = updatedCharacter.Class;
                character.Defense = updatedCharacter.Defense;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Strength = updatedCharacter.Strength;
                serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}