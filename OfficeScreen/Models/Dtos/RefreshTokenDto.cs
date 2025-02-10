namespace OfficeScreen.Models.Dtos
{
    public class RefreshTokenDto
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpireTime { get; set; }

        public bool Admin { get; set; }
    }
}
