namespace HRManagement.Message.Shared
{
    public class UserDeletedMessage
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
