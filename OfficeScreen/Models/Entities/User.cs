using Microsoft.AspNetCore.Identity;

namespace OfficeScreen.Models.Entities
{
    public class User : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpireTime { get; set; }
    }
}
