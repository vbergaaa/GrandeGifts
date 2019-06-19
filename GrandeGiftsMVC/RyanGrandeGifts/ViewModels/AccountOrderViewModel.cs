using RyanGrandeGifts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanGrandeGifts.ViewModels
{
    public class AccountOrderViewModel
    {
        public int OrderCount { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
