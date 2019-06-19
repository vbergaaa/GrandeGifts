using RyanGrandeGifts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanGrandeGifts.ViewModels
{
    public class AddressIndexViewModel
    {
        public string UserName { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
    }
}
