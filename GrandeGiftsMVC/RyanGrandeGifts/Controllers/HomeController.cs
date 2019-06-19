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
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDataService<Hamper> _HamperIndexViewModel;

        public HomeController(IDataService<Hamper> _hamperIndexViewModel)
        {
            _HamperIndexViewModel = _hamperIndexViewModel;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<Hamper> top3 = _HamperIndexViewModel.GetAll()
                .OrderByDescending(h => h.HamperOrders.Count())
                .Take(3)
                .ToList();

            foreach(Hamper h in top3)
            {
                if (h.Picture == null)
                {
                    h.Picture = "DefaultHamper.png";
                }
            }
            
            HomeIndexViewModel vm = new HomeIndexViewModel
            {
                TopHampers = top3
            };
            return View(vm);
        }

        [AllowAnonymous]
        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult TermsOfUse()
        {
            return View();
        }
    }
}