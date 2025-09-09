using DemoEFCoreRelationship.Models.ManyToMany.Exercise1;

namespace DemoEFCoreRelationship.Models.ManyToMany.Exercise2
{
    public class PersonBusiness
    {
        public int PersonId { get; set; }
        public Person? Person { get; set; }

        public int BusinessId { get; set; }
        public Business? Business { get; set; }
    }
}
