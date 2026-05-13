using Microsoft.EntityFrameworkCore;
using to_do_backend.Models;

namespace to_do_backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<TaskItem> Tasks {get; set;}
}