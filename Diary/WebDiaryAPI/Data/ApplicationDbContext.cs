using Microsoft.EntityFrameworkCore;
using WebDiaryAPI.Models;

namespace WebDiaryAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Create a Table of type DiaryEntries 
        public DbSet<DiaryEntry> DiaryEntries { get; set; }
    }
}
