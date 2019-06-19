using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanGrandeGifts.Models
{
    public class HamperOrder
    {
        public string HamperOrderId { get; set; }
        public string HamperId { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public Hamper Hamper { get; set; }
        public int Qty { get; set; }
    }
}
