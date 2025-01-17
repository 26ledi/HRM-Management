namespace HRManagement.Message.Shared
{
    public class UserResetPasswordMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ConfirmationCode { get; set; } = string.Empty;
    }
}
