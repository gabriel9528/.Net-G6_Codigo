using DemoEFCoreRelationship.Models.ManyToMany;
using DemoEFCoreRelationship.Models.OneToMany;
using DemoEFCoreRelationship.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace DemoEFCoreRelationship.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarCompany> CarCompanies { get; set; }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to One Relationship between CarCompany(parent) and CarModel(child)

            //CarCompany es la entidad principal (parent)
            modelBuilder.Entity<CarCompany>()
                .HasOne(a => a.CarModel)                  // CarCompany tiene una relación 1:1 con CarModel
                .WithOne(b => b.CarCompany)                // CarModel tiene una relación 1:1 con CarCompany
                .HasForeignKey<CarModel>(x => x.CarCompanyId) // La clave foránea está en CarModel
                .IsRequired()                              // CarModel NO puede existir sin un CarCompany
                .OnDelete(DeleteBehavior.Cascade);         // Si se elimina CarCompany, también se elimina CarModel






            //One to Many Relationship between Doctor(parent) and Patient(child)
            //Un Doctor puede tener muchos Patients asociados
            //Cada Patient pertenece a un único Doctor
            modelBuilder.Entity<Doctor>()
                .HasMany(a => a.Patients)       // Un Doctor tiene muchos Pacientes
                .WithOne(b => b.Doctor)         // Un Paciente tiene un solo Doctor
                .HasForeignKey(x => x.DoctorId) // La clave foránea está en Patient
                .IsRequired()                   // No puede haber un paciente sin un doctor
                .OnDelete(DeleteBehavior.NoAction); // No se eliminan pacientes al eliminar un doctor





            //Many to Many Relationship between Student and Subject
            modelBuilder.Entity<StudentSubject>(entity =>
            {
                //establece que StudentId y SubjectId juntos forman la clave primaria compuesta.
                //La clave primaria compuesta es necesaria porque StudentSubject no tiene un Id propio
                entity.HasKey(x => new { x.StudentId, x.SubjectId });

                entity
                .HasOne(a => a.Student)             // `StudentSubject` tiene un `Student`
                .WithMany(s => s.StudentSubjects)   // `Student` tiene muchas `StudentSubject`
                .HasForeignKey(ss => ss.StudentId)  //  Clave foránea StudentId en StudentSubject referencia a Student.
                .IsRequired()                        // No puede ser nulo
                .OnDelete(DeleteBehavior.NoAction); // No se eliminan registros en cascada

                entity
                .HasOne(a => a.Subject)             // `StudentSubject` tiene un `Subject`
                .WithMany(s => s.StudentSubjects)   // `Subject` tiene muchas `StudentSubject`
                .HasForeignKey(ss => ss.SubjectId)  // Clave foránea SubjectId en StudentSubject referencia a Subject.
                .IsRequired()                        // No puede ser nulo
                .OnDelete(DeleteBehavior.NoAction); // No se eliminan registros en cascada

            });
            


        }
    }
}
