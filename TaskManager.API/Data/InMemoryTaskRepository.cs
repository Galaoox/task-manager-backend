using TaskManager.API.Models;

namespace TaskManager.API.Data
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly List<TodoTask> _tasks = new();

        public Task<IEnumerable<TodoTask>> GetAllTasksAsync()
        {
            return Task.FromResult<IEnumerable<TodoTask>>(_tasks.ToList());
        }

        public Task<TodoTask?> GetTaskByIdAsync(string id)
        {
            return Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));
        }

        public Task<TodoTask> CreateTaskAsync(CreateTaskDto taskDto)
        {
            if (string.IsNullOrWhiteSpace(taskDto.Title))
                throw new ArgumentException("Title cannot be empty", nameof(taskDto));

            var task = new TodoTask
            {
                Title = taskDto.Title,
                Description = taskDto.Description ?? string.Empty,
                Completed = false,
                CreatedAt = DateTime.UtcNow
            };

            _tasks.Add(task);
            return Task.FromResult(task);
        }

        public Task<TodoTask?> UpdateTaskAsync(UpdateTaskDto taskDto)
        {
            if (string.IsNullOrWhiteSpace(taskDto.Id))
                throw new ArgumentException("Id cannot be empty", nameof(taskDto));

            var task = _tasks.FirstOrDefault(t => t.Id == taskDto.Id);
            if (task == null)
                return Task.FromResult<TodoTask?>(null);

            if (taskDto.Title != null)
                task.Title = taskDto.Title;
            
            if (taskDto.Description != null)
                task.Description = taskDto.Description;
            
            if (taskDto.Completed.HasValue)
                task.Completed = taskDto.Completed.Value;

            return Task.FromResult<TodoTask?>(task);
        }

        public Task<bool> DeleteTaskAsync(string id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return Task.FromResult(false);

            _tasks.Remove(task);
            return Task.FromResult(true);
        }
    }
}
