namespace DemoEFCoreRelationship.Models.OneToOne
{
    public class CarCompany
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //Relationship: one to one
        public CarModel? CarModel { get; set; } //Navigation property

    }
}
