using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NetTrackAPI.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }


        public DbSet<Contact> Contact { get; set; }
        public DbSet<Alert> Alert { get; set; }
        public DbSet<Image> Image { get; set; }

        
    }
}
