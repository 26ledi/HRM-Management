using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public TaskEvaluation? TaskEvaluation { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public string AttachmentUrl { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}