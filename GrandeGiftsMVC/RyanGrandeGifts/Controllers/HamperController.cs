using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RyanGrandeGifts.Helpers;
using RyanGrandeGifts.Models;
using RyanGrandeGifts.Services;
using RyanGrandeGifts.ViewModels;

namespace RyanGrandeGifts.Controllers
{
    [Authorize(Roles ="Admin")]
    public class HamperController : Controller
    {
        private readonly IDataService<Hamper> _hamperManager;
        private readonly IDataService<Category> _categoryManager;
        private readonly IHostingEnvironment environment;
        private readonly UserManager<ApplicationUser> _UserManager;

        public HamperController(IDataService<Hamper> hamperManager, 
            IDataService<Category> categoryManager,
            IHostingEnvironment environment,
            UserManager<ApplicationUser> _userManager)
        {
            _hamperManager = hamperManager;
            _categoryManager = categoryManager;
            this.environment = environment;
            _UserManager = _userManager;
        }



        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<Category> categories = _categoryManager.GetAll();
            HamperCreateViewModel vm = new HamperCreateViewModel
            {
                Categories = categories
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HamperCreateViewModel vm, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByNameAsync(User.Identity.Name);

                Hamper h = new Hamper
                {
                    Active = true,
                    CategoryId = vm.CategoryId,
                    HamperDescription = vm.HamperDescription,
                    HamperName = vm.HamperName,
                    HamperPrice = vm.HamperPrice,
                };

                if (file != null)
                {
                    var uploadPath = Path.Combine(environment.WebRootPath, "uploads/hampers/");
                    string extension = Path.GetExtension(file.FileName);
                    string fileName = StringFormatHelper.ToTitleCase(h.HamperName)+"-1"+extension;
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    h.Picture = fileName;
                }
                _hamperManager.Add(h);
                return RedirectToAction("Index");
            }
            IEnumerable<Category> categories = _categoryManager.GetAll();
            vm.Categories = categories;
            return View(vm);
        }


        [AllowAnonymous]
        public IActionResult Index(HamperIndexViewModel vm)
        {
            IQueryable<Hamper> hampers = _hamperManager.GetAll().Include(h=>h.Category);
            List<Category> categories = _categoryManager.GetAll().ToList();
            categories.Insert(0, new Category { CategoryName=""});
            if( !vm.IncludeInactive)
            {
                hampers = hampers.Where(h => h.Active);
            }
            if (vm.FilterCategoryName != null)
            {
                Category cat = _categoryManager.GetAll().Where(c => c.CategoryName == vm.FilterCategoryName).FirstOrDefault();
                hampers = hampers.Where(h=>h.CategoryId == cat.CategoryId);
            };
            if (vm.FilterMinPrice > 0)
            {
                hampers = hampers.Where(h => h.HamperPrice >= vm.FilterMinPrice);
            }
            if (vm.FilterMaxPrice > 0)
            {
                hampers = hampers.Where(h => h.HamperPrice <= vm.FilterMaxPrice);
            }
            if (vm.FilterKeyword != null && vm.FilterKeyword != "")
            {
                hampers = hampers.Where(h => h.Category.CategoryName.Contains(vm.FilterKeyword) || h.HamperName.Contains(vm.FilterKeyword));
            }
            vm.Categories = categories;
            vm.Hampers = hampers;
            return View(vm);
        }


        public IActionResult Update(string id, string url = "~/Hamper/")
        {
            Hamper h = _hamperManager.Read(id);
            if (h==null)
            {
                return NotFound();
            }
            HamperUpdateViewModel vm = new HamperUpdateViewModel
            {
                Categories = _categoryManager.GetAll(),
                CategoryId = h.CategoryId,
                HamperDescription = h.HamperDescription,
                HamperName = h.HamperName,
                HamperPrice = h.HamperPrice,
                Retired = !h.Active,
                HamperId = h.HamperId,
                Picture = h.Picture,
                ReturnUrl = url
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(HamperUpdateViewModel vm)
        {
            Hamper h = _hamperManager.Read(vm.HamperId);
            if (h == null )
            {
                return NotFound();
            }
            if (vm.File != null)
            {
                var uploadPath = Path.Combine(environment.WebRootPath, "uploads/hampers/");
                string extension = Path.GetExtension(vm.File.FileName);
                string fileName = StringFormatHelper.ToTitleCase(h.HamperName) + "-1" + extension;
                using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                {
                    await vm.File.CopyToAsync(fileStream);
                }
                h.Picture = fileName;
            }
            h.HamperDescription = vm.HamperDescription;
            h.HamperName = vm.HamperName;
            h.HamperPrice = vm.HamperPrice;
            h.CategoryId = vm.CategoryId;
            h.Active = !vm.Retired;

            _hamperManager.Update(h);

            return Redirect(vm.ReturnUrl);
        }

        [AllowAnonymous]
        public IActionResult Filter(HamperIndexViewModel vm)
        {
            return RedirectToAction("Index", "Hamper", vm);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Read(string id)
        {
            Hamper hamper = _hamperManager.GetAll()
                .Include(h=>h.HamperOrders)
                .Include(h=>h.Category)
                .FirstOrDefault(h => h.HamperId == id);

            HamperReadViewModel vm = new HamperReadViewModel
            {
                Active = hamper.Active,
                Category = hamper.Category,
                OrderCount = hamper.HamperOrders.Count(),
                HamperDescription = hamper.HamperDescription,
                HamperId = hamper.HamperId,
                HamperName = hamper.HamperName,
                HamperPrice = hamper.HamperPrice,
                Picture = hamper.Picture,
            };
            if (vm.Picture == null)
            {
                vm.Picture = "DefaultHamper.png";
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Retire(string id)
        {
            Hamper h = _hamperManager.Read(id);
            h.Active = false;
            _hamperManager.Update(h);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ReActivate(string id)
        {
            Hamper h = _hamperManager.Read(id);
            h.Active = true;
            _hamperManager.Update(h);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetInactiveHampers()
        {
            HamperIndexViewModel vm = new HamperIndexViewModel
            {
                Hampers = _hamperManager.GetAll().Where(h => !h.Active)
            };
            return RedirectToAction("Index", vm);
        }
    }
}