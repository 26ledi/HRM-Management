namespace Domain.Entities
{
    public class TaskEvaluation
    {
        public Guid Id { get; set; }
        public string EvaluatedBy { get; set; } = string.Empty;
        public List<UserTask> UserTasks { get; set; } = [];
        public int Rating { get; set; }
        public DateTime DateEvaluation { get; set; }
    }
}
