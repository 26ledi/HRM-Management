namespace Contracts.Responses
{
    public class TaskAssignementResponse
    {
        public Guid Id { get; set; }
        public Guid EmployeeTaskId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string AssignementStatus { get; set; } = string.Empty;
    }
}
