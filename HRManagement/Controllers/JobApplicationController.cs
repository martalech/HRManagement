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
    [Route("[controller]/[action]")]
    public class JobApplicationController : Controller
    {
        DataContext _context;
        public JobApplicationController(DataContext context)
        {
            _context = context;
        }

        public IActionResult ApplyFor(int offerId)
        {
            var jobApp = new JobApplication();
            jobApp.OfferId = offerId;
            return View(jobApp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ApplyFor(JobApplication jobApplication)
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
            newJobApp.OfferId = jobApplication.OfferId;
            var offer = _context.JobOffers.Include(x => x.JobApplications).FirstOrDefault(x => x.Id == jobApplication.OfferId);
            offer.JobApplications.Add(newJobApp);
            await _context.JobApplications.AddAsync(newJobApp);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "JobOffer", new { id = newJobApp.OfferId });
        }
    }   
}