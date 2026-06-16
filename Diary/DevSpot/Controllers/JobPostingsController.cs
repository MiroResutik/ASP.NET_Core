using DevSpot.Models;
using DevSpot.Repositories;
using DevSpot.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;
using DevSpot.Constants;

namespace DevSpot.Controllers
{
    //[Authorize]
    public class JobPostingsController : Controller
    {

        private readonly IRepository<JobPosting> _repository;

        private readonly UserManager<IdentityUser> _userManager;

        public JobPostingsController(IRepository<JobPosting> repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            

            if (User.IsInRole(Roles.Employer))
            {
                var allJobPostings = await _repository.GetAllAsync();
                // Get current logged in user Id
                var userId = _userManager.GetUserId(User);

                var filteredJobPostings = allJobPostings.Where(jp => jp.UserId == userId);

                return View(filteredJobPostings);
            }

            var jobPostings = await _repository.GetAllAsync();
            return View(jobPostings);
        }
        // JobPosting/Delete/5
        [HttpDelete]
        //[Authorize(Roles="Admin,Employer")]
        public async Task<IActionResult> Delete(int id)
        {
            var jobPosting = await _repository.GetByIdAsync(id);

            if (jobPosting == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if(User.IsInRole(Roles.Admin) == false && jobPosting.UserId != userId)
            {
                return Forbid();
            }
            await _repository.DeleteAsync(id);

            return Ok();
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        //[Authorize(Roles="Admin,Employer")]
        public async Task<IActionResult> Create(JobPostingViewModel jobPostingViewModel)
        {

            if (ModelState.IsValid)
            {

                var jobPosting = new JobPosting
                {
                    Title = jobPostingViewModel.Title,
                    Description = jobPostingViewModel.Description,
                    Company = jobPostingViewModel.Company,
                    Location = jobPostingViewModel.Location,
                    UserId = _userManager.GetUserId(User)
                };
                // Load user manualy
                
                await _repository.AddAsync(jobPosting);

                return RedirectToAction(nameof(Index));

            }


            return View(jobPostingViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
