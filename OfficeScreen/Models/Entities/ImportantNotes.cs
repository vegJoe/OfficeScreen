using System.ComponentModel.DataAnnotations;

namespace OfficeScreen.Models.Entities
{
    public class ImportantNotes
    {
        [Required]
        public int Id { get; set; }
        public string? Note {  get; set; }
    }
}
