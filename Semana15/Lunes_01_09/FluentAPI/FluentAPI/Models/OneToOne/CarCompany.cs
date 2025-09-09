using System.Text.Json.Serialization;

namespace FluentAPI.Models.OneToOne
{
    public class CarCompany
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public CarModel? CarModel { get; set; }//propiedad de navegacion
    }
}
