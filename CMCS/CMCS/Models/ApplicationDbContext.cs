using Microsoft.EntityFrameworkCore;

namespace CMCS.Models
{
    /*
    Author: Microsoft
    Date: N/A
    Title: DbContext Configuration - Entity Framework Core
    Type: Documentation
    Availability: Microsoft Learn, https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/
    */
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // DbSets represent tables in the database
        public DbSet<Claim> Claims { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
