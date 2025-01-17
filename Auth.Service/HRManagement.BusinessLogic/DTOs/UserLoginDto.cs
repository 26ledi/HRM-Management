namespace HRManagement.BusinessLogic.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a user's login.
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// The user's register name
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
