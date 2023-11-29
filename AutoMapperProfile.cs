using AutoMapper;
using dotnet_todo.DTOs.Characters;
using dotnet_todo.Models;

namespace dotnet_todo
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Character>();
            CreateMap<GetCharacterDTO, Character>();
        }

    }
}