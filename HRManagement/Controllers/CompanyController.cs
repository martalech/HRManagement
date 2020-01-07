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
    /// <summary>
    /// Company controller
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class CompanyController : Controller
    {
        /// <summary>
        /// Data context for company controller
        /// </summary>
        private readonly DataContext _context;

        /// <summary>
        /// Company controller's constructor
        /// </summary>
        /// <param name="context">Data context for constructor</param>
        public CompanyController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Main view for companies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Detail view for company
        /// </summary>
        /// <param name="id">Company's id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _context.Companies.FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false));
        }

        /// <summary>
        /// Edit view for company
        /// </summary>
        /// <param name="id">Company's id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Companies = _context.Companies.ToList();
            return View(await _context.Companies.FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false));
        }

        /// <summary>
        /// View for creating new company
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            var company = new Company();
            return View(company);
        }

        /// <summary>
        /// Adds created company if valid
        /// </summary>
        /// <param name="model">Created company model</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm]Company model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _context.Companies.AddAsync(model).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Edit company if valid
        /// </summary>
        /// <param name="model">Edited company model</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm]Company model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == model.Id);
            company.Name = model.Name;
            _context.Update(company);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return RedirectToAction("Details", new { id = model.Id });
        }
    }
}