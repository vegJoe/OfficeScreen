using OfficeScreen.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OfficeScreen.Data
{
    public class ODDSApiContext: IdentityDbContext<User>
    {
        public ODDSApiContext(DbContextOptions<ODDSApiContext> options) : base(options) { }

        public DbSet<OfficeScreen.Models.Entities.Webcomic> webcomics { get; set; } = default!;
        public DbSet<OfficeScreen.Models.Entities.ImportantNotes> importantNotes { get; set; } = default!;
        public DbSet<OfficeScreen.Models.Entities.LunchMenu> lunchMenu { get; set; } = default!;
        public DbSet<OfficeScreen.Models.Entities.StatusMessage> statusMessage { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=auth.db");
            }
        }
    }
}
