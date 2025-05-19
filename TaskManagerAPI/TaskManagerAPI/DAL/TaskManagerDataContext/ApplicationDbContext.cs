using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.DAL.Models;

namespace TaskManagerAPI.DAL.TaskManagerDataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
