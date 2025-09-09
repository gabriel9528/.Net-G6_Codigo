using FluentAPI.Models.ManyToMany;
using FluentAPI.Models.OneToMany;
using FluentAPI.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace FluentAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Relacion de uno a uno
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarCompany> CarCompanies { get; set; }

        //Relacion de uno a muchos
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relacion de Uno a uno entre CarModel y CarCompany
            modelBuilder.Entity<CarCompany>()
                .HasOne(a => a.CarModel).WithOne(x => x.CarCompany)
                .HasForeignKey<CarModel>(p => p.CarCompanyId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            //Relacion de uno a muchos entre Doctor y Paciente
            //donde 1 Doctor puede tener muchos pacientes
            modelBuilder.Entity<Doctor>()
                .HasMany(a => a.Patients).WithOne(x => x.Doctor)
                .HasForeignKey(x => x.DoctorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);


            //Relacion de muchos a muchos
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(x => new { x.StudentId, x.CourseId });

                entity
                .HasOne(x => x.Student).WithMany(a => a.StudentCourses)
                .HasForeignKey(p => p.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                entity
                .HasOne(X => X.Course).WithMany(a => a.StudentCourses)
                .HasForeignKey(p => p.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}
