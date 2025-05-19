using TaskManagerAPI.DAL.Models;

namespace TaskManagerAPI.BLL.Interfaces
{
    public interface ITaskItem
    {
        Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(int userId);
        Task<TaskItem> GetTaskByIdAsync(int id, int userId);
        Task<TaskItem> AddTaskAsync(TaskItem task);
        Task<TaskItem> UpdateTaskAsync(TaskItem task, int userId);
        Task<bool> DeleteTaskAsync(int id, int userId);
    }
}
