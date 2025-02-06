using System.ComponentModel.DataAnnotations;

namespace OfficeScreen.Models.Entities
{
    public class Webcomic
    {
        [Required]
        public int Id { get; set; }
        public string? Text { get; set; }
        public bool? IsOnline { get; set; }
        public string? FilePath { get; set; }
        public string? Url { get; set; }
    }
}
