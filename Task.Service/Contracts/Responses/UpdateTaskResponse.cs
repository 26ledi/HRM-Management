﻿namespace Contracts.Responses
{
    public class UpdateTaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TaskEvaluation { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public string AttachmentUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
