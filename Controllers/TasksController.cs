using Microsoft.AspNetCore.Mvc;
using to_do_backend.Models;

namespace to_do_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static List<TaskItem> tasks = new List<TaskItem>
    {
        new TaskItem { Id = 1, Title = "Hello", Completed = false },
        new TaskItem { Id = 2, Title = "World", Completed = false }
    };

    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetTasks()
    {
        return Ok(tasks);
    }

    [HttpPost]
    public ActionResult<TaskItem> CreateTask(TaskItem newTask)
    {
        newTask.Id = tasks.Count + 1;
        tasks.Add(newTask);

        return CreatedAtAction(nameof(GetTasks), new { id = newTask.Id}, newTask);
    }
}