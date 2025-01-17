using AutoMapper;
using HRManagement.BusinessLogic.DTOs;
using HRManagement.Message.Shared;
using Microsoft.AspNetCore.Identity;

namespace HRManagement.BusinessLogic.Configurations
{
    /// <summary>
    /// Sets up the objects mapping
    /// </summary>
    public class MappingProfiles : Profile
    {
        /// <summary>
        /// Creates an instance of <see cref="MappingProfiles"/> class
        /// </summary>
        public MappingProfiles()
        {
            CreateMap<IdentityUser, UserDto>()
                .ReverseMap();

            CreateMap<UserDto, UserRegisterMessage>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => new Guid(src.Id)));
        }
    }
}
