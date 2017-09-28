using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Data
{
    using Microsoft.EntityFrameworkCore;
    
    public class StudentsystemDBContext : DbContext
    {
        public DbSet<Student> Students{ get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudentSystemDb;Integrated Security=True;");

            

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Many-to-Many relation between Student and Course

            builder
                .Entity<StudentCourses>()
                .HasKey(sc => new {sc.StudentId, sc.CourseId});

            builder
                .Entity<StudentCourses>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(s => s.CourseId);

            builder
                .Entity<StudentCourses>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(c => c.StudentId);

            //One-to-Many between Course and Resources

            builder
                .Entity<Course>()
                .HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId);

            //One-to-Many between Course and Homework

            builder
                .Entity<Course>()
                .HasMany(c => c.Homeworks)
                .WithOne(h => h.Course)
                .HasForeignKey(h => h.CourseId);

            //One-to-Many between Student and Homework

            builder
                .Entity<Student>()
                .HasMany(s => s.Homeworks)
                .WithOne(h => h.Student)
                .HasForeignKey(h => h.StudentId);
            base.OnModelCreating(builder);
        }
    }
}
