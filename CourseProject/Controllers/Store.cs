using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class Store : Controller
    {
        public IActionResult Main()
        {
            return View();
        }

        public IActionResult Collection()
        {
            return View();
        }

        public IActionResult Item()
        {
            return View();
        }

    }
}
