using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoCapas.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es obligatorio")]
        [DisplayName("Nombre de la categoria")]
        public string Nombre { get; set; }

        [Display(Name = "Orden de la visualizacion")]
        [Range(0, 100, ErrorMessage = "El valor del orden de la categoria debe estar entre 1 y 100")]
        public int? Orden { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
