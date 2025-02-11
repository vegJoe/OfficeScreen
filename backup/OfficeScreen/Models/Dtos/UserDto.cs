namespace OfficeScreen.Models.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsAdmin { get; set; }
        public string? Status { get; set; }
    }
}
