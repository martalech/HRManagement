using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagement.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRManagement.Models;

namespace HRManagement.Controllers
{
    public class CompanyController : Controller
    {
        private readonly DataContext _context;

        public CompanyController(DataContext context)
        {
            //ViewBag.Companies = new SelectList(_context.Companies, "Id", "Name");
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync().ConfigureAwait(false));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _context.Companies.FirstOrDefaultAsync(o => o.Id == id));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Companies = _context.Companies.ToList();
            return View(await _context.Companies.FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false));
        }

        public IActionResult Create()
        {
            var company = new Company();
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Company model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _context.Companies.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Company model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == model.Id);
            company.Name = model.Name;
            _context.Update(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = model.Id });
        }
    }
}