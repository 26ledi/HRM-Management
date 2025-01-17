namespace HRManagement.Auth.API.Requests
{
    /// <summary>
    /// The login request which the user will send
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        /// The user's email
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// The user's password
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
