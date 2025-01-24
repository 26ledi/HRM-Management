namespace HRManagement.Auth.API.Requests
{
    public class UserUpdateRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; }= string.Empty;
    }
}
