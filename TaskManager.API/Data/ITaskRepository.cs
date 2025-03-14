using TaskManager.API.Models;

namespace TaskManager.API.Data
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TodoTask>> GetAllTasksAsync();
        Task<TodoTask?> GetTaskByIdAsync(string id);
        Task<TodoTask> CreateTaskAsync(CreateTaskDto taskDto);
        Task<TodoTask?> UpdateTaskAsync(UpdateTaskDto taskDto);
        Task<bool> DeleteTaskAsync(string id);
    }
}
