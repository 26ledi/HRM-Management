namespace HRManagement.Auth.API.Responses
{
    /// <summary>
    /// The register response which the user will receive
    /// </summary>
    public class UserRegisterResponse
    {
        /// <summary>
        /// The user's register response name
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// The user's register response email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The user role
        /// </summary>
        public string Role { get; set; } = string.Empty;
    }
}
