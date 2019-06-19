using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RyanGrandeGifts.Models;
using RyanGrandeGifts.Services;
using Microsoft.EntityFrameworkCore;
using RyanGrandeGifts.ViewModels;

namespace RyanGrandeGifts.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        #region Props and Constructors
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDataService<Address> _addressDataservice;

        public AddressController(UserManager<ApplicationUser> userManager, IDataService<Address> addressDataservice)
        {
            _userManager = userManager;
            _addressDataservice = addressDataservice;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            ApplicationUser user = _userManager.Users.Include(u=>u.Addresses).Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            AddressIndexViewModel vm = new AddressIndexViewModel
            {
                UserName = user.UserName,
                Addresses = user.Addresses
            };
            return View(vm);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create(string returnUrl = "/Account/Profile")
        {
            AddressCreateViewModel vm = new AddressCreateViewModel { ReturnUrl = returnUrl};
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddressCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

                Address a = new Address
                {
                    City = vm.City,
                    PostCode = vm.PostCode,
                    State = vm.State,
                    StreetName = vm.StreetName,
                    StreetNumber = vm.StreetNumber,
                    Suburb = vm.Suburb,
                    UnitNumber = vm.UnitNumber,
                    CustomerId = user.Id
                };
                _addressDataservice.Add(a);
                return Redirect(vm.ReturnUrl);
            }
            return View(vm);
        }
        #endregion
        #region Update
        [HttpGet]
        public IActionResult Update(string id)
        {
            Address a = _addressDataservice.Read(id);
            if (a == null)
            {
                return NotFound();
            }
            else
            {
                AddressUpdateViewModel vm = new AddressUpdateViewModel
                {
                    City = a.City,
                    PostCode = a.PostCode,
                    State = a.State,
                    StreetName = a.StreetName,
                    StreetNumber = a.StreetNumber,
                    Suburb = a.Suburb,
                    UnitNumber = a.UnitNumber,
                    Id = a.AddressId,
                };
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(AddressUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Address address = _addressDataservice.Read(vm.Id);
                address.City = vm.City;
                address.PostCode = vm.PostCode;
                address.State = vm.State;
                address.StreetName = vm.StreetName;
                address.StreetNumber = vm.StreetNumber;
                address.Suburb = vm.Suburb;
                address.UnitNumber = vm.UnitNumber;

                _addressDataservice.Update(address);
                return RedirectToAction("Index");
            
            }

            return View(vm);
        }
        #endregion
        #region Delete
        public IActionResult Delete(string id)
        {
            Address a = _addressDataservice.Read(id);
            _addressDataservice.Delete(a);
            return RedirectToAction("Index");
        }
        #endregion
    }
}