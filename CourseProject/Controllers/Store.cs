﻿using BusinessLayer.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CourseProject.ViewModels;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class Store : Controller
    {
        private readonly ICloudinaryService cloudinaryService;

        private readonly ICPUnitOfWork cPUnitOfWork;

        public Store(ICloudinaryService cloudinaryService, ICPUnitOfWork cPUnitOfWork)
        {
            this.cloudinaryService = cloudinaryService;
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

        [HttpGet]
        public IActionResult CreateCollection()
        {
            return View(new CreateCollectionVM());
        }

        [HttpPost]
        public IActionResult CreateCollection(CreateCollectionVM model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateItem()
        {
            return View(new CreateItemVM());
        }

        [HttpPost]
        public IActionResult CreateItem(CreateItemVM model)
        {
            return null;
        }

        public IActionResult Collection(int id)
        {
            return View();
        }

        public IActionResult Item(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var fileDesc = new FileDescription(file.FileName, file.OpenReadStream());
            var res = await cloudinaryService.UploadAsync(fileDesc);
            return Json(res);
        }

        public IActionResult _Collections(int id)
        {
            var model = new ProfileCollectionsVM
            {
                Collections = new List<CollectionVM>(),
                IsEditable = true
            };
            return PartialView("Profile/_Collections", model);
        }

        public IActionResult _Items()
        {
            var model = new ProfileItemsVM
            {
                Items = new List<ItemVM>(),
                IsEditable = true
            };
            return PartialView("Profile/_Items", model);
        }
    }
}