using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RyanGrandeGifts.Models;
using RyanGrandeGifts.Services;
using RyanGrandeGifts.ViewModels;

namespace RyanGrandeGifts.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        #region Constructor
        private readonly IDataService<Category> _categoryManager;

        public CategoryController(IDataService<Category> categoryManager)
        {
            _categoryManager = categoryManager;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            CategoryIndexViewModel vm = new CategoryIndexViewModel
            {
                Categories = _categoryManager.GetAll(),
            };
            return View(vm);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            CategoryCreateViewModel vm = new CategoryCreateViewModel { };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Category c = new Category
                {
                    CategoryDescription = vm.CategoryDescription,
                    CategoryName = vm.CategoryName,
                };
                _categoryManager.Add(c);
                return RedirectToAction("Index");
            }
            return View(vm);
            
        }
        #endregion
        #region Read
        [HttpGet]
        public IActionResult Read(string id)
        {
            Category c = _categoryManager.Read(id);
            CategoryReadViewModel vm = new CategoryReadViewModel
            {
                CategoryDescription = c.CategoryDescription,
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Hampers = c.Hampers,
            };
            return View(vm);
        }
        #endregion
        #region Update
        [HttpGet]
        public IActionResult Update(string id)
        {
            Category c = _categoryManager.Read(id);
            CategoryUpdateViewModel vm = new CategoryUpdateViewModel
            {
                CategoryDescription = c.CategoryDescription,
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CategoryUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Category c = _categoryManager.Read(vm.CategoryId);
                if (c == null)
                {
                    return NotFound();
                }
                c.CategoryDescription = vm.CategoryDescription;
                c.CategoryName = vm.CategoryName;
                _categoryManager.Update(c);
                return RedirectToAction("Read",new { id = vm.CategoryId });
            }
            return View(vm);
        }
        #endregion
        #region Delete
        public IActionResult Delete(string id)
        {
            Category c = _categoryManager.Read(id);
            if (c == null)
            {
                return NotFound();
            }
            _categoryManager.Delete(c);
            return RedirectToAction("Index");
        }
        #endregion
    }
}