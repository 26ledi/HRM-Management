namespace Contracts.Requests
{
    public class TaskEvaluationRequest
    {
        public int Rating { get; set; }
        public DateTime DateEvaluation { get; set; } = DateTime.Now;
    }
}
