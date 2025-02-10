namespace Contracts.Requests
{
    public class TaskAssignementRequest
    {
        public Guid UserTaskId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string AssignementStatus { get; set; } = string.Empty;
    }
}
