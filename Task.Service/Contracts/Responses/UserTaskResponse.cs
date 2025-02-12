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
        public string AttachmentUrl { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public string TaskEvaluation { get; set; } = string.Empty ;
        public Guid? UserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
