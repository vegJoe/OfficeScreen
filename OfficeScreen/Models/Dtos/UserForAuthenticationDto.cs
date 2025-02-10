using System.ComponentModel.DataAnnotations;

namespace OfficeScreen.Models.Dtos
{
    public class UserForAuthenticationDto
    {
        
       
        [Required]
        public string? UserName { get; init; }

        [Required]
        public string Password { get; init; } = string.Empty;   
    }
}
