namespace HRManagement.Messages.Shared
{
    public  class UserUpdatedMessage
    {
        public Guid Id { get; set; }
        public string UserEmailToUpdate { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
