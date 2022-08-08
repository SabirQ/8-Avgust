using API_Task_08_08.DAL.Configurations;
using API_Task_08_08.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Task_08_08.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
