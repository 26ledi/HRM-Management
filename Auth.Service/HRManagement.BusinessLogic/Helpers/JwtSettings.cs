namespace HRManagement.BusinessLogic.Helpers
{
    /// <summary>
    /// Represents the settings used for configuring JWT (JSON Web Token) authentication.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Gets or sets the secret key used to sign the JWT. 
        /// This key should be kept secure and used to validate the token's integrity.
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the audience for the JWT. 
        /// This typically represents the recipients that the JWT is intended for (e.g., the client application).
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the issuer of the JWT. 
        /// This usually represents the authority that issued the token (e.g., the authentication server).
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the duration (in minutes) for which the JWT is valid. 
        /// After this time, the token will expire and cannot be used for authentication.
        /// </summary>
        public int DurationInMinutes { get; set; }
    }
}
