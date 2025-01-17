namespace HRManagement.BusinessLogic.DTOs
{
    /// <summary>
    /// Represent the Data Transfer Object of the token
    /// </summary>
    public class TokenDto
    {
        public string Id { get; set; }
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The role of the user
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// The username of the user
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// The jwt token 
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// The refresh token 
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// The duration of the token in minutes
        /// </summary>
        public int DurationInMinutes { get; set; }
    }
}

