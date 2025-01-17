namespace HRManagement.BusinessLogic.DTOs
{ /// <summary>
  /// Represents a Data Transfer Object (DTO) for a user.
  /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the user's identifier.
        /// </summary>
        public Guid Id { get; set; } 

        /// <summary>
        /// The name of the user
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        /// 
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        /// 
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The role of the user
        /// </summary>
        public string Role { get; set; } = string.Empty;
    }
}
