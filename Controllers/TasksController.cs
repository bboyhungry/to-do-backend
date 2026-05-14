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
    public ActionResult<TaskItem> CreateTask(TaskItem newTask)
    {
        if (string.IsNullOrWhiteSpace(newTask.Title))
        {
            return BadRequest("Task name can not be empty.");
        }
        
        _context.Tasks.Add(newTask);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetTasks), new { id = newTask.Id }, newTask);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteTask(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null) return NotFound();
        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPut("{id}")]
    public ActionResult UpdateTask(int id, TaskItem updatedTask)
    {
        if (id != updatedTask.Id)
        {
            return BadRequest("ID mismatch");
        }

        _context.Entry(updatedTask).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        } 
        catch(DbUpdateConcurrencyException)
        {
            if (!_context.Tasks.Any(t => t.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }
}