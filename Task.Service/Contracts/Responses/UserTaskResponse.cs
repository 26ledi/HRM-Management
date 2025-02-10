namespace Contracts.Responses
{
    public class UserTaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public IEnumerable<string> AssignedUserEmails { get; set; } = new List<string>();
        public IEnumerable<string> AttachmentUrls { get; set; } = new List<string>();
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
