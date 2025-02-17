namespace Contracts.DTO_s
{
    public class TaskReportDto
    {
        public int TotalTasksAssigned { get; set; }
        public int TasksCompletedOnTime { get; set; }
        public double AverageTaskDelay { get; set; }
    }
}
