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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobOffers.ToListAsync().ConfigureAwait(false));
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _context.JobOffers.Include(x => x.CompanyName).Include(x => x.JobApplications).FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Companies = _context.Companies.ToList();
            return View(await _context.JobOffers.FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false));
        }

        [HttpGet]
        public PagingViewModel GetJobOffers(int pageNo = 1, int pageSize = 1)
        {
            int totalPage, totalRecord;

            totalRecord = _context.JobOffers.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            var record = (from u in _context.JobOffers
                          orderby u.JobTitle
                          select u).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            PagingViewModel empData = new PagingViewModel
            {
                JobOffers = record,
                TotalPage = totalPage
            };

            return empData;
        }


        [HttpGet]
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
            if (model.Salary < 0)
            {
                ModelState.AddModelError("Salary", "Salary cannot be negative");
            }
            if (model.CompanyName is null || (!(model.CompanyName is null) && model.CompanyNameId == 0))
            {
                ModelState.AddModelError("CompanyName", "Company cannot be left empty");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.CompanyName = _context.Companies.FirstOrDefault(x => x.Id == model.CompanyName.Id);
            model.CompanyNameId = model.CompanyName.Id;

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
            offer.CompanyNameId = offer.CompanyName.Id;
            offer.Description = model.Description;
            offer.Salary = model.Salary;
            offer.ContractType = model.ContractType;
            _context.Update(offer);
            await _context.SaveChangesAsync().ConfigureAwait(false);
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
            var jobapps = await _context.JobApplications.ToListAsync().ConfigureAwait(false);
            jobapps.RemoveAll(x => x.JobOfferId == idd);
            _context.JobOffers.Remove(new JobOffer() { Id = idd.Value });
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return RedirectToAction("Index");
        }
    }
}