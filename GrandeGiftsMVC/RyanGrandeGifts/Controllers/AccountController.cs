using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RyanGrandeGifts.Models;
using RyanGrandeGifts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RyanGrandeGifts.Services;

namespace RyanGrandeGifts.Controllers
{
    public class AccountController : Controller
    {
        #region Props & Constructors
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDataService<Order> _OrderManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, 
            SignInManager<ApplicationUser> signInManager,
            IDataService<Order> _OrderManager)
        {
            _signInManager = signInManager;
            this._OrderManager = _OrderManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register(string returnUrl = "/")
        {
            AccountRegisterViewModel vm = new AccountRegisterViewModel { ReturnUrl = returnUrl};
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // rejects form if not filled out properly
                return View(vm);
            }

            ApplicationUser c = new ApplicationUser
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName = vm.UserName
            };
            IdentityResult result = await _userManager.CreateAsync(c, vm.Password);

            if (result.Succeeded)
            {
                // add to role here
                IdentityResult roleResult = await _userManager.AddToRoleAsync(c, "Customer");
                await _signInManager.SignInAsync(c, false, null);
                if (roleResult.Succeeded)
                    return Redirect(vm.ReturnUrl);

                foreach (var error in roleResult.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            else
            {
                // alerts user to the reason it failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(vm);

        }
        #endregion
        #region Login
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            AccountLoginViewModel vm = new AccountLoginViewModel { ReturnUrl = returnUrl };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, vm.RememberMe, false);
                if (result.Succeeded)
                {
                    if (vm.ReturnUrl != "")
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Unfortunately we couldn't log you in. \nPlease check your username and password and try again");
            }

            return View(vm);
        }
        #endregion
        #region Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
        #region View Profile
        [Authorize]
        public IActionResult Profile()
        {
            ApplicationUser user = _userManager.Users.Where(u => u.UserName == User.Identity.Name).Include(u => u.Addresses).FirstOrDefault();
            AccountProfileViewModel vm;
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                vm = new AccountProfileViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Addresses = user.Addresses,
                    ProfilePictureUrl = ""
                };
            }
            return View(vm);
        }
        #endregion
        #region Update Profile
        [Authorize]
        [HttpGet]
        public IActionResult Update()
        {
            ApplicationUser user = _userManager.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            AccountUpdateViewModel vm = new AccountUpdateViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AccountUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Users.Where(u => u.Id == vm.Id).FirstOrDefault();
                if (user == null)
                {
                    return NotFound();
                }
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.UserName = vm.UserName;

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, false, null);
                    return RedirectToAction("Profile");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }
        #endregion
        #region Change Password
        [HttpGet]
        [Authorize]
        public IActionResult UpdatePassword()
        {
            ApplicationUser user = _userManager.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            AccountChangePasswordViewModel vm = new AccountChangePasswordViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(AccountChangePasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Users.Where(u => u.Id == vm.Id).FirstOrDefault();
                IdentityResult result = await _userManager.ChangePasswordAsync(user, vm.OldPassword, vm.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);

        }
        #endregion
        #region Register New Admin
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            AccountRegisterViewModel vm = new AccountRegisterViewModel { };
            return View(vm);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(AccountRegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // rejects form if not filled out properly
                return View(vm);
            }

            ApplicationUser c = new ApplicationUser
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName = vm.UserName
            };
            IdentityResult result = await _userManager.CreateAsync(c, vm.Password);

            if (result.Succeeded)
            {
                // add to role here
                IdentityResult roleResult = await _userManager.AddToRoleAsync(c, "Admin");
                if (roleResult.Succeeded)
                    return RedirectToAction("Index", "Home");

                foreach (var error in roleResult.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            else
            {
                // alerts user to the reason it failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(vm);
        }
        #endregion
        #region View Orders
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            IEnumerable<Order> orders = _OrderManager.GetAll().Where(o => o.ApplicationUserId == user.Id).Include(o => o.HamperOrders).ThenInclude(ho => ho.Hamper);

            AccountOrderViewModel vm = new AccountOrderViewModel
            {
                OrderCount = orders.Count(),
                Orders = orders
            };
            return View(vm);
        }
        #endregion
        // //CREATE ROLE LOGIC HERE
        //[HttpGet]
        // public IActionResult CreateRole()
        // {
        //     return View();
        // }
        // [HttpPost]
        // public async Task<IActionResult> CreateRole(CreateRoleViewModel vm)
        // {
        //     IdentityRole role = new IdentityRole(vm.RoleName);
        //     IdentityResult result = await _roleManager.CreateAsync(role);
        //     if (result.Succeeded)
        //         return RedirectToAction("Register", "Account");
        //     return View(vm);
        // }
    }
}
