using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataNotations.Models.OneToMany
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
}
