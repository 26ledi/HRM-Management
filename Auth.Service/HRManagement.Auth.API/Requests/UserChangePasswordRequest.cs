namespace HRManagement.Auth.API.Requests
{
    /// <summary>
    /// The request to change the password of the user
    /// </summary>
    public class UserChangePasswordRequest
    {
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The current password of the user
        /// </summary>
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// The new password of the user
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}
