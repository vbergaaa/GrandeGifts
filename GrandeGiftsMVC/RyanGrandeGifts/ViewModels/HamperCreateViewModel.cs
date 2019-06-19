using RyanGrandeGifts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RyanGrandeGifts.ViewModels
{
    public class HamperCreateViewModel
    {
        [Required]
        public string HamperName { get; set; }
        [Required]
        public string HamperDescription { get; set; }
        [Required, DataType(DataType.Currency)]
        public double HamperPrice { get; set; }
        [Required]
        public string CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string Picture { get; set; }
    }
}
