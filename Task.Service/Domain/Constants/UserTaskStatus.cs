namespace Domain.Constants
{
    public static class UserTaskStatus
    {
        public const string Created = "Created";
        public const string Pending = "Pending";
        public const string InProgress = "InProgress";
        public const string Completed = "Completed";
        public const string Verified = "Verified";

        public static bool IsValidStatus(string status)
        {
            return status == Pending ||
                   status == InProgress ||
                   status == Completed ||
                   status == Verified;
        }
    }
}
