using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Data;
using TaskManager.API.Models;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskRepository repository, ILogger<TasksController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTasks()
        {
            try
            {
                var tasks = await _repository.GetAllTasksAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tasks");
                return StatusCode(500, "An error occurred while retrieving tasks");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTask>> GetTask(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Invalid task id");

            try
            {
                var task = await _repository.GetTaskByIdAsync(id);
                
                if (task == null)
                    return NotFound($"Task with ID {id} not found");
                
                return Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task {Id}", id);
                return StatusCode(500, $"An error occurred while retrieving task {id}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoTask>> CreateTask(CreateTaskDto taskDto)
        {
            if (taskDto == null)
                return BadRequest("Task data is required");
            
            if (string.IsNullOrWhiteSpace(taskDto.Title))
                return BadRequest("Title is required");

            try
            {
                var createdTask = await _repository.CreateTaskAsync(taskDto);
                return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task");
                return StatusCode(500, "An error occurred while creating the task");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoTask>> UpdateTask(string id, UpdateTaskDto taskDto)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Invalid task id");
            
            if (taskDto == null)
                return BadRequest("Task data is required");
            
            if (id != taskDto.Id)
            {
                taskDto.Id = id;
            }

            try
            {
                var updatedTask = await _repository.UpdateTaskAsync(taskDto);
                
                if (updatedTask == null)
                    return NotFound($"Task with ID {id} not found");
                
                return Ok(updatedTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task {Id}", id);
                return StatusCode(500, $"An error occurred while updating task {id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Invalid task id");

            try
            {
                var result = await _repository.DeleteTaskAsync(id);
                
                if (!result)
                    return NotFound($"Task with ID {id} not found");
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task {Id}", id);
                return StatusCode(500, $"An error occurred while deleting task {id}");
            }
        }
    }
}
