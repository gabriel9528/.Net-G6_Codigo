namespace FluentAPI.Models.OneToMany
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DoctorId { get; set; }//Foreign key
        public virtual Doctor? Doctor { get; set; }
    }
}
