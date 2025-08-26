using System.ComponentModel.DataAnnotations;

namespace DapperMVC.Data.Models.Domain
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
