using System.ComponentModel.DataAnnotations;

namespace OfficeScreen.Models.Entities
{
    public class StatusMessage
    {
        [Required]
        public int Id { get; set; }
        public string? Message { get; set; }
    }
}
