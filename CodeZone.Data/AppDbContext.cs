using CodeZone.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CodeZone.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext ( DbContextOptions<AppDbContext> options ) : base ( options )
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {
            base.OnModelCreating ( modelBuilder );

            modelBuilder.ApplyConfigurationsFromAssembly ( Assembly.GetExecutingAssembly ( ) );
        }
    }
}