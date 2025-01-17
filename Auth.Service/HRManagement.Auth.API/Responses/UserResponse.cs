namespace HRManagement.Auth.API.Responses
{
    /// <summary>
    /// The login response which the user will receive
    /// </summary>
    public class UserResponse
    {
        public string Id { get; set; }
        /// <summary>
        /// The user's register name
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// User's email
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// User's Role
        /// </summary>
        public string Role { get; set; } = string.Empty;
        /// <summary>
        /// User's Token
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;
    }
}
