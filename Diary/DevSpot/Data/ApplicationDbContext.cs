using DevSpot.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DevSpot.Data
{
    // Identiy will allow to create users with specific roles
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<JobPosting> JobPostings { get; set; }

        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    }
}
