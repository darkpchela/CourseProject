using CourseProject.ViewModels;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class Store : Controller
    {
        private readonly ICPUnitOfWork cPUnitOfWork;
        public Store(ICPUnitOfWork cPUnitOfWork)
        {
            this.cPUnitOfWork = cPUnitOfWork;
        }
        public IActionResult Profile(int? id)
        {
            if (!id.HasValue)
                id = cPUnitOfWork.UsersRepository.GetAll().FirstOrDefault(u => u.Id.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier)).Id;
            var model = new ProfileVM
            {
                Username = User.Identity.Name,
                User = cPUnitOfWork.UsersRepository.GetAll().FirstOrDefault(u => u.Id == id)
            };
            return View(model);
        }

        public IActionResult Collection(int id)
        {
            return View();
        }

        public IActionResult Item(int id)
        {
            return View();
        }

        public IActionResult _Collections()
        {
            return PartialView("Profile/_Collections", new List<CollectionVM>());
        }

        public IActionResult _Items()
        {
            return PartialView("Profile/_Items", new List<ItemVM>());
        }

    }
}
