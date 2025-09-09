namespace DemoEFCoreRelationship.Models.OneToMany
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //Relationship: many to one
        public int DoctorId { get; set; } //ForeignKey
        //constraint fk_Patient_Doctor foreign key (DoctorId) references Doctor(Id)
        public Doctor? Doctor { get; set; }
    }
}
