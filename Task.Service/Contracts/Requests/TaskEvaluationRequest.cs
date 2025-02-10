namespace Contracts.Requests
{
    public class TaskEvaluationRequest
    {
        public string EvaluatedBy { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime DateEvaluation { get; set; }
    }
}
