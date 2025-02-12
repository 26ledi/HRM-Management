namespace Notification.Service.Configurations
{
    public class EmailSettings
    {
        public string EmailUsername { get; set; } = string.Empty;
        public string EmailHost { get; set; } = string.Empty;
        public int EmailPort { get; set; }
        public string EmailPassword { get; set; } = string.Empty;
    }
}
