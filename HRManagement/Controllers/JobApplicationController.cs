using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models;
using HRManagement.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Controllers
{
    /// <summary>
    /// Controller for job application
    /// </summary>
    [Route("[controller]")]
    public class JobApplicationController : Controller
    {
        /// <summary>
        /// Data context for job application
        /// </summary>
        DataContext _context;

        /// <summary>
        /// Creates new job application controller
        /// </summary>
        /// <param name="context">Data context</param>
        public JobApplicationController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Apply for get
        /// </summary>
        /// <param name="offerId">Job offer's id</param>
        /// <returns>Job application's view</returns>
        [HttpGet]
        public IActionResult ApplyFor(int offerId)
        {
            var jobApp = new JobApplication();
            jobApp.JobOfferId = offerId;
            return View(jobApp);
        }

        /// <summary>
        /// Apply for post
        /// </summary>
        /// <param name="jobApplication">Jop applicaiton model</param>
        /// <returns>Redirect to job offer's details</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ApplyFor([FromForm]JobApplication jobApplication)
        {
            if (!ModelState.IsValid)
            {
                return View(jobApplication);
            }
            JobApplication newJobApp = new JobApplication();
            newJobApp.ContactAgreement = jobApplication.ContactAgreement;
            newJobApp.CvUrl = jobApplication.CvUrl;
            newJobApp.EmailAddress = jobApplication.EmailAddress;
            newJobApp.FirstName = jobApplication.FirstName;
            newJobApp.LastName = jobApplication.LastName;
            newJobApp.PhoneNumber = jobApplication.PhoneNumber;
            newJobApp.JobOfferId = jobApplication.JobOfferId;
            var offer = _context.JobOffers.Include(x => x.JobApplications).FirstOrDefault(x => x.Id == jobApplication.JobOfferId);
            offer.JobApplications.Add(newJobApp);
            await _context.JobApplications.AddAsync(newJobApp);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "JobOffer", new { id = newJobApp.JobOfferId });
        }
    }   
}