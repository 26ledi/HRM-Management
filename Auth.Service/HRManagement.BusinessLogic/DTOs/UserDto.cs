﻿namespace HRManagement.BusinessLogic.DTOs
{ 
    public class UserDto
    {
        public string Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
