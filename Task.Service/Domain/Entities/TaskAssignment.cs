namespace Domain.Entities
{
    public class TaskAssignment
    {
        public Guid Id { get; set; }
        public Guid UserTaskId { get; set; }
        public UserTask UserTask { get; set; } = default!;
        public string UserEmail { get; set; } = string.Empty;
        public AssignementStatus assignementStatus { get; set; }
    }

    public enum AssignementStatus 
    {
        Assigned,
        InProgress,
        Completed,
        Verified
    }
}
