namespace Domain.Constants
{
    public static class AssignementStatus
    {
        public const string Assigned = "Assigned";
        public const string InProgress = "Assigned";
        public const string Completed = "Assigned";
        public const string Verified = "Verified";

        public static bool IsValidStatus(string status)
        {
            return status == Assigned ||
                   status == InProgress ||
                   status == Completed ||
                   status == Verified;
        }
    }
}
