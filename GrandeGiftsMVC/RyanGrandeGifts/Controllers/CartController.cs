using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RyanGrandeGifts.Helpers;
using RyanGrandeGifts.Models;
using RyanGrandeGifts.Services;
using RyanGrandeGifts.ViewModels;

namespace RyanGrandeGifts.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IDataService<Order> _OrderDataService;
        private readonly IDataService<Hamper> _hamperDataService;
        private readonly IDataService<Address> _AddressDataService;
        private readonly IDataService<HamperOrder> _HamperOrderDataService;
        private readonly UserManager<ApplicationUser> _UserManager;

        public CartController(IDataService<Order> _orderDataService, IDataService<Hamper> _hamperDataService,IDataService<Address> _addressDataService, IDataService<HamperOrder> _hamperOrderDataService, UserManager<ApplicationUser> _userManager)
        {
            _OrderDataService = _orderDataService;
            this._hamperDataService = _hamperDataService;
            _AddressDataService = _addressDataService;
            _HamperOrderDataService = _hamperOrderDataService;
            _UserManager = _userManager;
        }
        
        [AllowAnonymous]
        public IActionResult Index()
        {
            IEnumerable<HamperOrder> cart = SessionHelper.GetObectFromJson<IEnumerable<HamperOrder>>(HttpContext.Session, "cart");
            CartIndexViewModel vm = new CartIndexViewModel
            {
                HamperOrders = cart,
            };
            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Add(string id, int qty, string url)
        {
            List<HamperOrder> list;
            HamperOrder ho = new HamperOrder // entity to add to cart
            {
                HamperId = id,
                Qty = qty,
            };

            // get or intialise cart
            IEnumerable<HamperOrder> cart = SessionHelper.GetObectFromJson<IEnumerable<HamperOrder>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                list = new List<HamperOrder>();
            }
            else {
                list = cart.ToList<HamperOrder>();
            }

            // adjust cart
            if (list.Where(o => o.HamperId == id).FirstOrDefault() == null)
            {
                // add item to cart
                ho.Hamper = _hamperDataService.GetSingle(h => h.HamperId == id);
                list.Add(ho);

            }
            else
            {
                
                // add qty to existing item
                var hamperOrder = list.Where(o => o.HamperId == id).FirstOrDefault();
                int index = list.IndexOf(hamperOrder);

                list[index].Qty += qty;

                if (list[index].Qty == 0)
                {
                    list.Remove(list[index]);
                }
            }

            // save cart
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", list);

            return Redirect(url);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CartAddViewModel vm)
        {
            return Add(vm.HamperId, vm.Quantity, vm.ReturnUrl);
        }
        public async Task<IActionResult> Checkout()
        {
            ApplicationUser user = await _UserManager.GetUserAsync(HttpContext.User);
            CartCheckoutViewModel vm = new CartCheckoutViewModel
            {
                Addresses = _AddressDataService.GetAll().Where(a => a.CustomerId ==user.Id),
                HamperOrders = SessionHelper.GetObectFromJson<IEnumerable<HamperOrder>>(HttpContext.Session, "cart")
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CartCheckoutViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.HamperOrders = SessionHelper.GetObectFromJson<IEnumerable<HamperOrder>>(HttpContext.Session, "cart");
                ApplicationUser user = await _UserManager.GetUserAsync(HttpContext.User);
                Order o = new Order
                {
                    OrderDate = DateTime.Now,
                    ApplicationUserId = user.Id,
                };
                _OrderDataService.Add(o);

                foreach (HamperOrder hamperOrder in vm.HamperOrders)
                {
                    hamperOrder.OrderId = o.OrderId;
                    hamperOrder.Hamper = null;
                    _HamperOrderDataService.Add(hamperOrder);
                }
                return RedirectToAction("Success");
            }
            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RemoveItem(string id)
        {

            // get cart
            List<HamperOrder> cart = SessionHelper.GetObectFromJson<IEnumerable<HamperOrder>>(HttpContext.Session, "cart").ToList();
            if (cart == null)
                return NotFound();


            // get hamper order
            HamperOrder hamperOrder = cart.Where(ho => ho.HamperId == id).FirstOrDefault();
            if (hamperOrder == null)
                return NotFound();

            cart.Remove(hamperOrder);

            // save cart
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index");
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}