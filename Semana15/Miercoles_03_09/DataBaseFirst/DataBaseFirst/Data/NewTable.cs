using System.ComponentModel.DataAnnotations;

namespace DataBaseFirst.Data
{
    public class NewTable
    {
        public int Id { get; set; }


        [StringLength(200)]
        public string?Name { get; set; }
        public string? Description { get; set; }

        [StringLength(200)]
        public string? Type { get; set; }

        public string? Columna3 { get; set; }
    }
}
