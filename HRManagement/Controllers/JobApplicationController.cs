using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.Models;
using HRManagement.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

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
        private string apiKey;

        /// <summary>
        /// Creates new job application controller
        /// </summary>
        /// <param name="context">Data context</param>
        public JobApplicationController(DataContext context, IConfiguration config)
        {
            apiKey = config.GetSection("SengridApiKey").Value;
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
            JobApplication newJobApplication = new JobApplication();
            newJobApplication.ContactAgreement = jobApplication.ContactAgreement;
            newJobApplication.CvUrl = jobApplication.CvUrl;
            newJobApplication.EmailAddress = jobApplication.EmailAddress;
            newJobApplication.FirstName = jobApplication.FirstName;
            newJobApplication.LastName = jobApplication.LastName;
            newJobApplication.PhoneNumber = jobApplication.PhoneNumber;
            newJobApplication.JobOfferId = jobApplication.JobOfferId;
            var offer = _context.JobOffers.Include(x => x.JobApplications).FirstOrDefault(x => x.Id == jobApplication.JobOfferId);
            offer.JobApplications.Add(newJobApplication);
            await _context.JobApplications.AddAsync(newJobApplication).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("test@example.com", "Super Company Team"));
            msg.AddTo(new EmailAddress(newJobApplication.EmailAddress, newJobApplication.FirstName + " " + newJobApplication.LastName));
            msg.SetSubject("Application sent, confirmation");
            msg.AddContent(MimeType.Text, "Hello, your application was sent correctly");
            //var apiKey = Configuration["DatabaseConnectionString"];
            var client = new SendGridClient(apiKey);
            var resposne = await client.SendEmailAsync(msg);

            return RedirectToAction("Details", "JobOffer", new { id = newJobApplication.JobOfferId });
        }
    }   
}