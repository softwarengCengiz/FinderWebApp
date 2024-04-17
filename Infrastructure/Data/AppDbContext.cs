using Domain.Participant;
using Domain.Student;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
		public DbSet<Participant> Participants { get; set; }
		public DbSet<User> Users { get; set; }
    }
}
