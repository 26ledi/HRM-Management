namespace Domain.Entities
{
    public class TaskEvaluation
    {
        public Guid Id { get; set; }
        public string EvaluatedBy { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime DateEvaluation { get; set; }
        public Guid UserTaskId { get; set; }
        public UserTask UserTask { get; set; } = null!;
    }
}
