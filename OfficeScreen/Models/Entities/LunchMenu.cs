using System.ComponentModel.DataAnnotations;

namespace OfficeScreen.Models.Entities
{
    public class LunchMenu
    {
        [Required]
        public int Id { get; set; }

        public string WeeklyMenu { get; set; }
    }
}
