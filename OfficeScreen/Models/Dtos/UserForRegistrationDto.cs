using System.ComponentModel.DataAnnotations;

namespace OfficeScreen.Models.Dtos
{
    public class UserForRegistrationDto

    {
               
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }


        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Nmae is required")]
        public string? LastName { get; set; } = string.Empty;

        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required")]
        public bool? Admin { get; set; } = false;

        public string? Fullname => $"{FirstName} {LastName}";
    }
}
