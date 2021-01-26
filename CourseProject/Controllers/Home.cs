using CourseProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class Home : Controller
    {
        private readonly ILogger<Home> _logger;

        public Home(ILogger<Home> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeLang()
        {
            string current = CultureInfo.CurrentCulture.Name;
            string lang = current == "en" ? "ru" : "en";
            Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
               new CookieOptions
               {
                   Expires = DateTimeOffset.UtcNow.AddYears(1)
               }
           );
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        public IActionResult Blocked()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
