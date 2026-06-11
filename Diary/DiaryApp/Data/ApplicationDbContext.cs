using DiaryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        // Create a Table of type DiaryEntries 
        public DbSet<DiaryEntry> DiaryEntries { get; set; }

        // Method to seed the data into a database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DiaryEntry>().HasData(
                new DiaryEntry 
                { 
                    DiaryEntryId = 1, 
                    Title="Went Hiking", 
                    Content="Went hiking with Miro!",
                    Created = DateTime.Today
                },
                new DiaryEntry
                {
                    DiaryEntryId = 2,
                    Title = "Went Biking",
                    Content = "Went biking with Miro!",
                    Created = DateTime.Today
                },
                new DiaryEntry
                {
                    DiaryEntryId = 3,
                    Title = "Went Shoping",
                    Content = "Went Shoping with Miro!",
                    Created = DateTime.Today
                },
                new DiaryEntry
                {
                    DiaryEntryId = 4,
                    Title = "Went Diving",
                    Content = "Went Diving with Miro!",
                    Created = DateTime.Today
                }
                );
        }
    }
}
