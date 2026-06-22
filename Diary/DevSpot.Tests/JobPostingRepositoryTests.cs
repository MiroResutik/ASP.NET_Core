using DevSpot.Data;
using DevSpot.Models;
using DevSpot.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevSpot.Tests
{
    public class JobPostingRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("JobPostingDb")
                .Options;
        }

        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);

        [Fact]
        public async Task AddAsync_ShouldAddJobPosting()
        {
            // db context
            var db = CreateDbContext();

            // job posting reposotory instance
            var repository = new JobPostingRepository(db);

            // job posting to add
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Location",
                UserId = "Test UserId"
            };

            // execute
            await repository.AddAsync(jobPosting);

            // result?
            var result = db.JobPostings.SingleOrDefault(x => x.Description == "Test Description");

            // assert
            Assert.NotNull(result);
            Assert.Equal("Test Description", result.Description);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobPosting()
        {
            // db context
            var db = CreateDbContext();

            // job posting reposotory instance
            var repository = new JobPostingRepository(db);

            // job posting to add
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Location",
                UserId = "Test UserId"
            };

            // execute
             await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            // result?
            var result = await repository.GetByIdAsync(jobPosting.Id);

            // assert
            Assert.NotNull(result);
            Assert.Equal("Test Title", result.Title);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => repository.GetByIdAsync(999));
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllJobPostings()
        {
            // db context
            var db = CreateDbContext();

            // job posting reposotory instance
            var repository = new JobPostingRepository(db);

            // job posting to add
            var jobPosting2 = new JobPosting
            {
                Title = "Test Title One",
                Description = "Test Description One",
                PostedDate = DateTime.Now,
                Company = "Test Company One",
                Location = "Test Location One",
                UserId = "Test UserId One"
            };

            // job posting to add
            var jobPosting3 = new JobPosting
            {
                Title = "Test Title Two",
                Description = "Test Description Two",
                PostedDate = DateTime.Now,
                Company = "Test Company Two",
                Location = "Test Location Two",
                UserId = "Test UserId"
            };

            // execute
            await db.JobPostings.AddRangeAsync(jobPosting2,jobPosting3);
            await db.SaveChangesAsync();

            // result?
            var result = await repository.GetAllAsync();

            // assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateJobPostings()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

                        // job posting to add
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Location",
                UserId = "Test UserId"
            };
            // execute
            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            jobPosting.Description = "Updated Description";

            await repository.UpdateAsync(jobPosting);

            // result?
            var result = db.JobPostings.Find(jobPosting.Id);

            // assert
            Assert.NotNull(result);
            Assert.Equal("Updated Description", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteJobPostings()
        {
            var db = CreateDbContext();

            var repository = new JobPostingRepository(db);

            // job posting to add
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Location",
                UserId = "Test UserId"
            };
            // execute
            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            await repository.DeleteAsync(jobPosting.Id);

            var result = db.JobPostings.Find(jobPosting.Id);

            Assert.Null(result);

        }


    }
}
