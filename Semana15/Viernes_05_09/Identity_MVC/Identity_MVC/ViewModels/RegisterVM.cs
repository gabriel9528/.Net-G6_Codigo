using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Identity_MVC.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Name is Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Nombres is Required")]
        public string? Nombres { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password doesn't match")]
        [DisplayName("Confirm Password")]
        public string? ConfirmPassword { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }

        [Required(ErrorMessage ="Role is required")]
        public string? SelectedRole { get; set; }

        //Lista de roles disponibles
        public List<SelectListItem>? Roles { get; set; }
    }
}
