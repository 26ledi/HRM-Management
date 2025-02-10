namespace Domain.Entities
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public Guid EvaluationId { get; set; }
        public TaskEvaluation TaskEvaluation { get; set; } = default!;
        public List<TaskAssignment> TaskAssignments { get; set; } = [];
        public List<string> AttachmentUrls { get; set; } = [];
        public string CreatedBy { get; set; } = string.Empty;
        
        public enum TaskPriority
        {
            Low,
            Medium,
            High
        }

        public enum TaskStatus
        {
            Pending,
            InProgress,
            Completed,
            Verified
        }
    }
}
