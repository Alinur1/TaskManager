using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using TaskManagerAPI.BLL.Interfaces;
using TaskManagerAPI.DAL.Models;

namespace TaskManagerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskItem _taskService;
        public TaskController(ITaskItem taskService)
        {
            _taskService = taskService;
        }

        private int GetUserId() =>
        int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var userId = GetUserId();
            var tasks = await _taskService.GetTasksByUserIdAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var userId = GetUserId();
            var task = await _taskService.GetTaskByIdAsync(id, userId);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
            task.UserId = GetUserId();
            var created = await _taskService.AddTaskAsync(task);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItem task)
        {
            task.Id = id;
            task.UserId = GetUserId();
            var updated = await _taskService.UpdateTaskAsync(task, task.UserId);
            if (updated == null)
            {
                return NotFound();
            }
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var success = await _taskService.DeleteTaskAsync(id, userId);
            return success ? Ok(new { message = "Deleted" }) : NotFound();
        }
    }
}
