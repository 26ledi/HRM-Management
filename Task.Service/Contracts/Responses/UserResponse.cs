namespace Contracts.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int? AssignedTaskNumber { get; set; }
        public int? CompletedMonthlyTaskNumber { get; set; }
    }
}
