using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.BLL.Interfaces;
using TaskManagerAPI.DAL.Models;
using TaskManagerAPI.DAL.TaskManagerDataContext;

namespace TaskManagerAPI.BLL.Services
{
    public class TaskItemService : ITaskItem
    {
        private readonly ApplicationDbContext _context;
        public TaskItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int id, int userId)
        {
            var existingTaskItem = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (existingTaskItem == null)
            {
                return false;
            }
            _context.TaskItems.Remove(existingTaskItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id, int userId)
        {
            return await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.TaskItems.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<TaskItem> UpdateTaskAsync(TaskItem task, int userId)
        {
            var existingTaskItem = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == task.Id  && t.UserId == userId);
            if (existingTaskItem != null)
            {
                existingTaskItem.Title = task.Title;
                existingTaskItem.Description = task.Description;
                existingTaskItem.DueDate = task.DueDate;
                existingTaskItem.IsCompleted = task.IsCompleted;

                _context.TaskItems.Update(existingTaskItem);
                await _context.SaveChangesAsync();
            }
            return existingTaskItem ?? throw new KeyNotFoundException("Task not found.");
        }
    }
}
