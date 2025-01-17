namespace HRManagement.Auth.API.Requests
{
    /// <summary>
    /// The user confirm request
    /// </summary>
    public class UserConfirmPasswordRequest
    {
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The confirmation code of the password
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// The new password of the user
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}
