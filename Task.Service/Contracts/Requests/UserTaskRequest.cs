namespace Contracts.Requests
{
    public class UserTaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public string Priority { get; set; } = string.Empty;
        public Guid TaskEvaluationId { get; set; }
        public Guid? UserId { get; set; }
        public string AttachmentUrl { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
