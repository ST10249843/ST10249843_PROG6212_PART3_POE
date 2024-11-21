using CMCS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMCS.Models
{
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
