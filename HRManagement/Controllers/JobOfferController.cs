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
            //ViewBag.Companies = new SelectList(_context.Companies, "Id", "Name");
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.JobOffers.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _context.JobOffers.Include(x => x.CompanyName).Include(x => x.JobApplications).FirstOrDefaultAsync(o => o.Id == id));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Companies = _context.Companies.ToList();
            return View(await _context.JobOffers.FirstOrDefaultAsync(o => o.Id == id));
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.CompanyName = _context.Companies.FirstOrDefault(x => x.Id == model.CompanyName.Id);
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
    }
}