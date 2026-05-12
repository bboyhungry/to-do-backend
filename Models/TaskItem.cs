namespace to_do_backend.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public bool Completed { get; set; }
}