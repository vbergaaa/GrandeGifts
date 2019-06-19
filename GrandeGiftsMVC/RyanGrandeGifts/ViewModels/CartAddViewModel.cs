using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanGrandeGifts.ViewModels
{
    public class CartAddViewModel
    {
        public string HamperId { get; set; }
        public string ReturnUrl { get; set; }
        public int Quantity { get; set; }
    }
}
