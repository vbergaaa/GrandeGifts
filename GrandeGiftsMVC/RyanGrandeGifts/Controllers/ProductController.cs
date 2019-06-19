using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RyanGrandeGifts.Helpers;
using RyanGrandeGifts.Models;
using RyanGrandeGifts.Services;
using RyanGrandeGifts.ViewModels;

namespace RyanGrandeGifts.Controllers
{
    [Authorize(Roles="Admin")]
    public class ProductController : Controller
    {
        private readonly IDataService<Product> _productDataService;
        public ProductController(IDataService<Product> productDataService)
        {
            _productDataService = productDataService;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _productDataService.GetAll();
            ProductIndexViewModel vm = new ProductIndexViewModel
            {
                Products = products,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProducts(string productsStr) 
        {
            if (productsStr == null)
            {
                return RedirectToAction("Index");
            }
            IQueryable<Product> allProducts = _productDataService.GetAll();
            IEnumerable<string> productList = Regex.Split(productsStr," *, *");
            IEnumerable<Product> products = productList.Select(p => new Product { ProductName = StringFormatHelper.CaptialiseFirst(p) });
            foreach (var product in products)
            {
                Product match = allProducts.Where(p => p.ProductName == product.ProductName).FirstOrDefault();
                if (match == null)
                {
                    _productDataService.Add(product);
                }
            }
            return RedirectToAction("Index");
        }
    }
}