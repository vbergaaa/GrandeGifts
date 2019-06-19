using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanGrandeGifts.Models
{
    public class Hamper { 
    
        public string HamperId { get; set; }
        public string HamperName { get; set; }
        public string HamperDescription { get; set; }
        public double HamperPrice { get; set; }
        public string Picture { get; set; } = "DefaultHamper.png";
        //public int HampersInStock { get; set; }
        public bool Active { get; set; }
        public Category Category { get; set; }
        public string CategoryId { get; set; }
        public ICollection<HamperProduct> HamperProducts { get; set; }
        public ICollection<HamperOrder> HamperOrders { get; set; }
    }
}
