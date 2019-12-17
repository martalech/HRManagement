using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.EntityFramework;
using HRManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Controllers
{
    [Route("[controller]/[action]")]
    public class JobOfferController : Controller
    {
        private readonly DataContext _context;
        
        public JobOfferController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.JobOffers.ToListAsync().ConfigureAwait(false));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _context.JobOffers.Include(x => x.CompanyName).Include(x => x.JobApplications).FirstOrDefaultAsync(o => o.Id == id));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Companies = _context.Companies.ToList();
            return View(await _context.JobOffers.FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false));
        }

        public IActionResult Create()
        {
            var jobOffer = new JobOffer();
            jobOffer.JobApplications = new List<JobApplication>();
            ViewBag.Companies = _context.Companies.ToList();
            return View(jobOffer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobOffer model)
        {
            model.CompanyName = _context.Companies.FirstOrDefault(x => x.Id == model.CompanyName.Id);
            ViewBag.Companies = _context.Companies.ToList();
            if (string.IsNullOrWhiteSpace(model.JobTitle))
            {
                ModelState.AddModelError("JobTitle", "Job title cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(model.Location))
            {
                ModelState.AddModelError("Location", "Job location cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(model.ContractType))
            {
                ModelState.AddModelError("ContractType", "Job location cannot be empty");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string phoneNrRegex = "[0 - 9] + (\\.[0-9] [0-9]?)?";
            string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            await _context.JobOffers.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(JobOffer model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var offer = await _context.JobOffers.FirstOrDefaultAsync(x => x.Id == model.Id);
            offer.JobTitle = model.JobTitle;
            offer.CompanyName = _context.Companies.FirstOrDefault(x => x.Id == model.CompanyName.Id);
            offer.Description = model.Description;
            offer.Salary = model.Salary;
            offer.ContractType = model.ContractType;
            _context.Update(offer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? idd)
        {
            if (idd == null)
            {
                return BadRequest($"id should not be null");
            }
            var jobapps = await _context.JobApplications.ToListAsync();
            jobapps.RemoveAll(x => x.OfferId == idd);
            _context.JobOffers.Remove(new JobOffer() { Id = idd.Value });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}