namespace Contracts.Responses
{
    public class TaskEvaluationResponse
    {
        public Guid Id { get; set; }
        public string EvaluatedBy { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime DateEvaluation { get; set; }
    }
}
