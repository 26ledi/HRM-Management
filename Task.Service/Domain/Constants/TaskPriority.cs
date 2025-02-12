namespace Domain.Constants
{
    public static class TaskPriority
    {
        public const string Low = "Low";
        public const string Medium = "Medium";
        public const string High = "High";

        public static bool IsValidStatus(string priority)
        {
            return priority == Low ||
                   priority == Medium ||
                   priority == High;
        }
    }
}
