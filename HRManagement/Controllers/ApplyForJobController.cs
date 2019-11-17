using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    public class ApplyForJobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}