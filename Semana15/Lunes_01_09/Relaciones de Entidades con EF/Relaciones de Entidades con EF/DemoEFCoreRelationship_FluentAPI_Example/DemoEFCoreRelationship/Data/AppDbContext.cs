using DemoEFCoreRelationship.Models.ManyToMany.Exercise1;
using DemoEFCoreRelationship.Models.ManyToMany.Exercise2;
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

        //One-To-One
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarCompany> CarCompanies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }


        //One-To-many
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }


        //Many-To-Many
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        //*--------------------------*-----------------------------
        public DbSet<Person> Persons { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<PersonBusiness> PersonBusinesses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to One Relationship between CarCompany(parent) and CarModel(child)
            modelBuilder.Entity<CarCompany>()
                .HasOne(a => a.CarModel)
                .WithOne(b => b.CarCompany)
                .HasForeignKey<CarModel>(x => x.CarCompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(a => a.EmployeeAddress)
                .WithOne(b => b.Employee)
                .HasForeignKey<EmployeeAddress>(x => x.EmployeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //One to Many Relationship between Doctor(parent) and Patient(child)
            modelBuilder.Entity<Doctor>()
                .HasMany(a => a.Patients)
                .WithOne(b => b.Doctor)
                .HasForeignKey(x => x.DoctorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(x => x.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            //Many to Many Relationship between Student and Subject
            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.HasKey(x => new { x.StudentId, x.SubjectId });

                entity
                .HasOne(a => a.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

                entity
                .HasOne(a => a.Subject)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<PersonBusiness>(entity =>
            {
                entity.HasKey(x => new { x.PersonId, x.BusinessId });

                entity
                .HasOne(a => a.Person)
                .WithMany(s => s.PersonBusinesses)
                .HasForeignKey(ss => ss.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

                entity
                .HasOne(a => a.Business)
                .WithMany(s => s.PersonBusinesses)
                .HasForeignKey(ss => ss.BusinessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
