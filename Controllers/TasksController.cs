using Microsoft.AspNetCore.Mvc;
using to_do_backend.Models;
using to_do_backend.Data;
using Microsoft.EntityFrameworkCore;

namespace to_do_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context = null!;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
    {  
        return await _context.Tasks.ToListAsync();    
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTask([FromBody] TaskItem newTask)
    {
        if (string.IsNullOrWhiteSpace(newTask.Title))
        {
            return BadRequest("Task name can not be empty.");
        }
        
        _context.Tasks.Add(newTask);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTasks), new { id = newTask.Id }, newTask);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTask(int id, [FromBody] TaskItem updatedTask)
    {
        if (id != updatedTask.Id)
        {
            return BadRequest("ID mismatch");
        }

        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        task.Title = updatedTask.Title;
        task.Completed = updatedTask.Completed;

        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}