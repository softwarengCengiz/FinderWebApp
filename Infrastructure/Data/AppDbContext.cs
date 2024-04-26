using Domain.Community;
using Domain.Event;
using Domain.Participant;
using Domain.Polling;
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
		public DbSet<Event> Events { get; set; }
		public DbSet<Polling> Pollings { get; set; }
		public DbSet<Community> Communities { get; set; }
    }
}
