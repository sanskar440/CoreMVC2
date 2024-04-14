using CoreMVC2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace CoreMVC2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
          
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DataAccess.Models.Persons> Persons { get; set; } = default!;

    }
}
