using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRManagement.Models;

namespace HRManagement.Controllers
{
    [Route("[controller]/[action]")]
    public class JobApplicationController : Controller
    {
        public IActionResult ApplyFor(int offerId)
        {
            var jobApp = new JobApplication();
            jobApp.OfferId = offerId;
            return View(jobApp);
        }
    }
}