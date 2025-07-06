using Microsoft.EntityFrameworkCore;
using PruebaBack_Senegocia.Models.Entities;

namespace PruebaBack_Senegocia.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student_Course> Students_Courses { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student_Course>() //Se guarda como 0 o 1 el enum de estado, para mayor legibilidad esta la transformación
            .Property(sc => sc.Status)
            .HasConversion<string>();
        }

    }
}
