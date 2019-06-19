using RyanGrandeGifts.Models;
using System.Collections.Generic;

namespace RyanGrandeGifts.ViewModels
{
    public class CartCheckoutViewModel
    {
        public Address Address { get; set; }
        public IEnumerable<HamperOrder> HamperOrders { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
    }
}