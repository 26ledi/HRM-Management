using AutoMapper;
using HRManagement.Auth.API.Requests;
using HRManagement.Auth.API.Responses;
using HRManagement.BusinessLogic.DTOs;
using Microsoft.AspNetCore.Identity;

namespace HRManagement.Auth.API.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<UserRegisterRequest, IdentityUser>()
                .ReverseMap();
            CreateMap<UserRegisterRequest, UserDto>()
                .ReverseMap();
            CreateMap<UserLoginRequest, IdentityUser>()
                .ReverseMap();
            CreateMap<UserLoginRequest, UserLoginDto>()
                .ReverseMap();
            CreateMap<UserLoginDto, UserResponse>()
               .ReverseMap();
            CreateMap<UserUpdateRequest, UserResponse>()
               .ReverseMap();
            CreateMap<UserUpdateRequest, UserDto>()
               .ReverseMap();
            CreateMap<TokenDto, UserLoginResponse>()
                .ReverseMap();
            CreateMap<UserDto, UserRegisterResponse>()
               .ReverseMap();
            CreateMap<UserDto, UserResponse>()
               .ReverseMap();
            CreateMap<UserDto, UserLoginResponse>()
               .ReverseMap();

        }
    }
}
