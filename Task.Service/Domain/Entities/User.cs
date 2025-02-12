using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        [JsonIgnore]
        public List<UserTask> Tasks { get; set; } = [];
    }
}
