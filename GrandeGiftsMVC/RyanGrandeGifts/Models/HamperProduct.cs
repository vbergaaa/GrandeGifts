using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanGrandeGifts.Models
{
    public class HamperProduct
    { 
        public string HamperProductId { get; set; }
        public string HamperId { get; set; }
        public Hamper Hamper { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
