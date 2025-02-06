using OfficeScreen.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OfficeScreen.Data
{
    public class ODDSApiContext: IdentityDbContext<User>
    {
        public ODDSApiContext(DbContextOptions<ODDSApiContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=auth.db");
            }
        }
    }
}
