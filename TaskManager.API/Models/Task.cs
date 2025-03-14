namespace TaskManager.API.Models
{
    public class TodoTask
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class UpdateTaskDto
    {
        public string Id { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? Completed { get; set; }
    }
}
