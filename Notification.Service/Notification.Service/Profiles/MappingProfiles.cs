using AutoMapper;
using HRManagement.Message.Shared;
using Notification.Service.Models;

namespace Notification.Service.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserRegisterMessage, EmailReceiver>();
        }
    }
}
