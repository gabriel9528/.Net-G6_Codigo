using DataNotations.Models.ManyToMany;
using DataNotations.Models.ManyToMany.Example2;
using DataNotations.Models.OneToMany;
using DataNotations.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace DataNotations.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Relation One To One
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarCompany> CarCompanies { get; set; }

        //Relation One To Many
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        //Relation Many to Many
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentsCourse { get; set; }

        //Relation Many to Many 2
        public DbSet<Business> Businesss { get; set; }
        public DbSet<Person> Persons { get; set; }
    }

}
