using RyanGrandeGifts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanGrandeGifts.ViewModels
{
    public class HamperIndexViewModel
    {
        public IEnumerable<Hamper> Hampers { get; set; }

        public string FilterKeyword { get; set; }
        public string FilterCategoryName { get; set; }
        public double FilterMinPrice { get; set; }
        public double FilterMaxPrice { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public bool IncludeInactive { get; set; }
    }
}
