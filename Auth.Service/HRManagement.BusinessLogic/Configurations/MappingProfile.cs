using AutoMapper;
using HRManagement.BusinessLogic.DTOs;
using HRManagement.Message.Shared;
using Microsoft.AspNetCore.Identity;

namespace HRManagement.BusinessLogic.Configurations
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IdentityUser, UserDto>()
                .ReverseMap();

            CreateMap<UserDto, UserRegisterMessage>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => new Guid(src.Id)));
        }
    }
}
