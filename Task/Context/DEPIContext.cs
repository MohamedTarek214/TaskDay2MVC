using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Task.Context
{
    public class DEPIContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StuCrsRes> StuCrsRes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DEPI;Integrated Security=True;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
            .HasOne(s => s.Department)
            .WithMany(d => d.Students)
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Teacher>()
            .HasOne(t => t.Department)
            .WithMany(d => d.Teachers)
            .HasForeignKey(t => t.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
            .HasOne(c => c.Department)
            .WithMany(d => d.Courses)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Teacher>()
            .HasOne(t => t.Course)
            .WithMany(c => c.Teachers)
            .HasForeignKey(t => t.CourseId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StuCrsRes>()
            .HasOne(r => r.Student)
            .WithMany(s => s.CourseResults)
            .HasForeignKey(r => r.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StuCrsRes>()
            .HasOne(r => r.Course)
            .WithMany(c => c.StudentResults)
            .HasForeignKey(r => r.CourseId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StuCrsRes>()
            .HasKey(r => new { r.StudentId, r.CourseId });

            modelBuilder.Entity<Teacher>()
            .Property(t => t.Salary)
            .HasPrecision(10, 2);

            modelBuilder.Entity<Department>()
            .Property(d => d.Name)
            .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .IsRequired();

            modelBuilder.Entity<Teacher>()
                .Property(t => t.Name)
                .IsRequired();

            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

    }
}
