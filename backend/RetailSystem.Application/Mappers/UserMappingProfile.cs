using AutoMapper;
using RetailSystem.SharedLibrary.Dtos.Users;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
