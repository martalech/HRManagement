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
            return View(await _context.JobOffers.Include(x => x.CompanyName).FirstOrDefaultAsync(o => o.Id == id));
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.JobOffers.FirstOrDefaultAsync(o => o.Id == id));
        }
    }
}